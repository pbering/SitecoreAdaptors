<#
	.DESCRIPTION
		Extract package to the webroot
#>
function Expand-Package([string]$downloadLocation, [string]$webroot, [string]$extractedFolder, [string]$overwrite)
{
    if (($overwrite -eq "1") -and (Test-Path $webroot))
    {
        Write-Output "Webroot already exists, removing..."
   		Remove-Item $webroot -Recurse -Force
        Start-sleep -milliseconds 2000
    }

    if(Test-Path $webroot)
	{
		Write-Output "Web root already exists, skipping."

		return
	}

	# Create a web root directory and unzip archive
	New-Item $webroot -type directory -Force | out-null

    Expand-Zip $downloadLocation $webroot

    if (Test-Path "$webroot\$extractedFolder") {
    	move "$webroot\$extractedFolder\*" "$webroot" -Verbose
    	rm "$webroot\$extractedFolder" -Verbose
    }
}

<#
	.DESCRIPTION
		Extract archive
#>
function Expand-Zip ([string]$file, [string]$targetFolder)
{
    if (Test-Path $file)
	{
        Write-Output "Expanding package - $file"
		
    	$shell_app = new-object -com shell.application 
    	$zip_file = $shell_app.namespace($file) 
    	$destination = $shell_app.namespace($targetFolder)
    	$destination.CopyHere($zip_file.items(), 0x14)
		
    	Write-Output "Package expanded."	
	}
    else 
    {
        Write-Output "Package $file not found!"	
    }	
}

function Edit-HostFile ([string]$IPAddress, [string]$computer)          
{                              
	$file = Join-Path -Path $($env:windir) -ChildPath "system32\drivers\etc\hosts"            
	
    if (-not (Test-Path -Path $file)){            
		Throw "Hosts file not found!"            
	}            
	
    $data = Get-Content -Path $file             
	$data += "$IPAddress  $computer"            
	
    Set-Content -Value $data -Path $file -Force -Encoding ASCII     
	
	Write-Output "Hosts file updated."
}
 
Export-ModuleMember -function Expand-Package
Export-ModuleMember -function Expand-Zip
Export-ModuleMember -function Edit-HostFile