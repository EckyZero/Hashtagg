
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
using System.ComponentModel;
using Android.Graphics;
using Android.Support.V4.Widget;
using Android.Text;
using Droid.Controls;
using Droid.UIHelpers;
using Koush;
using Shared.Common.Helpers;

namespace Droid.Phone
{
	public class HomeFragment : Android.Support.V4.App.Fragment
	{
		HomeViewModel _viewModel;
		ListView _listLayout;
		LayoutInflater _inflater;
        private Android.Views.View header;
	    private SwipeRefreshLayout _swipeLayout;
	    private bool _init;
	    private bool _loaded;
	    private TextView _usernameText;
        private List<CircularImageView> _headerImages;

	    public HomeFragment(HomeViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			_inflater = inflater;

			var viewGroup = inflater.Inflate (Resource.Layout.Home, container, false);
		    _swipeLayout = viewGroup.FindViewById<SwipeRefreshLayout>(Resource.Id.HomeSwipeRefreshLayout);
			_listLayout = viewGroup.FindViewById<ListView> (Resource.Id.HomeListView);
            header = inflater.Inflate(Resource.Layout.HomeHeader, _listLayout, false);
			_listLayout.AddHeaderView(header);
			_listLayout.Adapter = new HomeListAdapter<IListItem> (_viewModel.CardViewModels,  inflater, _listLayout);
		    _usernameText = header.FindViewById<TextView>(Resource.Id.HomeHeaderUsernameText);
            _usernameText.Text = string.Empty;
            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
            _swipeLayout.Refresh += SwipeLayoutOnRefresh;
            _viewModel.RequestCompleted += OnRequestCompleted;

            _headerImages = new List<CircularImageView>()
            {
                header.FindViewById<CircularImageView>(Resource.Id.HomeHeaderImage1),
                header.FindViewById<CircularImageView>(Resource.Id.HomeHeaderImage2),
                header.FindViewById<CircularImageView>(Resource.Id.HomeHeaderImage3)
            };

            _viewModel.RequestHeaderImages = OnRequestHeaderImages;
            viewGroup.ViewTreeObserver.GlobalLayout += ViewTreeObserverOnGlobalLayout;

			return viewGroup;
		}

        private void OnRequestHeaderImages(List<string> urls)
        {
            var i = 0;
            var shift = urls.Count > 1 ? -12 : 0;
            foreach (var url in urls)
            {
                _headerImages[i].SetX(_headerImages[i].GetX() + TypedValue.ApplyDimension(ComplexUnitType.Dip, shift, Application.Context.ApplicationContext.Resources.DisplayMetrics));
                UrlImageViewHelper.SetUrlDrawable(_headerImages[i], url, Resource.Drawable.Profile_Image_Default, new CircularImageShadowCallback() );
                shift += 24;
                i++;
            }
            for (i = i; i < _headerImages.Count; i++)
                _headerImages[i].Visibility = ViewStates.Gone;
        }

	    private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
	    {
	        switch (propertyChangedEventArgs.PropertyName)
	        {
	            case "Title":
	                _usernameText.Text = _viewModel.Title;
	                break;
	        }
	    }

	    private void OnRequestCompleted()
	    {
	        _swipeLayout.Refreshing = false;
	    }

	    private void SwipeLayoutOnRefresh(object sender, EventArgs eventArgs)
	    {
	        _viewModel.RefreshCommand.Execute(null);
	    }

	    private async void ViewTreeObserverOnGlobalLayout(object sender, EventArgs eventArgs)
	    {
	        
            if (_init && !_loaded)
	        {
	            _loaded = true;

                _listLayout.OverScrollMode = OverScrollMode.Never;
                _swipeLayout.SetProgressViewOffset(false, header.Height - header.FindViewById<RelativeLayout>(Resource.Id.HomeHeaderPaddingLayout).Height, (int)Math.Ceiling(header.Height + _swipeLayout.Height * 0.01));
                _swipeLayout.SetColorSchemeResources(Resource.Color.carnation);

                await _viewModel.DidAppear();
                return;
            }
            _init = true;

	    }

	    public class CircularImageShadowCallback : Java.Lang.Object, IUrlImageViewCallback
	    {

            public void OnLoaded(ImageView p0, Android.Graphics.Bitmap p1, string p2, bool p3)
            {
                (p0 as CircularImageView).SetShadow(5f, 1f, 2f, Color.DarkSlateGray);
                (p0 as CircularImageView).SetBorderColor(Color.White);
                (p0 as CircularImageView).Invalidate();
            }
        }

	    private class HomeListAdapter<T> : ObservableAdapter<T> where T : IListItem
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
                
                switch (vm.ListItemType)
                {
                    case ListItemType.Default:
                        return ProcessSocialCard(position, vm as BaseContentCardViewModel, convertView);
                }
				var cell = _inflater.Inflate(Resource.Layout.DefaultCell, _listView, false);
				return cell;
			}

            private View ProcessSocialCard(int position, BaseContentCardViewModel cardViewModel, View convertView)
            {
                switch (cardViewModel.SocialType)
                {
                    case SocialType.Facebook:
                        return ProcessFacebook(position, cardViewModel as FacebookCardViewModel, convertView);
                        break;
                    case SocialType.Twitter:
                        return ProcessTwitter(position, cardViewModel as TwitterCardViewModel, convertView);
                        break;
                    case SocialType.None:
                        throw new NotImplementedException();
                        break;
                    default:
                        throw new NotImplementedException();
                        break;
                }          
            }

            private View ProcessFacebook(int position, FacebookCardViewModel fbVm, View convertView)
            {
                if ( convertView == null || convertView.Id != Resource.Id.DefaultCellMainLayout)
                    convertView = _inflater.Inflate(Resource.Layout.DefaultCell, null, false);
                var mainImage = convertView.FindViewById<ImageView>(Resource.Id.DefaultCellMainImage);
                var profileImage = convertView.FindViewById<CircularImageView>(Resource.Id.DefaultCellProfileImage);
                convertView.FindViewById<TextView>(Resource.Id.DefaultCellMainText).TextFormatted = Html.FromHtml(fbVm.Text);
                convertView.FindViewById<CircularImageView>(Resource.Id.DefaultCellSocialImage).SetImageResource(Resource.Drawable.Facebook);

                var likeButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellLikeButton);
                var commentButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellCommentButton);
                var shareButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellShareButton);

                likeButton.Text = fbVm.LikeButtonText;
                likeButton.SetCommand("Click", fbVm.LikeCommand);

                commentButton.Text = fbVm.CommentButtonText;
                commentButton.SetCommand("Click", fbVm.CommentCommand);

                shareButton.Text = fbVm.ShareButtonText;
                shareButton.SetCommand("Click", fbVm.ShareCommand);

                if (!string.IsNullOrWhiteSpace(fbVm.UserImageUrl))
                {
                    UrlImageViewHelper.SetUrlDrawable(profileImage, fbVm.UserImageUrl, SharedDrawableHelpers.GetSharedDrawableResourceIdViaReflection(fbVm.UserImagePlaceholder));
                }
                else
                {
                    profileImage.SetImageResource(SharedDrawableHelpers.GetSharedDrawableResourceIdViaReflection(fbVm.UserImagePlaceholder));
                }

                if (fbVm.ShowImage)
                {
                    mainImage.Visibility = ViewStates.Visible;
                    UrlImageViewHelper.SetUrlDrawable(mainImage, fbVm.ImageUrl);
                }
                else
                {
                    mainImage.Visibility = ViewStates.Gone;
                }

                return convertView;
            }

            private View ProcessTwitter(int position, TwitterCardViewModel tVm, View convertView)
            {
                if (convertView == null || convertView.Id != Resource.Id.DefaultCellMainLayout)
                    convertView = _inflater.Inflate(Resource.Layout.DefaultCell, null, false);

                var mainImage = convertView.FindViewById<ImageView>(Resource.Id.DefaultCellMainImage);
                var profileImage = convertView.FindViewById<CircularImageView>(Resource.Id.DefaultCellProfileImage);
                convertView.FindViewById<TextView>(Resource.Id.DefaultCellMainText).TextFormatted = Html.FromHtml(tVm.Text);
                convertView.FindViewById<CircularImageView>(Resource.Id.DefaultCellSocialImage).SetImageResource(Resource.Drawable.Twitter);

                var likeButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellLikeButton);
                var commentButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellCommentButton);
                var shareButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellShareButton);

                likeButton.Text = tVm.LikeButtonText;
                likeButton.SetCommand("Click", tVm.LikeCommand);

                commentButton.Text = tVm.CommentButtonText;
                commentButton.SetCommand("Click", tVm.CommentCommand);

                shareButton.Text = tVm.ShareButtonText;
                shareButton.SetCommand("Click", tVm.ShareCommand);

                if (!string.IsNullOrWhiteSpace(tVm.UserImageUrl))
                {
                    UrlImageViewHelper.SetUrlDrawable(profileImage, tVm.UserImageUrl, SharedDrawableHelpers.GetSharedDrawableResourceIdViaReflection(tVm.UserImagePlaceholder));
                }
                else
                {
                    profileImage.SetImageResource(SharedDrawableHelpers.GetSharedDrawableResourceIdViaReflection(tVm.UserImagePlaceholder));
                }

                if (tVm.ShowImage)
                {
                    mainImage.Visibility = ViewStates.Visible;
                    UrlImageViewHelper.SetUrlDrawable(mainImage, tVm.ImageUrl);
                }
                else
                {
                    mainImage.Visibility = ViewStates.Gone;
                }

                return convertView;
            }

		}
	}
}

