using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.NUnit.UI;
using MonoTouch.UIKit;
using Xamarin;

namespace Demo.iOS.Tests
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("UnitTestAppDelegate")]
    public class UnitTestAppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        private TouchRunner runner;
        private UIWindow window;

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            window = new UIWindow(UIScreen.MainScreen.Bounds);
            runner = new TouchRunner(window);


            string location = Assembly.GetExecutingAssembly().Location;
            // register every tests included in the main application/assembly
            runner.Add(Assembly.GetExecutingAssembly());

            window.RootViewController = new UINavigationController(runner.GetViewController());

            // make the window visible
            window.MakeKeyAndVisible();

			UnitTests.Shared.App.InitializeAction = App.Initialize;


#if DEBUG
            // This is not necessary for Android applications
            Calabash.Start();
#endif

            return true;
        }
    }
}