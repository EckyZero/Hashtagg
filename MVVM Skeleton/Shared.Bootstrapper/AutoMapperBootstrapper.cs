using System;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using UnityServiceLocator = Microsoft.Practices.Unity.UnityServiceLocator;
using Microsoft.Practices.Unity;
using Shared.Common;
using Shared.Api;
using System.Linq;

namespace Shared.Bootstrapper
{
	public static class AutoMapperBootstrapper
	{
		public static void MapTypes()
		{
			// Model to DTO
			Mapper.CreateMap<Tweet, TwitterFeedItemDto>();

			// DTO to Model
			Mapper.CreateMap<TwitterFeedItemDto, Tweet>()
				.ForMember(model => model.RetweetCount,
					opts => opts.MapFrom(dto => dto.Retweet_Count))
				.ForMember(model => model.FavoriteCount,
					opts => opts.MapFrom(dto => dto.Favorite_Count))
				.ForMember(model => model.IsFavoritedByUser,
					opts => opts.MapFrom(dto => dto.Favorited))
				.ForMember(model => model.IsRetweetedByUser,
					opts => opts.MapFrom(dto => dto.Retweeted))
				.ForMember(model => model.UserId,
					opts => opts.MapFrom(dto => dto.User.Id))
				.ForMember(model => model.UserName,
					opts => opts.MapFrom(dto => dto.User.Name))
				.ForMember(model => model.UserScreenName,
					opts => opts.MapFrom(dto => dto.User.Screen_Name))
				.ForMember(model => model.UserLocation,
					opts => opts.MapFrom(dto => dto.User.Location))
				.ForMember(model => model.UserUrl,
					opts => opts.MapFrom(dto => dto.User.Url))
				.ForMember(model => model.UserImageUrl,
					opts => opts.MapFrom(dto => dto.User.Profile_Image_Url))
				.ForMember(model => model.ImageUrl,
					opts => opts.MapFrom(dto => (dto.Entities != null && dto.Entities.Media != null) ? dto.Entities.Media.FirstOrDefault( m => m.Type.Equals("photo")).Media_Url : String.Empty));
		}
	}
}

