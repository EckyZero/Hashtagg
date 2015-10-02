using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Threading;

namespace UITests.Phone
{
    [TestFixture]
    public class MainTests : BaseTest
    {
        [SetUp]
        public void BeforeEach()
        {
            // Must be called to initialize the Utils, App, and Queries Properties
            base.BeforeEach(repl: true, platform: AppUtils.Android);
        }

        [Test]
        public void AppLaunches()
        {
            SignInToTwitter();

            SignIn();

            LikeUnlikePost();

            OpenMenu();

            SignInToFacebook();

            CloseMenu();

            NewPost();
        }

        private void SignInToFacebook ()
        {
            // Sign in to Facebook
            App.Tap(c => c.Marked("Facebook"));
            App.Screenshot("Presenting Facebook Login");

            App.Tap(c => c.WebView().Css("[name=\"email\"]"));
            App.EnterText("hashtaggapp@gmail.com");
            App.Screenshot("Entering Facebook Username");

            App.Tap(c => c.WebView().Css("[name=\"pass\"]")); 
            App.EnterText("Hashtaggapp123$");
            App.Screenshot("Entering Facebook Password");

            App.Tap(c => c.WebView().Css("[name=\"login\"]"));    
            App.Screenshot("Authorizing Facebook");
        }

        private void SignInToTwitter ()
        {
            // Sign in to Twitter
            App.Tap(c => c.Marked("Twitter"));
            App.Screenshot("Presenting Twitter Login");

            App.Tap(c => c.WebView().Css("[name=\"session[username_or_email]\"]"));
            App.EnterText("HashtaggApp");
            App.Screenshot("Entering Twitter Username");

            App.Tap(c => c.WebView().Css("[name=\"session[password]\"]")); 
            App.EnterText("Hashtagg123$");
            App.Screenshot("Entering Twitter Password");

            App.DismissKeyboard();
            App.Tap(c => c.WebView().Css("[id=\\\"allow\\\"]")); 
            App.Screenshot("Authorizing Twitter");
        }

        private void SignIn ()
        {
            App.WaitForElement(c => c.Marked("Let's go!"));
            App.Tap(c => c.Marked("Let's go!"));
            App.Screenshot("Signing in with all accounts authorized");

            // Dismiss alert if exists
            Thread.Sleep(3000);
            if(App.Query(c => c.Marked("OK")).Length > 0)
            {
                App.Tap(c => c.Marked("OK"));
            }   
        }

        private void OpenMenu ()
        {
            App.WaitForElement(c => c.Marked("Menu Button"));
            App.Tap(c => c.Marked("Menu Button"));
            App.Screenshot("Opening Menu");

            App.WaitForElement(c => c.Marked("Sign out"));
            App.Tap(c => c.Marked("Sign out"));
            App.Screenshot("Tapping Sign out");

            App.WaitForElement(c => c.Marked("Done!"));
            App.Tap(c => c.Marked("Done!"));
            App.Screenshot("Tapping Done");
        }

        private void CloseMenu ()
        {
            App.WaitForElement(c => c.Marked("Menu Button"));
            App.Tap(c => c.Marked("Menu Button"));
            App.Screenshot("Closing menu");
        }

        private void LikeUnlikePost ()
        {
            if(App.Query(c => c.Property("text").Like("*Like (*")).Length < 0)
            {
                App.ScrollDown();   
            }
            App.Tap(c => c.Property("text").Like("*Like (*"));
            App.Screenshot("Like Post");

            App.WaitForElement(c => c.Property("text").Like("*Liked (*"));
            App.Tap(c => c.Property("text").Like("*Liked (*"));
            App.Screenshot("Unlike Post");
        }

        private void NewPost ()
        {
            App.WaitForElement(c => c.Marked("newPostButton"));
            App.Tap(c => c.Marked("newPostButton"));
            App.Screenshot("Opening New Post");

            App.WaitForElement("New Post");

            Thread.Sleep(2000);
            App.EnterText("This is a test post. Please ignore.");
            App.Screenshot("Entering text");

            App.Tap("socialTwitterUnselected");
            App.Screenshot("Select Twitter account");

            App.Tap("socialFacebookUnselected");
            App.Screenshot("Select Facebook account");

            App.Tap("Post");
            App.Screenshot("Post");

            App.WaitForElement("OK");
            App.Tap("OK");
            App.Screenshot("Dismiss alert");
        }
    }
}

