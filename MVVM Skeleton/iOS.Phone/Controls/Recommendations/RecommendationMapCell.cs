using System;
using UIKit;
using Shared.VM;
using Foundation;
using ObjCRuntime;

namespace iOS.Phone
{
	public abstract class RecommendationMapCell : UIView
	{
		public RecommendationMapCell () {}
		public RecommendationMapCell (IntPtr handle) : base (handle) {}

		public abstract void ConfigureCell(BaseRecommendationListItemViewModel docVm, Action<BaseRecommendationListItemViewModel> openMenu);
		protected abstract string CellNibFilename { get; }

		protected Action<BaseRecommendationListItemViewModel> _onRequestAction;

		protected UIView InitView()
		{
			var nib = NSBundle.MainBundle.LoadNib(CellNibFilename, this, null);

			var view = Runtime.GetNSObject(nib.ValueAt(0)) as UIView;

			return view;
		}

		protected void OnActionButtonClick(BaseRecommendationListItemViewModel vm)
		{
			if(_onRequestAction != null){
				_onRequestAction (vm);
			}
		}
	}
}

