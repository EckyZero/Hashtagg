using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Shared.Exceptions;
using Demo.Shared.Models;
using Xamarin;

namespace Demo.Shared
{
    public static class TeamBL
    {
        public static async Task<List<Team>> GetTeamsWithSeasonId(int seasonId, bool forceSync)
        {
            try
            {
                List<Team> teams = TeamDal.GetTeamsWithSeasonId(seasonId);

                if (forceSync || teams == null || teams.Count == 0)
                {
                    teams = await TeamSal.GetTeamsWithSeasonId(seasonId);

                    TeamDal.Refresh(seasonId, teams);
                }
                return teams;
            }
            catch (BaseException exception)
            {
                Insights.Report(exception);
                throw new BLException(string.Format("Getting teams with seasonId={0} failed", seasonId), exception);
            }
        }
    }
}