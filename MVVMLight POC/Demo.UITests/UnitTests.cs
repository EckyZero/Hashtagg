using System;
using System.Linq;
using Demo.UITests.Helpers;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Demo.UITests
{
    [TestFixture]
    public class iOSUnitTests
    {
        [SetUp]
        public void BeforeEach()
        {
            AppUtils = new AppUtils(true);

            _app = AppUtils.App;
            _queries = AppUtils.Queries;
        }

        private AppUtils AppUtils;
        private IApp _app;
        private IScreenQueries _queries;

//		[Test]
//		public void SetupTests ()
//		{
//			_app.Repl ();
//		}

        [Test]
        public void TestUnitTests()
        {
            try
            {
                _app.WaitForElement(_queries.StartUnitTestsView);
                _app.Tap(_queries.StartUnitTestsView);

                AppResult[] results = _app.WaitForElement(_queries.UnitTestsSuccessView, "Unit Tests Failed",
                    TimeSpan.FromSeconds(5));

                Assert.IsTrue(results.Any(), "Unit Tests Failed");
            }
            catch (Exception e)
            {
                AppUtils.Screenshot("test-unit-tests-failure.png", "Testing Unit Tests");

                throw e;
            }
        }
    }
}