using NUnit.Framework;

namespace Demo.Shared.Tests
{
    [SetUpFixture]
    public class TestsSetup
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            UnitTests.Shared.App.InitializeAction = App.Initialize;
        }
    }
}