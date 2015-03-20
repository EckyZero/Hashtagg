using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Shared
{
    public interface IServiceAccessLayer
    {
        Task<TClass> Get<TClass>(Dictionary<string, string> parameters, string endpoint);
    }
}