using Microsoft.Build.Construction;

var inFile = args.Length == 0 ? "../../../../FFXIVAddressInfo/FFXIVAddressInfo.csproj" : args[0];

var project = ProjectRootElement.Open(inFile);
ArgumentNullException.ThrowIfNull(project);

foreach (var property in project.Properties)
{
    if (property.Name != "Version")
    {
        continue;
    }

    var version = Version.Parse(property.Value);
    Console.WriteLine($"Bumping version from {version} to {version.Major}.{version.Minor}.{version.Build + 1}");

    property.Value = $"{version.Major}.{version.Minor}.{version.Build + 1}";
}

project.Save();