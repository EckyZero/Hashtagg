using System;
using System.IO;
using System.Reflection;
using Xamarin.UITest;

namespace Demo.UITests.Helpers
{
    public class AppUtils
    {
        private readonly string platform;

        public AppUtils(bool unitTesting)
        {
            string path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var info = new FileInfo(path);
            string directory = info.Directory.Parent.Parent.Parent.FullName;
            TestPlatform environment = TestEnvironment.Platform;

            if (environment == TestPlatform.TestCloudiOS)
            {
                Queries = new IosQueries();
                App = ConfigureApp.iOS.StartApp();
            }
            else if (TestEnvironment.Platform.Equals(TestPlatform.TestCloudAndroid))
            {
                Queries = new AndroidQueries();
                App = ConfigureApp.Android.StartApp();
            }
            else if (environment == TestPlatform.Local)
            {
                platform = Environment.GetEnvironmentVariable("ps_platform");

                if (string.IsNullOrEmpty(platform))
                {
                    throw new Exception("ps_platform environment variable not set");
                }

                string pathToApp;
                if (platform == "ios")
                {
                    pathToApp = unitTesting
                        ? Path.Combine(directory, "Demo.iOS.Tests", "bin", "iPhoneSimulator", "Debug",
                            "DemoiOSTests.app")
                        : Path.Combine(directory, "iOS", "bin", "iPhoneSimulator", "Debug", "DemoiOS.app");
                    Queries = new IosQueries();
                    App = ConfigureApp.iOS.AppBundle(pathToApp).EnableLocalScreenshots().StartApp();
                }
                else
                {
                    pathToApp = unitTesting
                        ? Path.Combine(directory, "Demo.Android.Tests", "bin", "Debug", "Demo.Android.Tests.apk")
                        : Path.Combine(directory, "Android", "bin", "Debug", "Demo.Android.apk");
                    Queries = new AndroidQueries();
                    App = ConfigureApp.Android.ApkFile(pathToApp).EnableLocalScreenshots().StartApp();
                }
            }
            else
            {
                throw new NotImplementedException(String.Format("I don't know this platform {0}", environment));
            }
        }

        public IScreenQueries Queries { get; set; }
        public IApp App { get; set; }

        public void Screenshot(string fileName, string caption)
        {
            FileInfo file = App.Screenshot(caption);
            string directory = file.Directory.FullName;
            string newPath = Path.Combine(directory, string.Format("{0}-{1}", platform, fileName));

            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }
            File.Move(file.FullName, newPath);
        }
    }
}