Param($projectFile, $buildNum, $buildVersion)

write-host "project file: $projectFile"
write-host "build num: $buildNum"
write-host "build version: $buildVersion"

$regex = new-object System.Text.RegularExpressions.Regex ('(<Version>)([\d]+.[\d]+.[\d]+)(.[\d]+)(<\/Version>)', 
         [System.Text.RegularExpressions.RegexOptions]::MultiLine)

$content = [IO.File]::ReadAllText($projectFile)

$version = $null
$match = $regex.Match($content)
if($match.Success) {
    $version = $match.groups[2].value
}

# new version
$version = "$version.$buildNum"

# update assembly info
$content = $regex.Replace($content, '${1}' + $version + '${4}')

write-host $content
#[IO.File]::WriteAllText($projectFile, $content)

# update AppVeyor build
#Update-AppveyorBuild -Version $version