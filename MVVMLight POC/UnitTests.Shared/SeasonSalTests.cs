using System.Collections.Generic;
using System.Linq;
using Demo.Shared;
using Demo.Shared.Models;
using NUnit.Framework;

namespace UnitTests.Shared
{
    [TestFixture]
    public class SeasonSalTests
    {
        [TestFixtureSetUp]
        public void SetupTests()
        {
            App.Initialize();
        }

        [Test]
        public async void TestGet()
        {
            // Arrange
            List<Season> seasons = await SeasonSal.GetAll();

            // Act
            Season season1 = seasons.FirstOrDefault();

            // Assert
            Assert.IsNotNull(season1);
            Assert.AreNotEqual(string.Empty, season1.League);
        }
    }
}