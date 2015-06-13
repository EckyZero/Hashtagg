using System;
using Shared.Api;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Shared.Common;
using Microsoft.Practices.Unity;
using AutoMapper;

namespace Shared.Service
{
	public class FacebookService : BaseService, IFacebookService
	{
		#region Private Variables

		IFacebookApi _facebookApi;

		#endregion

		#region Methods

		public FacebookService ()
		{
			_facebookApi = IocContainer.GetContainer ().Resolve<IFacebookApi> ();
		}

		public async Task<ServiceResponse<ObservableCollection<FacebookPost>>> GetHomeFeed ()
		{
			var models = new ObservableCollection<FacebookPost> ();

			try
			{
				if(_connectivityService.IsConnected)
				{
					var dtos = await _facebookApi.GetHomeFeed();

					foreach (FacebookFeedItemDto dto in dtos.Data)
					{
						var model = Mapper.Map<FacebookPost> (dto);
						models.Add(model);
					}	

					return new ServiceResponse<ObservableCollection<FacebookPost>>(models,ServiceResponseType.SUCCESS);	
				}
				else
				{
					return new ServiceResponse<ObservableCollection<FacebookPost>>(models,ServiceResponseType.NO_CONNECTION);
				}
			}
			catch (BaseException exception)
			{
				return new ServiceResponse<ObservableCollection<FacebookPost>> (models, ServiceResponseType.ERROR);
			}
			catch (Exception e)
			{
				_logger.Log (new ServiceException ("Error getting facebook posts", e), LogType.ERROR);
				return new ServiceResponse<ObservableCollection<FacebookPost>> (models, ServiceResponseType.ERROR);
			} 
		}

		#endregion
	}
}

