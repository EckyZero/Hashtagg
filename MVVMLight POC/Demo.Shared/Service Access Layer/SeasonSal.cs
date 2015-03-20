using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Shared.Models;

namespace Demo.Shared
{
    public static class SeasonSal
    {
        public static async Task<List<Season>> GetAll()
        {
            IServiceAccessLayer service = SharedSal.GetImplementation();

            return await service.Get<List<Season>>(null, SharedSal.SeasonsEndpoint);
        }
    }
}