using System;
using System.Collections.Generic;
using Demo.Shared.Exceptions;
using Demo.Shared.Models;
using Xamarin;
using SQLite;

namespace Demo.Shared
{
    public static class TeamDal
    {
        static TeamDal()
        {
            ISecureDatabase database = SharedDal.GetDatabase();

            try
            {
                database.CreateTable<Team>();
            }
			catch (SQLiteException exception)
            {
                Insights.Report(exception);
                throw new DalException("Creating table Team failed.", exception);
            }
        }

        public static List<Team> GetTeamsWithSeasonId(int seasonId)
        {
            ISecureDatabase database = SharedDal.GetDatabase();

            try
            {
                IEnumerable<Team> teams = database.QueryWithSql<Team>("select * from team where seasonId = ? ",
                    new object[] {seasonId});

                return new List<Team>(teams);
            }
			catch (SQLiteException exception)
            {
                Insights.Report(exception);
                throw new DalException(string.Format("Getting teams with seasonId={0} failed", seasonId), exception);
            }
        }

        public static void DeleteTeamsWithSeasonId(int seasonId)
        {
            ISecureDatabase database = SharedDal.GetDatabase();
            List<Team> teams = GetTeamsWithSeasonId(seasonId);

            try
            {
                foreach (Team team in teams)
                {
                    database.Delete(team);
                }
            }
			catch (SQLiteException exception)
            {
                Insights.Report(exception);
                throw new DalException(string.Format("Deleting teams with seasonId={0} failed.", seasonId), exception);
            }
        }

        public static void Refresh(int seasonId, List<Team> teams)
        {
            ISecureDatabase database = SharedDal.GetDatabase();

            DeleteTeamsWithSeasonId(seasonId);

            try
            {
                foreach (Team team in teams)
                {
                    team.SeasonId = seasonId;
                }
				database.InsertOrReplaceAll(teams);
            }
			catch (SQLiteException exception)
            {
                Insights.Report(exception);
                throw new DalException(string.Format("Replacing teams in database for seasonId={0} failed.", seasonId),
                    exception);
            }
        }
    }
}