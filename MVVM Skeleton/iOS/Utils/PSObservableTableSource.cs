using System;
using UIKit;
using Foundation;
using Shared.Common;
using CoreGraphics;
using System.Collections.Generic;
using System.Collections;
using Shared.VM;

namespace iOS
{
	public class PSObservableTableSource<T> where T : IListItem
	{
		#region Private Variables

		ExtendedObservableTableViewController<T> _controller;
		ObservableRangeCollection<T> _collection;

		#endregion

		#region Actions/Funcs/Events

		public event EventHandler SelectionChanged {
			add { _controller.SelectionChanged += value; }
			remove { _controller.SelectionChanged -= value; 
			}
		}

		public Action<UITableViewCell, T, NSIndexPath> BindCellDelegate {
			get { return _controller.BindCellDelegate; }
			set { _controller.BindCellDelegate = value; } 
		}

		public Func<NSString, UITableView, NSIndexPath, UITableViewCell> CreateCellDelegate { 
			get { return _controller.CreateCellDelegate; }
			set { _controller.CreateCellDelegate = value; }
		}

		public Func<UITableView, int, UIView> GetViewForHeaderDelegate { 
			get { return _controller.GetViewForHeaderDelegate; }
			set { _controller.GetViewForHeaderDelegate = value; }
		}

		public Func<UITableView, int, float> GetHeightForHeaderDelegate { 
			get { return _controller.GetHeightForHeaderDelegate; }
			set { _controller.GetHeightForHeaderDelegate = value; }
		}

		public Func<UITableView, int, UIView> GetViewForFooterDelegate { 
			get { return _controller.GetViewForFooterDelegate; }
			set { _controller.GetViewForFooterDelegate = value; }
		}

		public Func<UITableView, int, float> GetHeightForFooterDelegate { 
			get { return _controller.GetHeightForFooterDelegate; }
			set { _controller.GetHeightForFooterDelegate = value; }
		}

		public Func<UITableView, NSIndexPath, float> GetHeightForRowDelegate { 
			get { return _controller.GetHeightForRowDelegate; }
			set { _controller.GetHeightForRowDelegate = value; }
		}

		#endregion

		public PSObservableTableSource (ExtendedObservableTableViewController<T> controller, ObservableRangeCollection<T> collection)
		{
			_controller = controller;
			_collection = collection;

			InitBindings ();
		}

		private void InitBindings ()
		{
			_controller.DataSource = _collection;
			_controller.SelectionChanged += OnSelectionChanged;

			InitBindCellDelegate ();
			InitCreateCellDelegate ();
//			InitGetHeightForRowDelegate ();

			InitGetViewForHeaderDelegate ();
			InitGetHeightForHeaderDelegate ();
			InitGetViewForFooterDelegate ();
			InitGetHeightForFooterDelegate ();
		}

		private void OnSelectionChanged (object sender, EventArgs e)
		{
			var item = (IListItem)sender;
			item.Selected ();
		}

		private void InitBindCellDelegate ()
		{
			BindCellDelegate = ((UITableViewCell cell, T model, NSIndexPath indexPath) => {
			
				var item = (IListItem)_collection[indexPath.Row];
				var baseCell = cell as BaseCell;

				baseCell.Configure(item);
			});

			_controller.BindCellDelegate = BindCellDelegate;
		}

		private void InitCreateCellDelegate ()
		{
			CreateCellDelegate = ((NSString id, UITableView tableView, NSIndexPath indexPath) => {

				var item = _collection[indexPath.Row];
				var identifier = item.ListItemType.ToString();
				var cell = tableView.DequeueReusableCell(identifier);

				return cell;
			});

			_controller.CreateCellDelegate = CreateCellDelegate;
		}

		private void InitGetHeightForRowDelegate ()
		{
			GetHeightForRowDelegate = ((UITableView tableView, NSIndexPath indexPath) => {
				return (float)UITableView.AutomaticDimension;
			});

			_controller.GetHeightForRowDelegate = GetHeightForRowDelegate;
		}

		private void InitGetViewForHeaderDelegate ()
		{
			GetViewForHeaderDelegate = ((UITableView tableView, int section) => {
				return _controller.GetViewForHeader(tableView, section);
			});

			_controller.GetViewForHeaderDelegate = GetViewForHeaderDelegate;
		}

		private void InitGetHeightForHeaderDelegate ()
		{
			GetHeightForHeaderDelegate = ((UITableView tableView, int section) => {
				return (float)_controller.GetHeightForHeader(tableView, section);
			});

			_controller.GetHeightForHeaderDelegate = GetHeightForHeaderDelegate;
		}

		private void InitGetViewForFooterDelegate ()
		{
			GetViewForFooterDelegate = ((UITableView tableView, int section) => {
				return _controller.GetViewForFooter(tableView, section);
			});

			_controller.GetViewForFooterDelegate = GetViewForFooterDelegate;
		}

		private void InitGetHeightForFooterDelegate ()
		{
			GetHeightForFooterDelegate = ((UITableView tableView, int section) => {
				return (float)_controller.GetHeightForFooter(tableView, section);
			});

			_controller.GetHeightForFooterDelegate = GetHeightForFooterDelegate;
		}
	}
}

