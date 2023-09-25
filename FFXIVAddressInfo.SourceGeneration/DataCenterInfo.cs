using System.Net;

namespace FFXIVAddressInfo.SourceGeneration;

internal sealed record DataCenterInfo(string Name, string LobbyServer, IReadOnlyList<IPAddress> Addresses);