using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Demo.Shared.Exceptions;
using Demo.Shared.Helpers;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Xamarin;

namespace Demo.Shared
{
    public class SharedSal : IServiceAccessLayer
    {
        // Full list at http://www.football-data.org/
        public static readonly string BaseUrl = "http://www.football-data.org";
        public static readonly string SeasonsEndpoint = "/soccerseasons";
        public static readonly string TeamsEndpoint = "/%@/teams";

        public async Task<TClass> Get<TClass>(Dictionary<string, string> parameters, string endpoint)
        {
            var client = new HttpClient();
            string url = string.Format("{0}{1}?{2}", BaseUrl, endpoint, ToQueryString(parameters));

            try
            {
                string response;

                using (ITrackHandle handle = Insights.TrackTime("Time Spent: " + url))
                {
                    response = await client.GetStringAsync(url);
                }
                var result = JsonConvert.DeserializeObject<TClass>(response);

                return result;
            }
            catch (HttpRequestException exception)
            {
                Insights.Report(exception);
				throw new SalException(string.Format("Retrieving data from resource={0}", url), exception);
            }
        }

        public static IServiceAccessLayer GetImplementation()
        {
            var service = App.IocContainer.Resolve<IServiceAccessLayer>();
            return service;
        }

        private string ToQueryString(IDictionary<string, string> dict)
        {
            if (dict == null) return string.Empty;

            var buffer = new StringBuilder();
            int count = 0;
            bool end = false;

            foreach (string key in dict.Keys)
            {
                if (count == dict.Count - 1) end = true;

                if (end)
                    buffer.AppendFormat("{0}={1}", key, dict[key]);
                else
                    buffer.AppendFormat("{0}={1}&", key, dict[key]);

                count++;
            }

            return buffer.ToString();
        }
    }
}