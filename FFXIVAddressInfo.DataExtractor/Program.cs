using System.Text.Json;
using FFXIVAddressInfo.DataExtractor;

var outFile = args.Length == 0 ? "./data.json" : args[0];

Console.WriteLine("Extracting server data...");

var data = await XivUp.GetDataCenterInfo();

Console.WriteLine("Downloaded all data, saving...");

var json = JsonSerializer.Serialize(data);

await File.WriteAllTextAsync(outFile, json);

Console.WriteLine("Done!");