using System.Net;

namespace FFXIVAddressInfo;

public interface IDataCenterAddresses
{
    string LobbyServer { get; }

    IReadOnlyCollection<IPAddress> Addresses { get; }
}