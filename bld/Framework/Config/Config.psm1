<#
	.DESCRIPTION
		Set Config Attribute
#>
function Set-ConfigAttribute([string]$configPath, [string] $xpath, [string] $attribute, [string] $value)
{
    Write-Output "Setting attribute $xpath in $configPath to $value."
	
	$config = [xml](get-content $configPath)
	$config.configuration.SelectSingleNode($xpath).SetAttribute($attribute, $value)	
	$config.Save($configPath)
}

<#
	.DESCRIPTION
		Set Connection String
#>
function Set-ConnectionString([string]$configPath, [string] $connectionStringName, [string] $value)
{
    Write-Output "Setting connection string $connectionStringName in $configPath to $value."
	
	$config = [xml](get-content $configPath)
	$config.SelectSingleNode("connectionStrings/add[@name='$connectionStringName']").SetAttribute("connectionString", $value)	
	$config.Save($configPath)
}

<#
	.DESCRIPTION
		Set Sitecore Setting
#>
function Set-SitecoreSetting([string]$configPath, [string] $name, [string] $value)
{
    Write-Output "Setting Sitecore setting $name"
	
    $xpath = "settings/setting[@name='" + $name + "']"   
	$config = [xml](get-content $configPath)
	$config.configuration.sitecore.SelectSingleNode($xpath).SetAttribute("value", $value)	
	$config.Save($configPath)
}

Export-ModuleMember -function Set-ConfigAttribute
Export-ModuleMember -function Set-ConnectionString
Export-ModuleMember -function Set-SitecoreSetting