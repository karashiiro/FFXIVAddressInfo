using System.Net;

namespace FFXIVAddressInfo;

internal sealed record DataCenterInfo
    (string LobbyServer, IReadOnlyCollection<IPAddress> Addresses) : IDataCenterAddresses;