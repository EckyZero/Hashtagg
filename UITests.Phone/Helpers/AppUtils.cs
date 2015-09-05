using System;
using System.Reflection;
using System.IO;
using Xamarin.UITest;

namespace UITests.Phone
{
	public class AppUtils
	{
		public static readonly string iOS = "ios";
		public static readonly string Android = "android";
		public static readonly string Key = "fdd8ffdd0f98818b7dbf418bbe6699ee";
			
		public readonly string Platform;
		public ScreenQueries Queries { get; set; }
		public IApp App { get; set; }

		public AppUtils(bool repl, string platform)
		{

			string path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			var info = new FileInfo(path);
			string directory = info.Directory.Parent.Parent.Parent.FullName;
			TestPlatform environment = TestEnvironment.Platform;
			string pathToApp;

			if (environment == TestPlatform.TestCloudiOS)
			{
				Queries = new iOSQueries ();
				App = ConfigureApp.iOS.ApiKey(Key).StartApp ();
			}
			else if (TestEnvironment.Platform.Equals(TestPlatform.TestCloudAndroid))
			{
				Queries = new AndroidQueries ();
				App = ConfigureApp.Android.ApiKey(Key).StartApp();
			}
			else if (environment == TestPlatform.Local)
			{
				Platform = platform; 

			    if (Platform == iOS)
			    {
                    pathToApp = Path.Combine(directory, "iOS.Phone", "bin", "iPhoneSimulator", "Debug", "CompassMobileiOSPhone.app");
                    App = ConfigureApp.iOS.AppBundle(pathToApp).EnableLocalScreenshots().ApiKey(Key).StartApp();
                    Queries = new iOSQueries();
			    }

			    else
			    {
                    pathToApp = Path.Combine(directory, "Droid.Phone", "bin", "Debug", "com.parivedasolutions.projectez.apk");
                    App = ConfigureApp.Android.ApkFile(pathToApp).EnableLocalScreenshots().ApiKey(Key).StartApp();
			        Queries = new AndroidQueries();
			    }
			}
			if(repl)
			{
				App.Repl ();
			}
		}

		public void Screenshot(string fileName, string caption)
		{
		    App.Screenshot(caption);
		}
	}
}

