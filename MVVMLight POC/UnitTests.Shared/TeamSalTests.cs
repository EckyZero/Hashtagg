using System.Collections.Generic;
using Demo.Shared;
using Demo.Shared.Models;
using NUnit.Framework;

namespace UnitTests.Shared
{
    [TestFixture]
    public class TeamSalTests
    {
        [TestFixtureSetUp]
        public void SetupTests()
        {
            App.Initialize();
        }

        [Test]
        public async void TestGet()
        {
            // Arrange/Act
            List<Team> teams = await TeamSal.GetTeamsWithSeasonId(351);

            // Assert
            Assert.IsNotNull(teams);
            Assert.IsTrue(teams.Count > 0);
        }
    }
}