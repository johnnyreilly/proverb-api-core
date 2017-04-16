Param($projectFile, $buildNum)

# write-host "project file: $projectFile"
# write-host "build num: $buildNum"

$content = [IO.File]::ReadAllText($projectFile)

$regex = new-object System.Text.RegularExpressions.Regex ('(<Version>)([\d]+.[\d]+.[\d]+)(.[\d]+)(<\/Version>)', 
         [System.Text.RegularExpressions.RegexOptions]::MultiLine)

$version = $null
$match = $regex.Match($content)
if($match.Success) {
    # from "<Version>1.0.0.0</Version>" this will extract "1.0.0"
    $version = $match.groups[2].value
}

# suffix build number onto $version. eg "1.0.0.15"
$version = "$version.$buildNum"

# update "<Version>1.0.0.0</Version>" to "<Version>$version</Version>"
$content = $regex.Replace($content, '${1}' + $version + '${4}')

# update csproj file
[IO.File]::WriteAllText($projectFile, $content)

# update AppVeyor build
Update-AppveyorBuild -Version $version