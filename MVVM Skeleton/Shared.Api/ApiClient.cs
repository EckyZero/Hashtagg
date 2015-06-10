using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Shared.Common;
using System.Text;
using System.Net;
using Microsoft.Practices.Unity;

namespace Shared.Api
{
	public abstract class ApiClient
	{
		private const string DefaultMediaType = "application/json"; 

		protected abstract string BASE_URL { get; }
		protected string GET = "GET";
		protected string POST = "POST";
		protected string PUT = "PUT";
		protected string DELETE = "DELETE";

		protected ILogger Logger;

		private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings 
		{ 
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			NullValueHandling = NullValueHandling.Include
		};

		private HttpClient CreateClient()
		{
			return new HttpClient();
		}

		public ApiClient()
		{
			Logger = IocContainer.GetContainer ().Resolve<ILogger> ();
		}

		#region Get

		public async Task<T> GetAsync<T>(string relativeResource, string query = "", Func<string, T> parser = null)
		{
			using (var client = CreateClient())
			{
				string response = await client.GetStringAsync (PrepareUrl (relativeResource, query)).ConfigureAwait (false);

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
		}

		public async Task<T> GetAsync<T>(string relativeResource, IDictionary<string, string> query = null, Func<string, T> parser = null)
		{
			if(query == null){
				return await GetAsync<T> (relativeResource, string.Empty, parser);
			}
			else{
				return await GetAsync<T> (relativeResource, query.ToQueryString (), parser);
			}
		}

		#endregion

		#region Post

		public async Task<R> PostAsync<T,R> (string relativeResource, T model)
		{
			return await PostAsync<R> (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public async Task<R> PostAsync<R> (string relativeResource, string json)
		{
			using (var client = CreateClient())
			{
				var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
				var response = await client.PostAsync (PrepareUrl(relativeResource), httpContent).ConfigureAwait(false);
				if (response.StatusCode != HttpStatusCode.OK &&
					response.StatusCode != HttpStatusCode.Created &&
					response.StatusCode != HttpStatusCode.Accepted &&
					response.StatusCode != HttpStatusCode.NonAuthoritativeInformation &&
					response.StatusCode != HttpStatusCode.NoContent &&
					response.StatusCode != HttpStatusCode.ResetContent &&
					response.StatusCode != HttpStatusCode.PartialContent) {
					throw new ApiException(response.StatusCode);
				}
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<R> (jsonResponse, _jsonSettings);
				return result;	
			}
		}

		public async Task PostAsync<T>(string relativeResource, T model)
		{
			await PostAsync (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public async Task PostAsync(string relativeResource, string json)
		{
			using (var client = CreateClient())
			{
				var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
				var response = await client.PostAsync (PrepareUrl(relativeResource), httpContent).ConfigureAwait(false);
				if (response.StatusCode != HttpStatusCode.OK &&
					response.StatusCode != HttpStatusCode.Created &&
					response.StatusCode != HttpStatusCode.Accepted &&
					response.StatusCode != HttpStatusCode.NonAuthoritativeInformation &&
					response.StatusCode != HttpStatusCode.NoContent &&
					response.StatusCode != HttpStatusCode.ResetContent &&
					response.StatusCode != HttpStatusCode.PartialContent) {
					throw new ApiException(response.StatusCode);
				}	
			}
		}

		#endregion

		#region Put

		public async Task<R> PutAsync<T,R> (string relativeResource, T model)
		{
			return await PutAsync<R> (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public async Task<R> PutAsync<R> (string relativeResource, string json)
		{
			using (var client = CreateClient())
			{
				var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
				var response = await client.PutAsync (PrepareUrl(relativeResource), httpContent).ConfigureAwait(false);
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<R> (jsonResponse, _jsonSettings);
				return result;	
			}
		}

		public async Task PutAsync<T>(string relativeResource, T model)
		{
			await PutAsync (relativeResource, JsonConvert.SerializeObject (model, _jsonSettings));
		}

		public async Task PutAsync(string relativeResource, string json)
		{
			using (var client = CreateClient())
			{
				var httpContent = new StringContent(json, Encoding.UTF8, DefaultMediaType);
				var response = await client.PutAsync (PrepareUrl(relativeResource), httpContent).ConfigureAwait(false);
				await response.Content.ReadAsStringAsync();	
			}
		}

		#endregion

		#region Delete

		public async Task<T> DeleteAsync<T>(string relativeResource, string query = "")
		{
			using (var client = CreateClient())
			{
				var response = await client.DeleteAsync (PrepareUrl (relativeResource, query)).ConfigureAwait(false);
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<T> (jsonResponse, _jsonSettings);
				return result;	
			}
		}

		public async Task DeleteAsync(string relativeResource, string query = "")
		{
			using (var client = CreateClient())
			{
				var response = await client.DeleteAsync (PrepareUrl (relativeResource, query)).ConfigureAwait(false);
				var result = await response.Content.ReadAsStringAsync();	

				System.Diagnostics.Debug.WriteLine (result);
			}
		}

		public async Task<T> DeleteAsync<T>(string relativeResource, IDictionary<string, string> query = null)
		{
			if(query == null){
				return await DeleteAsync<T> (relativeResource, string.Empty);
			}
			else{
				return await DeleteAsync<T> (relativeResource, query.ToQueryString ());
			}
		}

		public async Task DeleteAsync(string relativeResource, IDictionary<string, string> query = null)
		{
			if(query == null){
				await DeleteAsync (relativeResource, string.Empty);
			}
			else{
				await DeleteAsync (relativeResource, query.ToQueryString ());
			}
		}

		#endregion

		private string PrepareUrl(string relativeResource, string query = null)
		{
			var resource = "";

			if(query == null)
			{
				resource = string.Format ("{0}{1}", BASE_URL, relativeResource);
			}
			else if (!relativeResource.EndsWith ("?") && !string.IsNullOrEmpty (query)) 
			{
				resource = string.Format ("{0}{1}?{2}", BASE_URL, relativeResource, query);
			} 
			else 
			{
				resource = string.Format ("{0}{1}{2}", BASE_URL, relativeResource, query);
			}
			System.Diagnostics.Debug.WriteLine (resource);
			return resource;
		}
	}
}

