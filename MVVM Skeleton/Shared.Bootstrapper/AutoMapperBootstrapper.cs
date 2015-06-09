using System;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using UnityServiceLocator = Microsoft.Practices.Unity.UnityServiceLocator;
using Microsoft.Practices.Unity;
using Shared.Common;
using Shared.Api;

namespace Shared.Bootstrapper
{
	public static class AutoMapperBootstrapper
	{
		public static void MapTypes()
		{
			// Model to DTO
			Mapper.CreateMap<TwitterFeedItem, TwitterFeedItemDto>();

			// DTO to Model
			Mapper.CreateMap<TwitterFeedItemDto, TwitterFeedItem>();
		}
	}
}

