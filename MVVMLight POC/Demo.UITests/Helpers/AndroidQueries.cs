using System;
using Xamarin.UITest.Queries;

namespace Demo.UITests.Helpers
{
    public class AndroidQueries : IScreenQueries
    {
        #region IScreenQueries implementation

        public Func<AppQuery, AppQuery> LoginField
        {
            get { return c => c.Class("EditText"); }
        }

        public Func<AppQuery, AppQuery> LoginButton
        {
            get { return c => c.Class("Button"); }
        }

        public Func<AppQuery, AppQuery> LoginErrorMessageView
        {
            get { return c => c.Class("DialogTitle").Text("Please Enter a Valid Email Address"); }
        }

        public Func<AppQuery, AppQuery> SeasonsView
        {
            get { return c => c.Class("TextView").Text("European Soccer Seasons"); }
        }

        public Func<AppQuery, AppQuery> TestSeasonView
        {
            get { return c => c.Class("TextView").Text("Premier League 2014/15"); }
        }

        public Func<AppQuery, AppQuery> TeamsView
        {
            get { return c => c.Class("TextView").Text("Teams"); }
        }

        public Func<AppQuery, AppQuery> TestTeamView
        {
            get { return c => c.Class("TextView").Text("Manchester United FC"); }
        }

        public Func<AppQuery, AppQuery> StartUnitTestsView
        {
            get { return c => c.Class("TextView").Text("Run Tests"); }
        }

        public Func<AppQuery, AppQuery> UnitTestsSuccessView
        {
            get { return c => c.Property("text").Like("Passed"); }
        }

        public Func<AppQuery, AppQuery> TestIncorrectSeasonView
        {
            get { return c => c.Class("TextView").Text("This is supposed to fail"); }
        }

        #endregion
    }
}