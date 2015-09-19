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
            base.BeforeEach(repl: false, platform: AppUtils.iOS);
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
            catch (Exception)
            {
                App.Screenshot("Failed to Sign in to Facebook");
                throw;
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
            catch (Exception)
            {
                App.Screenshot("Failed to Sign in to Twitter");
                throw;
            }
        }

        private void SignIn ()
        {
            try
            {
                App.Screenshot("Signing in with all accounts authorized");
                App.WaitForElement(c => c.Marked("Let's go!"));
                App.Tap(c => c.Marked("Let's go!"));

                // Dismiss alert if exists
                Thread.Sleep(3000);
                if(App.Query(c => c.Marked("OK")).Length > 0)
                {
                    App.Screenshot("Dismiss alert");
                    App.Tap(c => c.Marked("OK"));
                }   
            }
            catch (Exception)
            {
                App.Screenshot("Failed to Sign in");
                throw;
            }
        }

        private void OpenMenu ()
        {
            try
            {
                App.Screenshot("Opening Menu");
                App.WaitForElement(c => c.Marked("Menu Button"));
                App.Tap(c => c.Marked("Menu Button"));

                App.Screenshot("Tapping Sign out");
                App.WaitForElement(c => c.Marked("Sign out"));
                App.Tap(c => c.Marked("Sign out"));

                App.Screenshot("Tapping Done");
                App.WaitForElement(c => c.Marked("Done!"));
                App.Tap(c => c.Marked("Done!"));
            }
            catch (Exception)
            {
                App.Screenshot("Failed to open menu");
                throw; 
            }
        }

        private void CloseMenu ()
        {
            try
            {
                App.Screenshot("Closing menu");
                App.WaitForElement(c => c.Marked("Menu Button"));
                App.Tap(c => c.Marked("Menu Button"));
            }
            catch (Exception)
            {
                App.Screenshot("Failed to close menu");
                throw;
            }
        }

        private void LikeUnlikePost ()
        {
            try
            {
                App.Screenshot("Like Post");

                if(App.Query(c => c.Property("text").Like("*Like (*")).Length < 0)
                {
                    App.ScrollDown();   
                }
                App.Tap(c => c.Property("text").Like("*Like (*"));

                App.Screenshot("Unlike Post");
                App.WaitForElement(c => c.Property("text").Like("*Liked (*"));
                App.Tap(c => c.Property("text").Like("*Liked (*"));
            }
            catch (Exception)
            {
                App.Screenshot("Failed to like/unlike post");
                throw; 
            }
        }

        private void NewPost ()
        {
            try
            {
                App.Screenshot("Opening New Post");
                App.WaitForElement(c => c.Marked("newPostButton"));
                App.Tap(c => c.Marked("newPostButton"));

                App.Screenshot("Entering text");
                App.WaitForElement("New Post");

                Thread.Sleep(2000);
                App.EnterText("This is a test post. Please ignore.");

                App.Screenshot("Select social accounts");
                App.Tap("socialTwitterUnselected");

                App.Screenshot("Post");
                App.Tap("Post");

                App.Screenshot("Dismiss alert");
                App.WaitForElement("OK");
                App.Tap("OK");
            }
            catch (Exception)
            {
                App.Screenshot("Failed to create post");
                throw; 
            }
        }
    }
}

