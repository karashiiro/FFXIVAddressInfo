# FFXIVAddressInfo
A C# package with FFXIV data center addresses.

## Installation
`Install-Package FFXIVAddressInfo` or other methods as described [here](https://www.nuget.org/packages/FFXIVAddressInfo/).

Consider setting your installed version to `1.0.*` to get automatic updates as server addresses change.

## Example

```csharp
Console.WriteLine(DataCenters.Aether.LobbyServer);

foreach (var address of DataCenters.Aether.Addresses)
{
    Console.WriteLine(address.ToString());
}
```