using System;
using Xamarin.UITest.Queries;

namespace Demo.UITests.Helpers
{
    public class IosQueries : IScreenQueries
    {
        #region IScreenQueries implementation

        public Func<AppQuery, AppQuery> LoginField
        {
            get { return c => c.Class("UITextField"); }
        }

        public Func<AppQuery, AppQuery> LoginButton
        {
            get { return c => c.Class("UIButton"); }
        }

        public Func<AppQuery, AppQuery> LoginErrorMessageView
        {
            get { return c => c.Class("UILabel").Text("Please Enter a Valid Email Address"); }
        }

        public Func<AppQuery, AppQuery> SeasonsView
        {
            get { return c => c.Class("UILabel").Text("European Soccer Seasons"); }
        }

        public Func<AppQuery, AppQuery> TestSeasonView
        {
            get { return c => c.Class("UILabel").Text("Premier League 2014/15"); }
        }

        public Func<AppQuery, AppQuery> TeamsView
        {
            get { return c => c.Class("UILabel").Text("Teams"); }
        }

        public Func<AppQuery, AppQuery> TestTeamView
        {
            get { return c => c.Class("UILabel").Text("Manchester United FC"); }
        }

        public Func<AppQuery, AppQuery> StartUnitTestsView
        {
            get { return c => c.Class("UILabel").Text("Run Everything"); }
        }

        public Func<AppQuery, AppQuery> UnitTestsSuccessView
        {
            get { return c => c.Property("text").Like("Success! *"); }
        }

        public Func<AppQuery, AppQuery> TestIncorrectSeasonView
        {
            get { return c => c.Class("UILabel").Text("This is supposed to fail"); }
        }

        #endregion
    }
}