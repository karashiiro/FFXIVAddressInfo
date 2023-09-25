using System.Net;
using FFXIVAddressInfo.Shared;
using HtmlAgilityPack;

namespace FFXIVAddressInfo.DataExtractor;

public class XivUp
{
    public static async Task<IReadOnlyDictionary<string, DataCenterInfo>> GetDataCenterInfo()
    {
        using var http = new HttpClient();

        var xivUp = await http.GetStreamAsync(new Uri("https://is.xivup.com/adv"));

        var document = new HtmlDocument();
        document.Load(xivUp);

        return document.DocumentNode.SelectNodes("/html/body/div/div/div")
            .Where(node => node.HasClass("region"))
            .SelectMany(node => node.ChildNodes.Where(n => n.HasClass("dc")))
            .Select(node =>
            {
                var dcName = node.ChildNodes.FirstOrDefault(n => n.HasClass("title"))?.InnerText.Trim();
                return (Node: node, DataCenterName: dcName);
            })
            .Where(data => !string.IsNullOrEmpty(data.DataCenterName))
            .Select(data =>
            {
                var (node, dcName) = data;
                var realms = node.ChildNodes.Where(n => n.HasClass("realm")).Select(GetRealmAddress).ToList();
                var lobby = realms.First();

                // Parse the address to validate it, and then convert it back to a string for serialization later
                var rest = realms.Skip(1).Select(IPAddress.Parse).Select(addr => addr.ToString()).ToArray();
                return (DataCenterName: dcName!, Lobby: lobby, Servers: rest);
            })
            .Select(data => new DataCenterInfo(data.DataCenterName, data.Lobby, data.Servers))
            .ToDictionary(data => data.Name, data => data);
    }

    private static string GetRealmAddress(HtmlNode realm)
    {
        var realmName = realm.ChildNodes.First(node => node.HasClass("realmname"));
        var tooltip = realmName.ChildNodes.First(node => node.HasClass("tooltip"));
        return string.IsNullOrEmpty(tooltip.InnerText) ? realmName.InnerText.Trim() : tooltip.InnerText.Trim();
    }
}