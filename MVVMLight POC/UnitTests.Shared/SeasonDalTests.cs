using System.Collections.Generic;
using Demo.Shared;
using Demo.Shared.Models;
using NUnit.Framework;

namespace UnitTests.Shared
{
    [TestFixture]
    public class SeasonDalTests
    {
        [SetUp]
        public void BeforeEach()
        {
            SeasonDal.DeleteAll();

            TestSeason = new Season {Id = 1234, Caption = "pl", League = "Premier League"};
        }

        private Season TestSeason;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            App.Initialize();
        }

        [Test]
        public void TestCreate()
        {
            // Arrange
            SeasonDal.Refresh(new List<Season> {TestSeason});

            // Act
            List<Season> newSeasons = SeasonDal.GetAll();
            Season season = newSeasons[0];

            // Assert
            Assert.AreEqual(1, newSeasons.Count);
            Assert.AreEqual(season.Id, TestSeason.Id);
        }

        [Test]
        public void TestDelete()
        {
            // Arrange
            var seasons = new List<Season> {TestSeason};

            // Act
            SeasonDal.Refresh(seasons);

            List<Season> newSeasons = SeasonDal.GetAll();

            // Assert
            Assert.AreEqual(1, newSeasons.Count);

            // Arrange
            SeasonDal.DeleteAll();

            // Act
            List<Season> newestSeasons = SeasonDal.GetAll();

            // Assert
            Assert.AreEqual(0, newestSeasons.Count);
        }

        [Test]
        public void TestRead()
        {
            // Arrange
            SeasonDal.Refresh(new List<Season> {TestSeason});

            // Act
            List<Season> newSeasons = SeasonDal.GetAll();
            Season season = newSeasons[0];

            // Assert
            Assert.AreEqual(season.Id, TestSeason.Id);
        }


        [Test]
        public void TestUpdate()
        {
            // Arrange
            SeasonDal.Refresh(new List<Season> {TestSeason});

            // Act
            List<Season> newSeasons = SeasonDal.GetAll();
            Season season = newSeasons[0];

            // Assert
            Assert.IsNull(season.Year);

            // Arranage
            season.Year = "2014";

            // Act
            SeasonDal.Refresh(new List<Season> {season});

            Season newestSeason = SeasonDal.GetAll()[0];

            // Assert
            Assert.IsNotNull(newestSeason.Year);
            Assert.AreEqual(newestSeason.Year, "2014");
        }
    }
}