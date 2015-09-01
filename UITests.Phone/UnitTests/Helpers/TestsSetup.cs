using NUnit.Framework;

namespace Tests
{
    [SetUpFixture]
    public class TestsSetup
    {
        // This has to be called before any tests (including those in the UnitTests.Shared project
        // The only way this can be guaranteed is if this project (CompassMobile.Shared.Tests) contains at least one test itself
        [SetUp]
        public void RunBeforeAnyTests()
        {
            App.Initialize ();
        }
    }
}

