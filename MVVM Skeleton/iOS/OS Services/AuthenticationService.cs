using System;
using Shared.Common;
using Auth0.SDK;
using System.Threading.Tasks;
using Shared.DAL;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace iOS
{
    public class AuthenticationService : BaseService, IOAuth
	{
		private string _url;
		private string _clientId;
		private string _connection;

		private Auth0Client _client;
		private IUserSettingDAL _userSettingDAL;

		private const string OAUTH_ACCESS_TOKEN_KEY = "OAuthAccessToken";
		private const string OAUTH_REFRESH_TOKEN_KEY = "OAuthRefreshToken";
		private const string OAUTH_SCOPE = "openid offline_access profile";
		private const string SCOPE = "scope";
		private const string PROVIDER_ACCESS_TOKEN_KEY = "id_token";

		public  OAuthUser CurrentUser { get; private set; }

		public AuthenticationService(string url, string clientId, string connection)
		{
			_url = url;
			_clientId = clientId;
			_connection = connection;
			_client = new Auth0Client (_url,_clientId);
			_userSettingDAL = IocContainer.GetContainer ().Resolve<IUserSettingDAL> ();
		}

		public async Task<OAuthUser> LoginAsync (string username, string password)
		{
            try
            {
                var handle = _logger.TrackTime("Logging in to authentication server");

                handle.Start();
				Auth0User auth0User = await _client.LoginAsync (_connection, username, password, withRefreshToken: true, scope: OAUTH_SCOPE);
                handle.Stop();

				var oauthUser = new OAuthUser ();
				oauthUser.MemberKey = int.Parse((string)auth0User.Profile["memberKey"]);
				oauthUser.EmployerKey = int.Parse((string)auth0User.Profile["employerKey"]);
				oauthUser.EmployeeKey = int.Parse((string)auth0User.Profile["employeeKey"]);
				oauthUser.PatientId = int.Parse((string)auth0User.Profile["patientId"]);
				oauthUser.PersonKey = int.Parse((string)auth0User.Profile["personKey"]);
				oauthUser.Username = (string)auth0User.Profile["memberId"];

				CurrentUser = oauthUser;

				PersistTokensToDb(auth0User.IdToken, auth0User.RefreshToken);

				return oauthUser;
			}
			catch(Exception e){
				_logger.Log (e);
				return null;
			}
		}

		private void PersistTokensToDb(string accessToken, string refreshToken)
		{
			var accessTokenSetting = new UserSetting (OAUTH_ACCESS_TOKEN_KEY,accessToken);
			PersistTokenToDb (accessTokenSetting);

			var refreshTokenSetting = new UserSetting (OAUTH_REFRESH_TOKEN_KEY,refreshToken);
			PersistTokenToDb (refreshTokenSetting);
		}

		private void PersistTokenToDb(UserSetting tokenSetting)
		{
			_userSettingDAL.CreateOrUpdate (tokenSetting);
		}


		public void Logout()
		{
			try{
				_client.Logout ();

				_userSettingDAL.ClearTable ();
			}
			catch(Exception e){
				_logger.Log (e);
			}
		}

		public bool HasTokenExpired()
		{
			try{
				if(_client.CurrentUser != null){
					return _client.HasTokenExpired ();
				}
				else
                {
					//Note: this code is executed in the normal scenario where tokens are being retrieved from the database
					var token = ReadAccessTokenFromDb();

					if(token == null){
						return true;
					}

					return TokenValidator.HasExpired(token);
				}
			}
			catch{
				return true;
			}
		}

		public async Task<string> GetAccessTokenAsync()
		{
			if (!HasTokenExpired ()) {

				if (_client.CurrentUser != null) {
					return _client.CurrentUser.IdToken;
				} else {
					return ReadAccessTokenFromDb ();
				}

			} 
            else 
            {
                var refreshToken = GetRefreshToken();
			    if (refreshToken != null)
			    {
                    var handle = _logger.TrackTime("Refreshing token from authentication server");

                    handle.Start();
                    var accessTokenResponse = await _client.RefreshToken(refreshToken, new Dictionary<string, string>() {
					{ SCOPE,OAUTH_SCOPE }
				});
                    handle.Stop();

                    var accessToken = new UserSetting(OAUTH_ACCESS_TOKEN_KEY, (string)accessTokenResponse[PROVIDER_ACCESS_TOKEN_KEY]);
                    PersistTokenToDb(accessToken);

                    return accessToken.Value;    
			    }
			    return null;
			}
		}


		public string GetRefreshToken()
		{
			if (_client.CurrentUser != null) {
				return _client.CurrentUser.RefreshToken;
			} else {
				return ReadRefreshTokenFromDb ();
			}
		}

		private string ReadAccessTokenFromDb()
		{
			var settings = _userSettingDAL.ReadAll ();

			var setting = settings.SingleOrDefault (s => s.Key == OAUTH_ACCESS_TOKEN_KEY);
			if(setting == null){
				return null;
			}

			return setting.Value;
		}

		private string ReadRefreshTokenFromDb()
		{
			var settings = _userSettingDAL.ReadAll ();

			var setting = settings.SingleOrDefault (s => s.Key == OAUTH_REFRESH_TOKEN_KEY);
			if(setting == null){
				return null;
			}
			return setting.Value;
		}

	}
}

