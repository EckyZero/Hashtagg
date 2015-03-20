using System;
using System.Linq;
using Demo.UITests.Helpers;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Demo.UITests
{
    [TestFixture]
    public class DemoTests
    {
        [SetUp]
        public void BeforeEach()
        {
            AppUtils = new AppUtils(false);

            _app = AppUtils.App;
            _queries = AppUtils.Queries;
        }

        private AppUtils AppUtils;
        private IApp _app;
        private IScreenQueries _queries;

        [Test]
        [ExpectedException]
        public void TestException()
        {
            try
            {
                _app.EnterText(_queries.LoginField, "testuser@example.com");
                _app.Tap(_queries.LoginButton);
                _app.WaitForElement(_queries.SeasonsView, "Seasons Did Not Appear", TimeSpan.FromSeconds(5));
                _app.WaitForElement(_queries.TestIncorrectSeasonView, "This is supposed to fail",
                    TimeSpan.FromSeconds(5));

                Assert.Fail("An exception should have been thrown before reaching here");
            }
            catch (Exception e)
            {
                AppUtils.Screenshot("test-exception-success.png", "Couldn't find correct season");

                Assert.Pass("The correct exception was thrown and caught successfully");
            }
        }

        [Test]
        public void TestInvalidEmail()
        {
            try
            {
                _app.EnterText(_queries.LoginField, "");
                _app.Tap(_queries.LoginButton);

                AppResult[] results = _app.WaitForElement(_queries.LoginErrorMessageView, "Alert Did Not Appear",
                    TimeSpan.FromSeconds(5));

                Assert.IsTrue(results.Any(), "The error message was not displayed on the screen");
            }
            catch (Exception e)
            {
                AppUtils.Screenshot("test-invalid-email-failure.png", "Test Invalid Email Failure");

                throw e;
            }
        }

        [Test]
        public void TestNavigation()
        {
            try
            {
                _app.EnterText(_queries.LoginField, "testuser@example.com");
                _app.Tap(_queries.LoginButton);
                _app.WaitForElement(_queries.SeasonsView, "Seasons Did Not Appear", TimeSpan.FromSeconds(5));
                _app.WaitForElement(_queries.TestSeasonView, "Test Seasons Did Not Appear", TimeSpan.FromSeconds(5));

                _app.Tap(_queries.TestSeasonView);

                _app.WaitForElement(_queries.TeamsView, "Teams Did Not Appear", TimeSpan.FromSeconds(5));

                AppResult[] results = _app.WaitForElement(_queries.TestTeamView, "Test Team Did Not Appear",
                    TimeSpan.FromSeconds(5));

                Assert.IsTrue(results.Any(), "Teams Did Not Appear");
            }
            catch (Exception e)
            {
                AppUtils.Screenshot("test-navigation-failure.png", "Test Navigation Failure");

                throw e;
            }
        }

        [Test]
        public void TestValidEmail()
        {
            try
            {
                _app.EnterText(_queries.LoginField, "testuser@example.com");
                _app.Tap(_queries.LoginButton);

                _app.WaitForElement(_queries.SeasonsView, "Seasons Did Not Appear", TimeSpan.FromSeconds(5));

                AppResult[] results = _app.WaitForElement(_queries.TestSeasonView, "Test Season Did Not Appear",
                    TimeSpan.FromSeconds(5));

                Assert.IsTrue(results.Any(), "seasons view did not appear correctly");
            }
            catch (Exception e)
            {
                AppUtils.Screenshot("test-valid-email-failure.png", "Test Valid Email Failure");

                throw e;
            }
        }
    }
}