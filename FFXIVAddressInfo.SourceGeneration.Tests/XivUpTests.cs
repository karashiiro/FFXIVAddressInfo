using System.Text.RegularExpressions;

namespace FFXIVAddressInfo.SourceGeneration.Tests;

public partial class XivUpTests
{
    [Fact]
    public async Task GetDataCenterInfo_Returns_Addresses()
    {
        var dcInfo = await XivUp.GetDataCenterInfo();
        Assert.NotNull(dcInfo);
        Assert.NotEmpty(dcInfo);

        Assert.All(dcInfo.Values, info =>
        {
            Assert.NotEmpty(info.Name);

            Assert.Matches(LobbyServerRegex(), info.LobbyServer);

            Assert.NotNull(info.Addresses);
            Assert.NotEmpty(info.Addresses);

            var ipCommonBytes = info.Addresses[0].GetAddressBytes().Take(3).Select(b => Convert.ToString(b));
            var ipStart = string.Join('.', ipCommonBytes);
            Assert.All(info.Addresses, addr =>
            {
                Assert.DoesNotMatch(LobbyServerRegex(), addr.ToString());
                Assert.StartsWith(ipStart, addr.ToString());
            });
        });
    }

    [GeneratedRegex(@"neolobby\d{2}.ffxiv\.com")]
    private static partial Regex LobbyServerRegex();
}