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
            App.WaitForElement(Queries.Utils.LabelWithTextLike("Hashtagg"));
        }
    }
}

