namespace Shared.Common
{
	public interface IHudService
	{
	    void Show(string message = "");
		void Dismiss();
	}
}

