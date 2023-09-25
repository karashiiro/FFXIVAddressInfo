using System.Text.RegularExpressions;

namespace FFXIVAddressInfo.Tests;

public partial class DataCentersTests
{
    [Fact]
    public void DataCenters_Are_Valid()
    {
        var currentDataCenters = new[]
        {
            DataCenters.Aether, DataCenters.Primal, DataCenters.Crystal, DataCenters.Dynamis,
            DataCenters.Chaos, DataCenters.Crystal, DataCenters.Crystal, DataCenters.Materia,
            DataCenters.Elemental, DataCenters.Gaia, DataCenters.Mana, DataCenters.Meteor,
        };

        Assert.All(currentDataCenters, dc =>
        {
            Assert.Matches(LobbyServerRegex(), dc.LobbyServer);

            Assert.NotNull(dc.Addresses);
            Assert.NotEmpty(dc.Addresses);

            var ipCommonBytes = dc.Addresses.First().GetAddressBytes().Take(3).Select(b => Convert.ToString(b));
            var ipStart = string.Join('.', ipCommonBytes);
            Assert.All(dc.Addresses, addr =>
            {
                Assert.DoesNotMatch(LobbyServerRegex(), addr.ToString());
                Assert.StartsWith(ipStart, addr.ToString());
            });
        });
    }

    [GeneratedRegex(@"neolobby\d{2}.ffxiv\.com")]
    private static partial Regex LobbyServerRegex();
}