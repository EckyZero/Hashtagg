using System.Reflection;
using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;

namespace Demo.Android.Tests
{
    [Activity(Label = "Demo.Android.Tests", MainLauncher = true)]
    public class MainActivity : TestSuiteActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
            UnitTests.Shared.App.InitializeAction = App.Initialize;
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);


            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}