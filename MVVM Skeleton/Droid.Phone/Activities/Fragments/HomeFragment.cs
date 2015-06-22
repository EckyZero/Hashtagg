
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using Shared.VM;
using System.Collections.ObjectModel;

namespace Droid.Phone
{
	public class HomeFragment : Android.Support.V4.App.Fragment
	{
		Button _refreshButton;
		Button _twitterButton;
		Button _facebookButton;
		HomeViewModel _viewModel;
		ListView _listLayout;
		LayoutInflater _inflater;

		public HomeFragment(HomeViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			_inflater = inflater;

			var viewGroup = inflater.Inflate (Resource.Layout.Home, container, false);
			_listLayout = viewGroup.FindViewById<ListView> (Resource.Id.HomeListView);
			var header = inflater.Inflate (Resource.Layout.HomeHeader, null, false);
			_listLayout.AddHeaderView(header);
			_listLayout.Adapter = new HomeListAdapter<BaseCardViewModel> (_viewModel.CardViewModels,  inflater, _listLayout);
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			_refreshButton = header.FindViewById<Button> (Resource.Id.RefreshButton);
			_twitterButton = header.FindViewById<Button> (Resource.Id.TwitterButton);
			_facebookButton = header.FindViewById<Button> (Resource.Id.FacebookButton);
			_refreshButton.SetCommand("Click", _viewModel.RefreshCommand);
			_twitterButton.SetCommand("Click", _viewModel.TwitterCommand);
			_facebookButton.SetCommand ("Click", _viewModel.FacebookCommand);
			return viewGroup;
		}

		private class HomeListAdapter<T> : ObservableAdapter<T>
		{
			LayoutInflater _inflater;
			ListView _listView;

			public HomeListAdapter(ObservableCollection<T> dataSource, LayoutInflater inflater, ListView listView)
			{
				_inflater = inflater;
				_listView = listView;
				DataSource = dataSource;
				GetTemplateDelegate = GetCell;
			}

			private View GetCell(int position, T vm, View convertView)
			{
				var cell = _inflater.Inflate(Resource.Layout.TwitterCell, _listView, false);
				var label = cell.FindViewById<TextView>(Resource.Id.TweetCellTweetBody);
				label.Text = vm.ToString();
				return cell;
			}
		}
	}
}

