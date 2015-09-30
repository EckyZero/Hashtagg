
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Droid.Controls;
using Shared.VM;
using System.ComponentModel;
using Koush;
using Droid.UIHelpers;
using Android.Text;

namespace Droid.Phone
{
    public static class AdapterHelpers
    {
        public class BaseCardViewHolder : Java.Lang.Object
        {
            public RelativeLayout CommentLineContainer { get; set; }
            public ImageView MainImage { get; set; }
            public CircularImageView ProfileImage { get; set; }
            public TextView UserName { get; set; }
            public TextView DateText { get; set;}
            public TextView BodyText { get; set; }
            public CircularImageView SocialImage { get; set; }
            public Button LikeButton { get; set; }
            public Button CommentButton { get; set; }
            public Button ShareButton { get; set; }
            public BaseContentCardViewModel LinkedVM { get; set; }
            public PropertyChangedEventHandler PropertyChangedEventHandler {get;set;}
            public EventHandler LikeEventHandler { get ;set;}
            public EventHandler CommentEventHandler { get ;set;}
            public EventHandler ShareEventHandler { get ;set;}
            public BaseCardViewHolder(){}
        }

        public class HeaderCardViewHolder : Java.Lang.Object
        {
            
        }



        public static View ProcessSocialCard(int position, BaseContentCardViewModel cardViewModel, View convertView, LayoutInflater inflater)
        {
            BaseCardViewHolder viewHolder = null;
            if (convertView == null || convertView.Id != Resource.Id.DefaultCellMainLayout)
            {
                convertView = inflater.Inflate(Resource.Layout.DefaultCell, null, false);

                viewHolder = new BaseCardViewHolder()
                    {
                        MainImage = convertView.FindViewById<ImageView>(Resource.Id.DefaultCellMainImage),
                        ProfileImage = convertView.FindViewById<CircularImageView>(Resource.Id.DefaultCellProfileImage),
                        UserName = convertView.FindViewById<TextView>(Resource.Id.DefaultCellUserName),
                        DateText = convertView.FindViewById<TextView>(Resource.Id.DefaultCellDateText),
                        BodyText = convertView.FindViewById<TextView>(Resource.Id.DefaultCellMainText),
                        SocialImage = convertView.FindViewById<CircularImageView>(Resource.Id.DefaultCellSocialImage),
                        LikeButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellLikeButton),
                        CommentButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellCommentButton),
                        ShareButton = convertView.FindViewById<Button>(Resource.Id.DefaultCellShareButton),
                        CommentLineContainer = convertView.FindViewById<RelativeLayout>(Resource.Id.DefaultCellCommentLineContainer),
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
                    viewHolder.LinkedVM.PropertyChanged -= viewHolder.PropertyChangedEventHandler;
                    viewHolder.LikeButton.Click -= viewHolder.LikeEventHandler;
                    viewHolder.CommentButton.Click -= viewHolder.CommentEventHandler;
                    viewHolder.ShareButton.Click -= viewHolder.ShareEventHandler;
                }
                else
                {
                    return convertView;
                }
                viewHolder.LinkedVM = cardViewModel;
                viewHolder.PropertyChangedEventHandler = null;
                viewHolder.LikeEventHandler = null;
                viewHolder.CommentEventHandler = null;
                viewHolder.ShareEventHandler = null;
            }

            if (viewHolder.LikeEventHandler == null)
            {
                viewHolder.LikeEventHandler = (object sender, EventArgs e) =>
                    {
                        viewHolder.LikeButton.Selected =  !viewHolder.LikeButton.Selected ;  
                        cardViewModel.LikeCommand.Execute(null);
                    };
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
            if (viewHolder.PropertyChangedEventHandler == null)
            {
                viewHolder.PropertyChangedEventHandler = (object sender, PropertyChangedEventArgs e) =>
                    {
                        switch(e.PropertyName)
                        {
                            case "IsLikedByUser":
                            case "LikeButtonText":
                            case "LikeButtonTextColor":
                            case "ShowLikeButton":
                                viewHolder.LikeButton.Text = cardViewModel.LikeButtonText;
                                viewHolder.LikeButton.SetTextColor(cardViewModel.LikeButtonTextColor.ToDroidColor());
                                viewHolder.LikeButton.Visibility = cardViewModel.ShowLikeButton ? ViewStates.Visible : ViewStates.Invisible;
                                break;

                            case "IsCommentedByUser":
                            case "CommentButtonText":
                            case "CommentButtonTextColor":
                            case "ShowCommentButton":
                                viewHolder.CommentButton.Text = cardViewModel.CommentButtonText;
                                viewHolder.CommentButton.SetTextColor(cardViewModel.CommentButtonTextColor.ToDroidColor());
                                viewHolder.CommentButton.Visibility = cardViewModel.ShowCommentButton ? ViewStates.Visible : ViewStates.Invisible;
                                break;

                            case "IsSharedByUser":
                            case "ShareButtonText":
                            case "ShareButtonTextColor":
                            case "ShowShareButton":
                                viewHolder.ShareButton.Text = cardViewModel.ShareButtonText;
                                viewHolder.ShareButton.SetTextColor(cardViewModel.ShareButtonTextColor.ToDroidColor());
                                viewHolder.ShareButton.Visibility = cardViewModel.ShowShareButton ? ViewStates.Visible : ViewStates.Invisible;
                                break;
                            
                            case "ShowDateTime":
                            case "DisplayDateTime":
                                viewHolder.DateText.Visibility = cardViewModel.ShowDateTime ? ViewStates.Visible : ViewStates.Gone;
                                viewHolder.DateText.Text = cardViewModel.DisplayDateTime;
                                break;
                        }      

                    };
                cardViewModel.PropertyChanged += viewHolder.PropertyChangedEventHandler;
            }
            viewHolder.UserName.Text = cardViewModel.UserName;
            viewHolder.BodyText.TextFormatted = Html.FromHtml(cardViewModel.Text);
            viewHolder.SocialImage.Visibility = cardViewModel.ShowSocialMediaImage ? ViewStates.Visible : ViewStates.Invisible;
            viewHolder.SocialImage.SetImageResource(DrawableHelpers.GetDrawableResourceIdViaReflection(cardViewModel.SocialMediaImage));

            viewHolder.LikeButton.Text = cardViewModel.LikeButtonText;
            viewHolder.LikeButton.SetTextColor(cardViewModel.LikeButtonTextColor.ToDroidColor());
            viewHolder.LikeButton.Visibility = cardViewModel.ShowLikeButton ? ViewStates.Visible : ViewStates.Invisible;

            viewHolder.CommentButton.Text = cardViewModel.CommentButtonText;
            viewHolder.CommentButton.SetTextColor(cardViewModel.CommentButtonTextColor.ToDroidColor());
            viewHolder.CommentButton.Visibility = cardViewModel.ShowCommentButton ? ViewStates.Visible : ViewStates.Invisible;

            viewHolder.ShareButton.Text = cardViewModel.ShareButtonText;
            viewHolder.ShareButton.SetTextColor(cardViewModel.ShareButtonTextColor.ToDroidColor());
            viewHolder.ShareButton.Visibility = cardViewModel.ShowShareButton ? ViewStates.Visible : ViewStates.Invisible;

            viewHolder.DateText.Visibility = cardViewModel.ShowDateTime ? ViewStates.Visible : ViewStates.Gone;
            viewHolder.DateText.Text = cardViewModel.DisplayDateTime;

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

            viewHolder.CommentLineContainer.Visibility = cardViewModel.ShowTimeline ? ViewStates.Visible : ViewStates.Gone;

            return convertView;
        }

    }
}

