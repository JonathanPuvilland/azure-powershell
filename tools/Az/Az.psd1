#
# Module manifest for module 'Az'
#
# Generated by: Microsoft Corporation
#
# Generated on: 8/21/2020
#

@{

# Script module or binary module file associated with this manifest.
# RootModule = ''

# Version number of this module.
ModuleVersion = '4.6.1'

# Supported PSEditions
CompatiblePSEditions = 'Core', 'Desktop'

# ID used to uniquely identify this module
GUID = 'd48d710e-85cb-46a1-990f-22dae76f6b5f'

# Author of this module
Author = 'Microsoft Corporation'

# Company or vendor of this module
CompanyName = 'Microsoft Corporation'

# Copyright statement for this module
Copyright = 'Microsoft Corporation. All rights reserved.'

# Description of the functionality provided by this module
Description = 'Microsoft Azure PowerShell - Cmdlets to manage resources in Azure. This module is compatible with WindowsPowerShell and PowerShell Core.
For more information about the Az module, please visit the following: https://docs.microsoft.com/en-us/powershell/azure/'

# Minimum version of the PowerShell engine required by this module
PowerShellVersion = '5.1'

# Name of the PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
DotNetFrameworkVersion = '4.7.2'

# Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# CLRVersion = ''

# Processor architecture (None, X86, Amd64) required by this module
# ProcessorArchitecture = ''

# Modules that must be imported into the global environment prior to importing this module
RequiredModules = @(@{ModuleName = 'Az.Accounts'; ModuleVersion = '1.9.3'; }, 
               @{ModuleName = 'Az.Advisor'; RequiredVersion = '1.1.1'; }, 
               @{ModuleName = 'Az.Aks'; RequiredVersion = '1.2.0'; }, 
               @{ModuleName = 'Az.AnalysisServices'; RequiredVersion = '1.1.4'; }, 
               @{ModuleName = 'Az.ApiManagement'; RequiredVersion = '2.1.0'; }, 
               @{ModuleName = 'Az.ApplicationInsights'; RequiredVersion = '1.1.0'; }, 
               @{ModuleName = 'Az.Automation'; RequiredVersion = '1.4.0'; }, 
               @{ModuleName = 'Az.Batch'; RequiredVersion = '3.1.0'; }, 
               @{ModuleName = 'Az.Billing'; RequiredVersion = '1.0.3'; }, 
               @{ModuleName = 'Az.Cdn'; RequiredVersion = '1.4.3'; }, 
               @{ModuleName = 'Az.CognitiveServices'; RequiredVersion = '1.5.1'; }, 
               @{ModuleName = 'Az.Compute'; RequiredVersion = '4.3.1'; }, 
               @{ModuleName = 'Az.ContainerInstance'; RequiredVersion = '1.0.3'; }, 
               @{ModuleName = 'Az.ContainerRegistry'; RequiredVersion = '1.1.1'; }, 
               @{ModuleName = 'Az.DataBoxEdge'; RequiredVersion = '1.1.0'; }, 
               @{ModuleName = 'Az.DataFactory'; RequiredVersion = '1.10.0'; }, 
               @{ModuleName = 'Az.DataLakeAnalytics'; RequiredVersion = '1.0.2'; }, 
               @{ModuleName = 'Az.DataLakeStore'; RequiredVersion = '1.2.8'; }, 
               @{ModuleName = 'Az.DataShare'; RequiredVersion = '1.0.0'; }, 
               @{ModuleName = 'Az.DesktopVirtualization'; RequiredVersion = '1.0.0'; }, 
               @{ModuleName = 'Az.DeploymentManager'; RequiredVersion = '1.1.0'; }, 
               @{ModuleName = 'Az.DevTestLabs'; RequiredVersion = '1.0.2'; }, 
               @{ModuleName = 'Az.Dns'; RequiredVersion = '1.1.2'; }, 
               @{ModuleName = 'Az.EventGrid'; RequiredVersion = '1.3.0'; }, 
               @{ModuleName = 'Az.EventHub'; RequiredVersion = '1.5.0'; }, 
               @{ModuleName = 'Az.FrontDoor'; RequiredVersion = '1.6.1'; }, 
               @{ModuleName = 'Az.Functions'; RequiredVersion = '1.0.1'; }, 
               @{ModuleName = 'Az.HDInsight'; RequiredVersion = '3.5.0'; }, 
               @{ModuleName = 'Az.HealthcareApis'; RequiredVersion = '1.1.0'; }, 
               @{ModuleName = 'Az.IotHub'; RequiredVersion = '2.5.0'; }, 
               @{ModuleName = 'Az.KeyVault'; RequiredVersion = '2.1.0'; }, 
               @{ModuleName = 'Az.LogicApp'; RequiredVersion = '1.3.2'; }, 
               @{ModuleName = 'Az.MachineLearning'; RequiredVersion = '1.1.3'; }, 
               @{ModuleName = 'Az.Maintenance'; RequiredVersion = '1.1.0'; }, 
               @{ModuleName = 'Az.ManagedServices'; RequiredVersion = '1.1.0'; }, 
               @{ModuleName = 'Az.MarketplaceOrdering'; RequiredVersion = '1.0.2'; }, 
               @{ModuleName = 'Az.Media'; RequiredVersion = '1.1.1'; }, 
               @{ModuleName = 'Az.Monitor'; RequiredVersion = '2.1.0'; }, 
               @{ModuleName = 'Az.Network'; RequiredVersion = '3.3.0'; }, 
               @{ModuleName = 'Az.NotificationHubs'; RequiredVersion = '1.1.1'; }, 
               @{ModuleName = 'Az.OperationalInsights'; RequiredVersion = '2.3.0'; }, 
               @{ModuleName = 'Az.PolicyInsights'; RequiredVersion = '1.3.1'; }, 
               @{ModuleName = 'Az.PowerBIEmbedded'; RequiredVersion = '1.1.2'; }, 
               @{ModuleName = 'Az.PrivateDns'; RequiredVersion = '1.0.3'; }, 
               @{ModuleName = 'Az.RecoveryServices'; RequiredVersion = '2.12.0'; }, 
               @{ModuleName = 'Az.RedisCache'; RequiredVersion = '1.2.1'; }, 
               @{ModuleName = 'Az.Relay'; RequiredVersion = '1.0.3'; }, 
               @{ModuleName = 'Az.Resources'; RequiredVersion = '2.5.0'; }, 
               @{ModuleName = 'Az.ServiceBus'; RequiredVersion = '1.4.1'; }, 
               @{ModuleName = 'Az.ServiceFabric'; RequiredVersion = '2.1.0'; }, 
               @{ModuleName = 'Az.SignalR'; RequiredVersion = '1.2.0'; }, 
               @{ModuleName = 'Az.Sql'; RequiredVersion = '2.9.1'; }, 
               @{ModuleName = 'Az.SqlVirtualMachine'; RequiredVersion = '1.1.0'; }, 
#               @{ModuleName = 'Az.Storage'; RequiredVersion = '2.1.1'; },  # Storage removed to support preview
               @{ModuleName = 'Az.StorageSync'; RequiredVersion = '1.3.0'; }, 
               @{ModuleName = 'Az.StreamAnalytics'; RequiredVersion = '1.0.1'; }, 
               @{ModuleName = 'Az.Support'; RequiredVersion = '1.0.0'; }, 
               @{ModuleName = 'Az.TrafficManager'; RequiredVersion = '1.0.4'; }, 
               @{ModuleName = 'Az.Websites'; RequiredVersion = '1.11.0'; })

# Assemblies that must be loaded prior to importing this module
# RequiredAssemblies = @()

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
# FormatsToProcess = @()

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
# NestedModules = @()

# Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
FunctionsToExport = @()

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = @()

# Variables to export from this module
# VariablesToExport = @()

# Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
AliasesToExport = @()

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
# FileList = @()

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = 'Azure','ARM','ResourceManager','Linux','AzureAutomationNotSupported'

        # A URL to the license for this module.
        LicenseUri = 'https://aka.ms/azps-license'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/Azure/azure-powershell'

        # A URL to an icon representing this module.
        # IconUri = ''

        # ReleaseNotes of this module
        ReleaseNotes = '4.6.0 - August 2020
Az.Accounts
* Loaded all public cloud environments when discovery endpoint doesn''t return default AzureCloud or other public environments [#12633]
* Exposed SubscriptionPolicies in ''Get-AzSubscription'' [#12551]

Az.Automation
* Added ''-RunOn'' parameters to ''Set-AzAutomationWebhook'' to specify a Hybrid Worker Group

Az.Compute
* Added ''-EncryptionAtHost'' parameter to ''New-AzVm'', ''New-AzVmss'', ''New-AzVMConfig'', ''New-AzVmssConfig'', ''Update-AzVM'', and ''Update-AzVmss''
* Added ''SecurityProfile'' to ''Get-AzVM'' and ''Get-AzVmss'' return object
* Added ''-InstanceView'' switch as optional parameter to ''Get-AzHostGroup''
* Added new cmdlet ''Invoke-AzVmPatchAssessment''

Az.DataFactory
* Added missing properties to PSPipelineRun class.

Az.HDInsight
* Supported creating cluster with encryption at host feature.

Az.KeyVault
* Added warning messages for planning to disable soft delete
* Added warning messages for planning to remove attribute SecretValueText

Az.Maintenance
* Added optional schedule related fields to ''New-AzMaintenanceConfiguration''
* Added new cmdlet for ''Get-AzMaintenancePublicConfiguration''

Az.ManagedServices
* Added breaking change warnings on cmdlets of managed services assignment and definition

Az.Monitor
* Extended the parameter set in ''Set-AzDiagnosticSetting'' for separation of Logs and Metrics enablement [#12482]
* Fixed bug for ''Add-AzMetricAlertRuleV2'' when getting metric alert from pipeline

Az.Resources
* Updated ''Get-AzPolicyAlias'' response to include information indicating whether the alias is modifiable by Azure Policy.
* Created new cmdlet ''Set-AzRoleAssignment''
* Added ''Get-AzDeploymentManagementGroupWhatIfResult'' for getting ARM template What-If results at management Group scope
* Added ''Get-AzTenantWhatIfResult'' new cmdlet for getting ARM template What-If results at tenant scope
* Overrode ''-WhatIf'' and ''-Confirm'' for ''New-AzManagementGroupDeployment'' and ''New-AzTenantDeployment'' to use ARM template What-If results
* Fixed the behaviors of ''-WhatIf'' and ''-Confirm'' for new deployment cmdlets so they comply with False and 
* Fixed serialization error for ''-TemplateObject'' and ''TemplateParameterObject'' [#1528] [#6292]
* Added breaking change attribute to ''Get-AzResourceGroupDeploymentOperation'' for the upcoming output type change

Az.SignalR
* Fixed ''Restart-AzSignalR'' and ''Update-AzSignalR'' help files errors
* Added cmdlets ''Update-AzSignalRNetworkAcl'', ''Set-AzSignalRUpstream''

Az.Storage
* Supported blob query acceleration
    -  ''Get-AzStorageBlobQueryResult''
    -  ''New-AzStorageBlobQueryConfig''
* Updated help file, added more description, and fixed typo
    -  ''Start-AzStorageBlobCopy''
    -  ''Get-AzDataLakeGen2Item''
* Fixed download blob fail when related sub directory not exist [#12592]
    -  ''Get-AzStorageBlobContent''
* Supported Set/Get/Remove Object Replication Policy on Storage accounts
    - ''New-AzStorageObjectReplicationPolicyRule''
    - ''Set-AzStorageObjectReplicationPolicy''
    - ''Get-AzStorageObjectReplicationPolicy''
    - ''Remove-AzStorageObjectReplicationPolicy''
* Supported enable/disable ChangeFeed on Blob Service of a Storage account
    - ''Update-AzStorageBlobServiceProperty''
'

        # Prerelease string of this module
        # Prerelease = ''

        # Flag to indicate whether the module requires explicit user acceptance for install/update/save
        # RequireLicenseAcceptance = $false

        # External dependent modules of this module
        # ExternalModuleDependencies = @()

    } # End of PSData hashtable

 } # End of PrivateData hashtable

# HelpInfo URI of this module
# HelpInfoURI = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}

