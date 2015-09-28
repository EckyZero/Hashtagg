
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
using Android.Views;

namespace Droid.Phone
{
	public class HomeFragment : BaseFragment
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
            _viewModel.RequestPostPage = OnRequestPostPage;
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

        void OnRequestPostPage(PostViewModel pVm)
        {
            NavigationService.NavigateTo(ViewModelLocator.POST_KEY, pVm, null, Shared.Common.AnimationFlag.Up);
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

            private class BaseCardViewHolder : Java.Lang.Object
            {
                public ImageView MainImage { get; set; }
                public CircularImageView ProfileImage { get; set; }
                public TextView UserName { get; set; }
                public TextView BodyText { get; set; }
                public CircularImageView SocialImage { get; set; }
                public Button LikeButton { get; set; }
                public Button CommentButton { get; set; }
                public Button ShareButton { get; set; }
                public BaseContentCardViewModel LinkedVM { get; set; }
                public EventHandler LikeEventHandler { get ;set;}
                public EventHandler CommentEventHandler { get ;set;}
                public EventHandler ShareEventHandler { get ;set;}
                public BaseCardViewHolder(){}
            }

            private View ProcessSocialCard(int position, BaseContentCardViewModel cardViewModel, View convertView)
            {
                BaseCardViewHolder viewHolder = null;
                if (convertView == null || convertView.Id != Resource.Id.DefaultCellMainLayout)
                {
                    convertView = _inflater.Inflate(Resource.Layout.DefaultCell, null, false);

                    viewHolder = new BaseCardViewHolder()
                    {
                        MainImage = convertView.FindViewById<ImageView>(Resource.Id.DefaultCellMainImage),
                        ProfileImage = convertView.FindViewById<CircularImageView>(Resource.Id.DefaultCellProfileImage),
                        UserName = convertView.FindViewById<TextView>(Resource.Id.DefaultCellUserName),
                        BodyText = convertView.FindViewById<TextView>(Resource.Id.DefaultCellMainText),
                        SocialImage = convertView.FindViewById<CircularImageView>(Resource.Id.DefaultCellSocialImage),
                        LikeButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellLikeButton),
                        CommentButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellCommentButton),
                        ShareButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellShareButton),
                        LinkedVM = cardViewModel
                    };
                    
                    convertView.Tag = viewHolder;
                }
                else
                {
                    viewHolder = convertView.Tag as BaseCardViewHolder;
                    if (viewHolder == null)
                        return convertView;
                    if (viewHolder.LinkedVM != cardViewModel)
                    {
                        viewHolder.LikeButton.Click -= viewHolder.LikeEventHandler;
                        viewHolder.CommentButton.Click -= viewHolder.CommentEventHandler;
                        viewHolder.ShareButton.Click -= viewHolder.ShareEventHandler;
                    }
                    else
                    {
                        return convertView;
                    }
                    viewHolder.LikeEventHandler = null;
                    viewHolder.CommentEventHandler = null;
                    viewHolder.ShareEventHandler = null;
                }

                if (viewHolder.LikeEventHandler == null)
                {
                    viewHolder.LikeEventHandler = (object sender, EventArgs e) => cardViewModel.LikeCommand.Execute(null);
                    viewHolder.LikeButton.Click += viewHolder.LikeEventHandler;
                }
                if (viewHolder.CommentEventHandler == null)
                {
                    viewHolder.CommentEventHandler = (object sender, EventArgs e) => cardViewModel.CommentCommand.Execute(null);
                    viewHolder.CommentButton.Click += viewHolder.CommentEventHandler;
                }
                if (viewHolder.ShareEventHandler == null)
                {
                    viewHolder.ShareEventHandler = (object sender, EventArgs e) => cardViewModel.ShareCommand.Execute(null);
                    viewHolder.ShareButton.Click += viewHolder.ShareEventHandler;
                }
                
                viewHolder.UserName.Text = cardViewModel.UserName;
                viewHolder.BodyText.TextFormatted = Html.FromHtml(cardViewModel.Text);
                viewHolder.SocialImage.SetImageResource(DrawableHelpers.GetDrawableResourceIdViaReflection(cardViewModel.SocialMediaImage));
                viewHolder.LikeButton.Text = cardViewModel.LikeButtonText;
                viewHolder.CommentButton.Text = cardViewModel.CommentButtonText;
                viewHolder.ShareButton.Text = cardViewModel.ShareButtonText;

                if (!string.IsNullOrWhiteSpace(cardViewModel.UserImageUrl))
                    UrlImageViewHelper.SetUrlDrawable(viewHolder.ProfileImage, cardViewModel.UserImageUrl, SharedDrawableHelpers.GetSharedDrawableResourceIdViaReflection(cardViewModel.UserImagePlaceholder));
                else
                    viewHolder.ProfileImage.SetImageResource(SharedDrawableHelpers.GetSharedDrawableResourceIdViaReflection(cardViewModel.UserImagePlaceholder));

                if (cardViewModel.ShowImage)
                {
                    viewHolder.MainImage.Visibility = ViewStates.Visible;
                    UrlImageViewHelper.SetUrlDrawable(viewHolder.MainImage, cardViewModel.ImageUrl);
                }
                else
                    viewHolder.MainImage.Visibility = ViewStates.Gone;

                return convertView;
            }
		}
	}
}

