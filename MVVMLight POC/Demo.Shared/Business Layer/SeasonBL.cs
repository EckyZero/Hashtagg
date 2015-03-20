using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Shared.Exceptions;
using Demo.Shared.Models;
using Xamarin;

namespace Demo.Shared
{
    public static class SeasonBL
    {
        public static async Task<List<Season>> GetAll(bool forceSync)
        {
            try
            {
                List<Season> seasons = SeasonDal.GetAll();

                if (forceSync || seasons == null || seasons.Count == 0)
                {
                    seasons = await SeasonSal.GetAll();

                    SeasonDal.Refresh(seasons);
                }
                return seasons;
            }
            catch (BaseException exception)
            {
                Insights.Report(exception);
                throw new BLException("Getting all seasons failed.", exception);
            }
        }
    }
}