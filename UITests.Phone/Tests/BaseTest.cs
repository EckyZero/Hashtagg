using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.UITest;
using NUnit.Framework;

namespace UITests.Phone
{
    public abstract class BaseTest
    {
        protected AppUtils Utils { get; set; }
        protected IApp App { get; set; }
        protected ScreenQueries Queries { get; set; }

        public void BeforeEach (bool repl, string platform = null)
        {
            Utils = new AppUtils(repl, platform);

            App = Utils.App;
            Queries = Utils.Queries;
        }

        protected BaseTest (AppUtils utils) : base ()
        {
            Utils = utils;
            App = Utils.App;
            Queries = Utils.Queries;
        }

        protected BaseTest () : base () { }
    }
}