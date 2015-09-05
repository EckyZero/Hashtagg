using System;
using Xamarin.UITest.Queries;

namespace UITests.Phone
{
	public interface IQueryUtils
	{
		Func<AppQuery, AppQuery> LabelWithTextLike (string text);
		Func<AppQuery, AppQuery> ButtonWithTextLike (string text);
        Func<AppQuery, AppQuery> ImageWithName (string imageFileName, string imageId = "");
	}
}

