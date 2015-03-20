using System.Collections.Generic;
using System.Linq;
using Demo.Shared;
using Demo.Shared.Models;
using NUnit.Framework;

namespace UnitTests.Shared
{
    [TestFixture]
    public class SeasonBLTests
    {
        [SetUp]
        public void BeforeEach()
        {
            SeasonDal.DeleteAll();
        }

        [TestFixtureSetUp]
        public void SetupTests()
        {
            App.Initialize();
        }

        [Test]
        public async void TestGet()
        {
            // Arrange
            List<Season> seasons = await SeasonBL.GetAll(false);
            Season season1 = seasons.FirstOrDefault();

            // Act
            List<Season> databaseSeasons = SeasonDal.GetAll();
            Season databaseSeason1 = databaseSeasons.FirstOrDefault(season => season.Id == season1.Id);

            // Assert
            Assert.IsNotNull(databaseSeason1);
            Assert.AreEqual(databaseSeason1.Id, season1.Id);
        }
    }
}