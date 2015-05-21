using System;
using System.Threading.Tasks;

namespace Shared.Common
{
	public interface IHudService
	{
		Task ShowFeedbackPopup (PSColor background, PSColor fontColor, string imagePath, string message, int timeout);
	    void Show(string message = "");
	    void Dismiss();
	}
}

