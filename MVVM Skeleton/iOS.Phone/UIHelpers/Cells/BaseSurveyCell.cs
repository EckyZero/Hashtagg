using System;
using UIKit;
using Shared.Common;
using Foundation;
using ObjCRuntime;
using Shared.Common.Models.TuringTree;
using System.Linq;
using Shared.Common.TuringTree;
using Shared.VM;
using CoreGraphics;

namespace iOS.Phone
{
	public abstract class BaseSurveyCell : UITableViewCell
	{
		#region Member Properties

		public Action<object, GenericAction> TouchUpAction;
		public NodeSection Section { get; private set; }
		public NSIndexPath IndexPath { get; private set; }

		#endregion

		#region Methods

		protected BaseSurveyCell (IntPtr handle) : base(handle) {}

		protected abstract void InitBindings ();

		public virtual void Configure (NodeSection section, NSIndexPath indexPath)
		{
			Section = section;
			IndexPath = indexPath;

			InitBindings ();
		}

		public nfloat CalculateHeight (NodeSection section, NSIndexPath indexPath)
		{
			Configure (section, indexPath);

			var height = ContentView.SystemLayoutSizeFittingSize (UILayoutFittingCompressedSize).Height;
			height += 1;

			return height;
		}

		public virtual void OnSelected (UITableView tableView, NSIndexPath indexPath) { }

		public virtual void OnDeselected (UITableView tableView, NSIndexPath indexPath) { }

		public virtual void OnCanExecute (bool canExecute, GenericAction action) { }

		protected void OnTouchUpInside (object sender, GenericAction action)
		{
			if(TouchUpAction != null)
			{
				TouchUpAction (sender, action);
			}
		}

		public static string GetIdentifier (GenericActionType type, NodeSection section)
		{
			var identifier = "";

			// determine which cell to inflate depending on the action type
			switch (type)
			{
			case GenericActionType.StandardAction:
				if (section.SectionActions.Count() == 1) {
					identifier = SurveyCellOneButton.Key;
				} else if (section.SectionActions.Count() == 2) {
					identifier = SurveyCellTwoButton.Key;
				} else {
					identifier = SurveyCellThreeButton.Key;
				}
				break;

			case GenericActionType.BooleanAction:
				identifier = SurveyCellCheckmark.Key;
				break;

			case GenericActionType.TextAction:
				if (section.SectionActions.Count() == 1) {
					identifier = SurveyCellOneButtonLink.Key;
				} else {
					identifier = SurveyCellTwoButtonLink.Key;
				}
				break;

			case GenericActionType.Seperator:
				identifier = SurveyCellSeparator.Key;
				break;

			case GenericActionType.CustomAction:
				identifier = SurveyCellDefault.Key;
				break;
			}
			return identifier;
		}

		public static string GetIdentifier (GenericAction action, NodeSection section)
		{
			var type = action.ActionType;

			return GetIdentifier (type, section);
		}

		public static string GetIdentifier(NodeSection section)
		{
			var type = section.SectionActions.ElementAt (0).ActionType;

			return GetIdentifier (type, section);
		}

		#endregion
	}
}