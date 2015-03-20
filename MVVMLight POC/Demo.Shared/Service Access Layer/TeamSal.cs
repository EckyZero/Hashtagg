using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Shared.Models;

namespace Demo.Shared
{
    public static class TeamSal
    {
        public static async Task<List<Team>> GetTeamsWithSeasonId(int seasonId)
        {
            string endpoint = string.Format("{0}{1}", SharedSal.SeasonsEndpoint,
                SharedSal.TeamsEndpoint.Replace("%@", seasonId.ToString()));
            IServiceAccessLayer service = SharedSal.GetImplementation();

            return await service.Get<List<Team>>(null, endpoint);
        }
    }
}