using System;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using UnityServiceLocator = Microsoft.Practices.Unity.UnityServiceLocator;
using Microsoft.Practices.Unity;
using Shared.Common;
using Shared.Api;
using System.Linq;
using System.Collections.Generic;

namespace Shared.Bootstrapper
{
	public static class AutoMapperBootstrapper
	{
		public static void MapTypes()
		{
			// Model to DTO


			// DTO to Model
			Mapper.CreateMap<TwitterFeedItemDto, Tweet>()
				.ForMember(model => model.CreatedAt,
					opts => opts.MapFrom(dto => dto.Created_At))
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
			Mapper.CreateMap<FacebookFeedItemDto, FacebookPost> ()
				.ForMember (model => model.CreatedAt,
					opts => opts.MapFrom (dto => dto.Created_Time))
				.ForMember (model => model.UpdatedAt,
					opts => opts.MapFrom (dto => dto.Updated_Time))
				.ForMember (model => model.Text,
					opts => opts.MapFrom (dto => dto.Message))
				.ForMember (model => model.ImageUrl,
					opts => opts.MapFrom (dto => dto.Picture))
				.ForMember (model => model.LinkUrl,
					opts => opts.MapFrom (dto => dto.Link))
				.ForMember (model => model.ShareCount,
					opts => opts.MapFrom (dto => (dto.Shares != null) ? dto.Shares.Count : 0))
				.ForMember (model => model.User,
					opts => opts.MapFrom (dto => dto.From))
				.ForMember (model => model.Likes,
					opts => opts.MapFrom (dto => (dto.Likes != null) ? dto.Likes.Data : new List<FacebookLikeDto>()))
				.ForMember (model => model.Comments,
					opts => opts.MapFrom (dto => (dto.Comments != null) ? dto.Comments.Data : new List<FacebookCommentDto>()));
			Mapper.CreateMap<FacebookToFromDto, FacebookUser> ();
			Mapper.CreateMap<FacebookCommentDto, FacebookComment> ()
				.ForMember (model => model.CreatedAt,
					opts => opts.MapFrom (dto => dto.Created_Time))
				.ForMember (model => model.User,
					opts => opts.MapFrom (dto => dto.From))
				.ForMember (model => model.Text,
					opts => opts.MapFrom (dto => dto.Message))
				.ForMember (model => model.LikedCount,
					opts => opts.MapFrom (dto => dto.Like_Count))
				.ForMember (model => model.IsLikedByUser,
					opts => opts.MapFrom (dto => dto.User_Likes));
		}
	}
}

