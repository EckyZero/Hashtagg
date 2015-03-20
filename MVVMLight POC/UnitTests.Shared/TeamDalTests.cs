using System.Collections.Generic;
using Demo.Shared;
using Demo.Shared.Models;
using NUnit.Framework;

namespace UnitTests.Shared
{
    [TestFixture]
    public class TeamDalTests
    {
        [SetUp]
        public void BeforeEach()
        {
            TeamDal.DeleteTeamsWithSeasonId(TestSeasonId);

            testTeam = new Team {Id = 1234, Name = "Test", SeasonId = TestSeasonId};
        }

        private const int TestSeasonId = 1;
        private Team testTeam;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            App.Initialize();
        }

        [Test]
        public void TestCreate()
        {
            // Arrange
            var teams = new List<Team> {testTeam};

            // Act
            TeamDal.Refresh(TestSeasonId, teams);

            List<Team> newTeams = TeamDal.GetTeamsWithSeasonId(TestSeasonId);
            Team team = newTeams[0];

            // Assert
            Assert.AreEqual(1, newTeams.Count);
            Assert.AreEqual(team.Id, testTeam.Id);
        }

        [Test]
        public void TestDelete()
        {
            // Arrange
            var teams = new List<Team> {testTeam};

            // Act
            TeamDal.Refresh(TestSeasonId, teams);

            List<Team> newTeams = TeamDal.GetTeamsWithSeasonId(TestSeasonId);

            TeamDal.DeleteTeamsWithSeasonId(TestSeasonId);

            List<Team> newestTeams = TeamDal.GetTeamsWithSeasonId(TestSeasonId);

            // Assert
            Assert.AreEqual(0, newestTeams.Count);
        }

        [Test]
        public void TestRead()
        {
            // Arrange
            var teams = new List<Team> {testTeam};

            // Act
            TeamDal.Refresh(TestSeasonId, teams);

            List<Team> newTeams = TeamDal.GetTeamsWithSeasonId(TestSeasonId);
            Team team = newTeams[0];

            // Assert
            Assert.NotNull(team.Id);
            Assert.AreEqual(team.Id, testTeam.Id);
        }


        [Test]
        public void TestUpdate()
        {
            // Arrange
            var teams = new List<Team> {testTeam};
            string testString = "Test Editing the Short Name";

            // Act
            TeamDal.Refresh(TestSeasonId, teams);

            Team team = TeamDal.GetTeamsWithSeasonId(TestSeasonId)[0];

            // Assert
            Assert.IsNull(team.ShortName);

            // Arrange
            team.ShortName = testString;

            // Act
            TeamDal.Refresh(TestSeasonId, new List<Team> {team});

            List<Team> newTeams = TeamDal.GetTeamsWithSeasonId(TestSeasonId);
            Team newTeam = newTeams[0];

            // Assert
            Assert.AreEqual(1, newTeams.Count);
            Assert.AreEqual(newTeam.ShortName, testString);
        }
    }
}