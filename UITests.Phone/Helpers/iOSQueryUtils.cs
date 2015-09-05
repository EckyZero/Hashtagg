using System;
using Xamarin.UITest.Queries;

namespace UITests.Phone
{
	public class iOSQueryUtils : IQueryUtils
	{
		public Func<AppQuery, AppQuery> LabelWithTextLike (string text)
		{
			return c => c.Class ("UILabel").Property ("text").Like ("*" + text + "*").Index(0);
		}

		public Func<AppQuery, AppQuery> ButtonWithTextLike (string text)
		{
			return c => c.Class ("UIButton").Property ("text").Like ("*" + text + "*").Index(0);
		}

        public Func<AppQuery, AppQuery> ImageWithName (string name, string id)
		{
			return c => c.Class ("UIImageView").Id (name).Index(0);  
		}
	}
}

