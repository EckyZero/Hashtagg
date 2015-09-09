using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests.Phone
{
    [TestFixture]
    public class MainTests : BaseTest
    {
        [SetUp]
        public void BeforeEach()
        {
            // Must be called to initialize the Utils, App, and Queries Properties
            base.BeforeEach(repl: false, platform: AppUtils.iOS);
        }

        [Test]
        public void AppLaunches()
        {
            SignInToFacebook();
            SignInToTwitter();

            App.Screenshot("Signing in with all accounts authorized");
            App.Tap(c => c.Marked("Let's go!"));

            // Dismiss alert if exists
            if(App.Query(c => c.Marked("Ok")).Length > 0)
            {
                App.Screenshot("Dismiss alert");
                App.Tap(c => c.Marked("Ok"));
            }
        }

        private void SignInToFacebook ()
        {
            // Sign in to Facebook
            try
            {
                App.Screenshot("Presenting Facebook Login");
                App.Tap(c => c.Marked("Facebook"));

                App.Screenshot("Entering Facebook Username");
                App.Tap(c => c.WebView().Css("[name=\"email\"]"));
                App.EnterText("hashtaggapp@gmail.com");

                App.Screenshot("Entering Facebook Password");
                App.Tap(c => c.WebView().Css("[name=\"pass\"]")); 
                App.EnterText("Hashtaggapp123$");

                App.Screenshot("Authorizing Facebook");
                App.Tap(c => c.WebView().Css("[name=\"login\"]"));    
            }
            catch (Exception e)
            {
                App.Screenshot("Failed to Sign in to Facebook");
            }
        }

        private void SignInToTwitter ()
        {
            // Sign in to Twitter
            try
            {
                App.Screenshot("Presenting Twitter Login");
                App.Tap(c => c.Marked("Twitter"));

                App.Screenshot("Entering Twitter Username");
                App.Tap(c => c.WebView().Css("[name=\"session[username_or_email]\"]"));
                App.EnterText("HashtaggApp");

                App.Screenshot("Entering Twitter Password");
                App.Tap(c => c.WebView().Css("[name=\"session[password]\"]")); 
                App.EnterText("Hashtagg123$");

                App.Screenshot("Authorizing Twitter");
                App.Tap(c => c.WebView().Css("[id=\\\"allow\\\"]")); 
            }
            catch (Exception e)
            {
                App.Screenshot("Failed to Sign in to Twitter");
            }
        }
    }
}

