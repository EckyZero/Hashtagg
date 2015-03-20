using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Shared.Exceptions;
using Demo.Shared.Models;
using Moq;
using Newtonsoft.Json;

namespace Demo.Shared.Tests.Mocks
{
    public static class SharedSalMock
    {
        public static IServiceAccessLayer GetMock()
        {
            var mock = new Mock<IServiceAccessLayer>();


            mock.Setup(sal => sal.Get<List<Season>>(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()))
                .Returns(
                    Task<List<Season>>.Factory.StartNew(() =>
                    {
                        var seasons = new List<Season>
                        {
                            new Season {Id = 1, League = "Premier League", Caption = "pl"},
                            new Season {Id = 2, League = "Bundesliga", Caption = "bl1"},
                            new Season {Id = 3, League = "Serie A", Caption = "sa"}
                        };
                        string json = JsonConvert.SerializeObject(seasons);

                        var obj = JsonConvert.DeserializeObject<List<Season>>(json);
                        return obj;
                    })
                );

            mock.Setup(sal => sal.Get<List<Season>>(It.IsAny<Dictionary<string, string>>(), It.Is<string>(
                s => s.Equals("Crash"))
                )).Throws(new SalException("Getting all seasons failed."));


            mock.Setup(sal => sal.Get<List<Team>>(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>())).Returns(
                Task<List<Team>>.Factory.StartNew(() =>
                {
                    var teams = new List<Team>
                    {
                        new Team {Id = 1, Name = "Everton", SeasonId = 1},
                        new Team {Id = 2, Name = "Manchester United", SeasonId = 1},
                        new Team {Id = 3, Name = "Chelsea", SeasonId = 1}
                    };

                    string json = JsonConvert.SerializeObject(teams);

                    var obj = JsonConvert.DeserializeObject<List<Team>>(json);
                    return obj;
                })
                );


            mock.Setup(sal => sal.Get<List<Team>>(It.IsAny<Dictionary<string, string>>(), It.Is<string>(
                s => s.Equals("Crash"))
                )).Throws(new SalException("Getting teams for season failed."));

            return mock.Object;
        }
    }
}