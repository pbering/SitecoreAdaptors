# Framework initialization
Clear-Host

$scriptRoot = Split-Path (Resolve-Path $myInvocation.MyCommand.Path)
$env:PSModulePath = $env:PSModulePath + ";$scriptRoot\bld\Framework"

Import-Module DB -Force
Import-Module Config -Force
Import-Module IIS -Force
Import-Module Filesystem -Force

# Setup variables
$siteName = "SitecoreAdaptors"
$siteHostName = "sitecoreadaptors.com" 
$installPath = "D:\Install"
$iisroot = "D:\inetpub"
$licensePath = "$installPath\license.xml"
$sourcePath = "$installPath\Sitecore 6.6.0 rev. 130404.zip"
$sqlServerName = "(local)"

# Additional variables
$sourcePackageFilename = [System.IO.Path]::GetFileNameWithoutExtension($sourcePath)
$solutionFolder = "$iisroot\$siteName"
$dataFolder = "$solutionFolder\Data"
$websiteFolder = "$solutionFolder\Website"

# Main
function Main 
{
    $ErrorActionPreference = "stop"

    Expand-Package "$sourcePath" "$solutionFolder" "$sourcePackageFilename" -overwrite 1

    $server = New-Object ("Microsoft.SqlServer.Management.Smo.Server") $sqlServerName

    foreach ($db in @("Core", "Master", "Web"))
    {
        Add-Database $server "$siteName`_$db" "$solutionFolder\Databases\Sitecore.$db.mdf" "$solutionFolder\Databases\Sitecore.$db.ldf"
        Set-ConnectionString "$websiteFolder\App_Config\ConnectionStrings.config" "$db".ToLowerInvariant() "Trusted_Connection=Yes;Data Source=$sqlServerName;Database=$siteName`_$db"
    }

    Set-ConfigAttribute "$websiteFolder\Web.config" "sitecore/sc.variable[@name='dataFolder']" "value" $dataFolder   
    Copy-Item $licensePath $dataFolder
    New-AppPool $siteName "v4.0"
    New-Site $siteName $siteHostName $websiteFolder
    Edit-HostFile "127.0.0.1" $siteHostName
}

# Run Main
Main