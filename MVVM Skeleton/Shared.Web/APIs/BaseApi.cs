using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Net;
using Shared.Common;
using Shared.Common.Logging;
using Shared.Web;

namespace Shared.Web
{
	public abstract class BaseApi 
	{
	    protected ILogger _logger;

	    private static TimeSpan DEFAULT_TIMEOUT = TimeSpan.FromSeconds(20);
		IHttpClientHelper _helper;
		IOAuth _authenticationService;

		private const string DefaultMediaType = "application/json"; 

		private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings 
		{ 
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			NullValueHandling = NullValueHandling.Include
		};

		public BaseApi()
		{
            _logger = IocContainer.GetContainer().Resolve<ILogger>();

			_helper = IocContainer.GetContainer().Resolve<IHttpClientHelper> ();
			_authenticationService = IocContainer.GetContainer().Resolve<IOAuth> ();
		}

		private async Task<HttpClient> CreateClient()
		{
			var accessToken = await _authenticationService.GetAccessTokenAsync ();

			if (_helper != null)
			{
			    var c = new HttpClient(_helper.MessageHandler)
			    {
                    Timeout = DEFAULT_TIMEOUT   
			    };

			    if (accessToken != null)
			    {
                    c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);    
			    }
				return c;
			} else {
                var c = new HttpClient()
                {
                    Timeout = DEFAULT_TIMEOUT
                };

			    if (accessToken != null)
			    {
                    c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);    
			    }
				return c;
			}
		}

		#region Get

	    public async Task<T> GetAsync<T>(string relativeResource, string query = "", Func<string, T> parser = null)
	    {
	        var client = await CreateClient();
	        string response = await client.GetStringAsync(PrepareUrl(relativeResource, query));

	        T result;
	        if (parser == null)
	        {
	            result = JsonConvert.DeserializeObject<T>(response, _jsonSettings);
	        }
	        else
	        {
	            result = parser(response);
	        }

	        return result;
	    }

	    public T Get<T>(string relativeResource, string query = "", Func<string, T> parser = null)
		{
			var client = CreateClient().Result;
			var response = client.GetStringAsync (PrepareUrl(relativeResource, query)).Result;

			T result;

			if(parser == null)
			{
				result = JsonConvert.DeserializeObject<T> (response, _jsonSettings );
			}
			else
			{
				result = parser (response);
			}

			return result;
		}
			
		public T Get<T>(string relativeResource, IDictionary<string, string> query = null,  Func<string, T> parser = null)
		{
			if(query == null){
				return this.Get<T> (relativeResource, string.Empty, parser);
			}
			else{
				return this.Get<T> (relativeResource, query.ToWebQueryString (), parser);
			}
		}

		public async Task<T> GetAsync<T>(string relativeResource, IDictionary<string, string> query = null, Func<string, T> parser = null)
		{
			if(query == null){
				return await GetAsync<T> (relativeResource, string.Empty, parser);
			}
			else{
				return await GetAsync<T> (relativeResource, query.ToWebQueryString (), parser);
			}
		}

		#endregion

		#region Post

		public async Task<R> PostAsync<T,R> (string relativeResource, T model)
		{
			return await PostAsync<R> (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public R Post<T,R>(string relativeResource, T model)
		{
			return Post<R>(relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}
			
		public async Task<R> PostAsync<R> (string relativeResource, string json)
		{
			var client = await CreateClient();
			var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
			var response = await client.PostAsync (PrepareUrl(relativeResource), httpContent);
			if (response.StatusCode != HttpStatusCode.OK &&
				response.StatusCode != HttpStatusCode.Created &&
				response.StatusCode != HttpStatusCode.Accepted &&
				response.StatusCode != HttpStatusCode.NonAuthoritativeInformation &&
				response.StatusCode != HttpStatusCode.NoContent &&
				response.StatusCode != HttpStatusCode.ResetContent &&
				response.StatusCode != HttpStatusCode.PartialContent) {
				throw new SalException(response.StatusCode);
			}
			var jsonResponse = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<R> (jsonResponse, _jsonSettings);
			return result;
		}

		public async Task PostAsync<T>(string relativeResource, T model)
		{
			await PostAsync (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public async Task PostAsync(string relativeResource, string json)
		{
			var client = await CreateClient();
			var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
			var response = await client.PostAsync (PrepareUrl(relativeResource), httpContent);
			if (response.StatusCode != HttpStatusCode.OK &&
			    response.StatusCode != HttpStatusCode.Created &&
			    response.StatusCode != HttpStatusCode.Accepted &&
			    response.StatusCode != HttpStatusCode.NonAuthoritativeInformation &&
			    response.StatusCode != HttpStatusCode.NoContent &&
			    response.StatusCode != HttpStatusCode.ResetContent &&
				response.StatusCode != HttpStatusCode.PartialContent) {
				throw new HttpRequestException (string.Format("Status Code was {0}", response.StatusCode));
			}
		}

		public R Post<R> (string relativeResource, string json)
		{
			var client = CreateClient().Result;
			var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
			var response = client.PostAsync (PrepareUrl(relativeResource), httpContent).Result;
			var jsonResponse = response.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<R> (jsonResponse, _jsonSettings);
			return result;
		}

		public void Post<T> (string relativeResource, T model)
		{
			Post (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public void Post (string relativeResource, string json)
		{
			var client = CreateClient().Result;
			var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
			var response = client.PostAsync (PrepareUrl(relativeResource), httpContent).Result;
			var jsonResponse = response.Content.ReadAsStringAsync().Result;
		}

		#endregion

		#region Put

		public async Task<R> PutAsync<T,R> (string relativeResource, T model)
		{
			return await PutAsync<R> (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public R Put<T,R>(string relativeResource, T model)
		{
			return Put<R>(relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public async Task<R> PutAsync<R> (string relativeResource, string json)
		{
			var client = await CreateClient();
			var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
			var response = await client.PutAsync (PrepareUrl(relativeResource), httpContent);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<R> (jsonResponse, _jsonSettings);
			return result;
		}

		public async Task PutAsync<T>(string relativeResource, T model)
		{
			await PutAsync (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public async Task PutAsync(string relativeResource, string json)
		{
			var client = await CreateClient();
			var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
			var response = await client.PutAsync (PrepareUrl(relativeResource), httpContent);
			await response.Content.ReadAsStringAsync();
		}

		public R Put<R> (string relativeResource, string json)
		{
			var client = CreateClient().Result;
			var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
			var response = client.PutAsync (PrepareUrl(relativeResource), httpContent).Result;
			var jsonResponse = response.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<R> (jsonResponse, _jsonSettings);
			return result;
		}

		public void Put<T> (string relativeResource, T model)
		{
			Put (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public void Put (string relativeResource, string json)
		{
			var client = CreateClient().Result;
			var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
			var response = client.PutAsync (PrepareUrl(relativeResource), httpContent).Result;
			var jsonResponse = response.Content.ReadAsStringAsync().Result;
		}

		#endregion

		#region Delete
		public async Task<T> DeleteAsync<T>(string relativeResource, string query = "")
		{
			var client = await CreateClient();
			var response = await client.DeleteAsync (PrepareUrl (relativeResource, query));
			var jsonResponse = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<T> (jsonResponse, _jsonSettings);
			return result;
		}


		public async Task DeleteAsync(string relativeResource, string query = "")
		{
			var client = await CreateClient();
			var response = await client.DeleteAsync (PrepareUrl (relativeResource, query));
			var result = await response.Content.ReadAsStringAsync();

			System.Diagnostics.Debug.WriteLine (result);
		}

		public async Task<T> DeleteAsync<T>(string relativeResource, IDictionary<string, string> query = null)
		{
			if(query == null){
				return await DeleteAsync<T> (relativeResource, string.Empty);
			}
			else{
				return await DeleteAsync<T> (relativeResource, query.ToWebQueryString ());
			}
		}

		public async Task DeleteAsync(string relativeResource, IDictionary<string, string> query = null)
		{
			if(query == null){
				await DeleteAsync (relativeResource, string.Empty);
			}
			else{
				await DeleteAsync (relativeResource, query.ToWebQueryString ());
			}
		}

		public T Delete<T>(string relativeResource, string query = "")
		{
			var client = CreateClient().Result;
			var response = client.DeleteAsync (PrepareUrl (relativeResource, query)).Result;
			var jsonResponse = response.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<T> (jsonResponse, _jsonSettings);
			return result;
		}

		public void Delete(string relativeResource, string query = "")
		{
			var client = CreateClient().Result;
			var response = client.DeleteAsync (PrepareUrl (relativeResource, query)).Result;
			var result = response.Content.ReadAsStringAsync().Result;
		}

		public T Delete<T>(string relativeResource, IDictionary<string, string> query = null)
		{
			if (query == null) {
				return this.Delete<T> (relativeResource, string.Empty);
			} else {
				return this.Delete<T> (relativeResource, query.ToWebQueryString ());
			}
		}

		public void Delete(string relativeResource, IDictionary<string, string> query = null)
		{
			if(query == null)
			{
				Delete (relativeResource, string.Empty);
			}
			else
			{
				Delete (relativeResource, query.ToWebQueryString ());
			}
		}

		#endregion

		private string PrepareUrl(string relativeResource, string query = null)
		{
			var resource = "";

			if(query == null)
			{
				resource = string.Format ("{0}{1}", Routes.BASE_URL, relativeResource);
			}
			else if (!relativeResource.EndsWith ("?") && !string.IsNullOrEmpty (query)) 
			{
				resource = string.Format ("{0}{1}?{2}", Routes.BASE_URL, relativeResource, query);
			} 
			else 
			{
				resource = string.Format ("{0}{1}{2}", Routes.BASE_URL, relativeResource, query);
			}
			System.Diagnostics.Debug.WriteLine (resource);
			return resource;
		}

	}
}

