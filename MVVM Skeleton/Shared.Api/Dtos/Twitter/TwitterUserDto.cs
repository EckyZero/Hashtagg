using System;

namespace Shared.Api
{
	public class TwitterUserDto
	{
		public int Id { get; set; }
		public string Id_str { get; set; }
		public string Name { get; set; }
		public string Screen_name { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public TwitterEntityDto Entities { get; set; }
		public bool Protected { get; set; }
		public int Followers_Count { get; set; }
		public int Friends_Count { get; set; }
		public int Listed_Count { get; set; }
		public DateTime Created_At { get; set; }
		public int Favourites_Count { get; set; }
		public int? Utc_Offset { get; set; }
		public string Time_Zone { get; set; }
		public bool Geo_Enabled { get; set; }
		public bool Verified { get; set; }
		public int Statuses_Count { get; set; }
		public string Lang { get; set; }
		public bool Contributors_Enabled { get; set; }
		public bool Is_Translator { get; set; }
		public bool Is_Translation_enabled { get; set; }
		public string Profile_Background_Color { get; set; }
		public string Profile_Background_Image_Url { get; set; }
		public string Profile_Background_Image_Url_Https { get; set; }
		public bool Profile_Background_Tile { get; set; }
		public string Profile_Image_Url { get; set; }
		public string Profile_Image_Url_Https { get; set; }
		public string Profile_Banner_Url { get; set; }
		public string Profile_Link_Color { get; set; }
		public string Profile_Sidebar_Border_Color { get; set; }
		public string Profile_Sidebar_Fill_Color { get; set; }
		public string Profile_Text_Color { get; set; }
		public bool Profile_Use_Background_Image { get; set; }
		public bool Default_Profile { get; set; }
		public bool Default_Profile_Image { get; set; }
		public bool Following { get; set; }
		public bool Follow_Request_Sent { get; set; }
		public bool Notifications { get; set; }

		public TwitterUserDto ()
		{
		}
	}
}

