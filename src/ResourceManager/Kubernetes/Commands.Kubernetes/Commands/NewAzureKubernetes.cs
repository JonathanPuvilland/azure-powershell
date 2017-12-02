// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Azure.Commands.Kubernetes.Generated;
using Microsoft.Azure.Commands.Kubernetes.Generated.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Graph.RBAC;
using Microsoft.Azure.Graph.RBAC.Models;
using Microsoft.Azure.Management.Authorization;
using Microsoft.Azure.Management.Authorization.Models;
using Microsoft.Azure.Management.Internal.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microsoft.Azure.Commands.Kubernetes
{
    [Cmdlet(VerbsCommon.New, KubeNounStr, DefaultParameterSetName = DefaultParamSet)]
    [OutputType(typeof(PSObject), typeof(List<PSObject>))]
    public class New : KubeCmdletBase
    {
        private const string DefaultParamSet = "defaultParameterSet";
        private const string SpParamSet = "servicePrincipalParameterSet";
        private readonly Regex _dnsRegex = new Regex("[^A-Za-z0-9-]");

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultParamSet,
            HelpMessage = "Resource Group Name.")]
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = SpParamSet,
            HelpMessage = "Resource Group Name.")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultParamSet,
            HelpMessage = "Kubernetes managed cluster Name.")]
        [Parameter(
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = SpParamSet,
            HelpMessage = "Kubernetes managed cluster Name.")]
        [ValidateNotNullOrEmpty]
        [ValidatePattern("^[a-zA-Z0-9][a-zA-Z0-9_.-]*$")]
        [ValidateLength(2, 64)]
        public string Name { get; set; }

        [Parameter(
            Position = 2,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = SpParamSet,
            HelpMessage =
                "The client ID of the AAD application / service principal used for cluster authentication to Azure APIs.")]
        public string ClientId { get; set; }

        [Parameter(
            Position = 3,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = SpParamSet,
            HelpMessage = "The secret associated with the AAD application / service principal.")]
        public string ClientSecret { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Azure location for the cluster. Defaults to the location of the resource group.")]
        public string Location { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "User name for the Linux Virtual Machines.")]
        public string AdminUserName { get; set; } = "azureuser";

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The DNS name prefix for the cluster.")]
        public string DnsNamePrefix { get; set; }


        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The version of Kubernetes to use for creating the cluster.")]
        [ValidateSet("1.7.7", "1.8.1")]
        public string KubernetesVersion { get; set; } = "1.8.1";

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The default number of nodes for the node pools.")]
        public int NodeCount { get; set; } = 3;

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The default number of nodes for the node pools.")]
        public int? NodeOsDiskSize { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The size of the Virtual Machine.")]
        public string NodeVmSize { get; set; } = "Standard_D2_v2";

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "SSH key file value or key file path. Defaults to {HOME}/.ssh/id_rsa.pub.")]
        public string SshKeyValue { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public Hashtable Tags { get; set; }


        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            RunCmdLet(() =>
            {
                WriteVerbose("Preparing for deployment of your managed Kubernetes cluster.");
                if (!string.IsNullOrEmpty(ResourceGroupName))
                {
                    var rg = RmClient.ResourceGroups.Get(ResourceGroupName);
                    Location = rg.Location;
                    WriteVerbose(string.Format("Using location {0} from the resource group {1}.", Location, ResourceGroupName));
                }

                if (string.IsNullOrEmpty(DnsNamePrefix))
                {
                    DnsNamePrefix = DefaultDnsPrefix();
                }

                WriteVerbose(string.Format("Using DNS name prefix {0}.", DnsNamePrefix));
                SshKeyValue = GetSshKey(SshKeyValue);

                var defaultAgentPoolProfile = new ContainerServiceAgentPoolProfile(
                    "default",
                    NodeVmSize,
                    NodeCount,
                    NodeOsDiskSize,
                    DnsNamePrefix);

                var pubKey = new List<ContainerServiceSshPublicKey> {new ContainerServiceSshPublicKey(SshKeyValue)};

                var linuxProfile =
                    new ContainerServiceLinuxProfile(AdminUserName, new ContainerServiceSshConfiguration(pubKey));

                var acsServicePrincipal = EnsureServicePrincipal(ClientId, ClientSecret);

                var spProfile = new ContainerServiceServicePrincipalProfile(
                    acsServicePrincipal.SpId,
                    acsServicePrincipal.ClientSecret);

                WriteVerbose(string.Format("Deploying your managed Kubernetes cluster.", AcsSpFilePath));
                var managedCluster = new ManagedCluster(
                    Location,
                    name: Name,
                    tags: TagsConversionHelper.CreateTagDictionary(Tags, true),
                    dnsPrefix: DnsNamePrefix,
                    kubernetesVersion: KubernetesVersion,
                    agentPoolProfiles: new List<ContainerServiceAgentPoolProfile> {defaultAgentPoolProfile},
                    linuxProfile: linuxProfile,
                    servicePrincipalProfile: spProfile);

                var cluster = Client.ManagedClusters.CreateOrUpdate(ResourceGroupName, Name, managedCluster);
                WriteObject(cluster);
            });
        }

        /// <summary>
        /// Fetch SSH public key string
        /// </summary>
        /// <param name="sshKeyOrFile">a string representing either the file location, the ssh key pub data or null.</param>
        /// <returns>SSH public key data</returns>
        /// <exception cref="ArgumentException">The SSH key or file argument was null and there was no default pub key in path.</exception>
        private string GetSshKey(string sshKeyOrFile)
        {
            // SSH key was specified as either a file or as key data
            if (!string.IsNullOrEmpty(SshKeyValue))
                if (File.Exists(sshKeyOrFile))
                {
                    WriteVerbose(string.Format("Fetching SSH public key from file {0}", sshKeyOrFile));
                    return File.ReadAllText(sshKeyOrFile);
                }
                else
                {
                    WriteVerbose(string.Format("Using SSH public key data as command line string."));
                    return sshKeyOrFile;
                }

            // SSH key value was not specified, so look in the home directory for the default pub key
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".ssh",
                "id_rsa.pub");

            if (!File.Exists(path))
                throw new ArgumentException(string.Format("Could not find SSH public key in {0}", path));

            WriteVerbose(string.Format("Fetching SSH public key from file {0}", path));
            return File.ReadAllText(path);

            // we didn't find an SSH key and there was no SSH public key in the home directory
        }

        private AcsServicePrincipal EnsureServicePrincipal(string spId = null, string clientSecret = null)
        {
            var acsServicePrincipal = LoadServicePrincipal();
            if (acsServicePrincipal == null)
            {
                WriteVerbose(string.Format("No Service Principal found in {0} for this subscription. Creating a new Service Principal.", AcsSpFilePath));
                // if nothing to load, make one
                if (string.IsNullOrEmpty(ClientSecret))
                {
                    ClientSecret = RandomUtfString(16);
                }
                var salt = RandomUtfString(3);
                var url = string.Format("http://{0}.{1}.{2}.cloudapp.azure.com", salt, DnsNamePrefix, Location);

                BuildServicePrincipal(Name, url, ClientSecret);
                WriteVerbose(string.Format("Created a new Service Principal and assigned the contributor role for this subcription.", AcsSpFilePath));
            }
            StoreServicePrincipal(acsServicePrincipal);
            return acsServicePrincipal;
        }

        private void BuildServicePrincipal(string name, string url, string clientSecret)
        {
            var pwCreds = new PasswordCredential(
                value: clientSecret,
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow.AddYears(2));

            var app = GraphClient.Applications.Create(new ApplicationCreateParameters(
                false,
                name,
                new List<string> {url},
                url,
                passwordCredentials: new List<PasswordCredential> {pwCreds}));

            var success = RetryAction(() =>
            {
                var sp = new ServicePrincipalCreateParameters(
                    app.AppId,
                    true,
                    passwordCredentials: new List<PasswordCredential> {pwCreds});
                GraphClient.ServicePrincipals.Create(sp);
            });

            if (!success)
            {
                throw new CmdletInvocationException(
                    "Could not create a service principal with the right permissions. Are you an Owner on this project?");
            }

            AddRoleAssignment("Contributor", app.AppId);
        }

        private void AddRoleAssignment(string role, string appId)
        {
            var success = RetryAction(() =>
                AuthClient.RoleAssignments.Create(role, appId, new RoleAssignmentProperties(role, appId)));

            if (!success)
            {
                throw new CmdletInvocationException(
                    "Could not create a service principal with the right permissions. Are you an Owner on this project?");
            }
        }

        private static bool RetryAction(Action action)
        {
            var success = false;
            foreach (var i in Enumerable.Range(1, 10))
            {
                try
                {
                    action();
                    success = true;
                    break;
                }
                catch (Exception)
                {
                    // AAD might puke here, so we catch it and try again until success
                    Thread.Sleep(1000 * i);
                }
            }
            return success;
        }

        private AcsServicePrincipal LoadServicePrincipal()
        {
            var config = LoadServicePrincipals();
            return config?[DefaultProfile.DefaultContext.Subscription.Id];
        }

        private Dictionary<string, AcsServicePrincipal> LoadServicePrincipals()
        {
            return File.Exists(AcsSpFilePath)
                ? JsonConvert.DeserializeObject<Dictionary<string, AcsServicePrincipal>>(
                    File.ReadAllText(AcsSpFilePath))
                : null;
        }

        private void StoreServicePrincipal(AcsServicePrincipal acsServicePrincipal)
        {
            var config = LoadServicePrincipals() ?? new Dictionary<string, AcsServicePrincipal>();
            config[DefaultProfile.DefaultContext.Subscription.Id] = acsServicePrincipal;
            File.WriteAllText(AcsSpFilePath, JsonConvert.SerializeObject(config));
        }

        private static string RandomUtfString(int size)
        {
            var rnd = new Random();
            var secretBytes = new byte[size];
            rnd.NextBytes(secretBytes);
            return Encoding.UTF8.GetString(secretBytes);
        }

        private string DefaultDnsPrefix()
        {
            var namePart = string.Join("", _dnsRegex.Replace(Name, "").Take(10));
            if (char.IsDigit(namePart[0]))
            {
                namePart = "a" + string.Join("", namePart.Skip(1));
            }

            var rgPart = _dnsRegex.Replace(ResourceGroupName, "");
            var subPart = string.Join("", DefaultProfile.DefaultContext.Subscription.Id.Take(6));
            return string.Format("{0}-{1}-{2}", namePart, rgPart, subPart);
        }
    }

    public class AcsServicePrincipal
    {
        [JsonProperty("service_principal")]
        public string SpId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}