using System;
using Xamarin.UITest.Queries;

namespace Demo.UITests.Helpers
{
    public interface IScreenQueries
    {
        Func<AppQuery, AppQuery> LoginField { get; }

        Func<AppQuery, AppQuery> LoginButton { get; }

        Func<AppQuery, AppQuery> LoginErrorMessageView { get; }

        Func<AppQuery, AppQuery> SeasonsView { get; }

        Func<AppQuery, AppQuery> TestSeasonView { get; }

        Func<AppQuery, AppQuery> TeamsView { get; }

        Func<AppQuery, AppQuery> TestTeamView { get; }

        Func<AppQuery, AppQuery> StartUnitTestsView { get; }

        Func<AppQuery, AppQuery> UnitTestsSuccessView { get; }

        Func<AppQuery, AppQuery> TestIncorrectSeasonView { get; }
    }
}