using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.VM;
using System.Threading.Tasks;
using Android.Animation;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Support.V4.Graphics.Drawable;
using Android.Util;
using Android.Views.Animations;
using GalaSoft.MvvmLight.Helpers;
using Shared.VM;
using System.ComponentModel;

namespace Droid.Phone.Activities
{
    [Activity(Label = "HashTagg", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : BaseActivity
    {
        private RelativeLayout _mainLayout;
        private bool _init;
        private TextView _appName;
        private TextView _whatAccounts;
        private RelativeLayout _appNameLayout;
        private Button _facebookButton;
        private Button _twitterButton;
        private OnboardingViewModel _viewModel;
        private Button _goButton;
        private MainViewModel _splashViewModel;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SplashLayout);

            _mainLayout = FindViewById<RelativeLayout>(Resource.Id.SplashMainRelativeLayout);
            _appName = FindViewById<TextView>(Resource.Id.SplashAppName);
            _whatAccounts = FindViewById<TextView>(Resource.Id.SplashWhatAccounts);
            _appNameLayout = FindViewById<RelativeLayout>(Resource.Id.SplashAppNameLayout);
            _facebookButton = FindViewById<Button>(Resource.Id.SplashFacebookButton);
            _twitterButton = FindViewById<Button>(Resource.Id.SplashTwitterButton);
            _goButton = FindViewById<Button>(Resource.Id.SplashGoButton);

            InitUi();

            _splashViewModel = new MainViewModel();
            _splashViewModel.RequestHomePage = OnRequestHomePage;
            _splashViewModel.RequestOnboardingPage = OnRequestOnboardingPage;
            _mainLayout.ViewTreeObserver.GlobalLayout += ViewTreeObserverOnGlobalLayout;
        }

        private void BindEvents()
        {

            _viewModel.CanExecute += b =>
            {
                _goButton.Enabled = b;
            };
            _twitterButton.SetCommand("Click", _viewModel.TwitterCommand);
            _facebookButton.SetCommand("Click", _viewModel.FacebookCommand);
            _goButton.SetCommand("Click", _viewModel.GoCommand);

            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
            _viewModel.RequestHomePage += OnRequestHomePage;  
        }

        private async void OnRequestCommentPage(CommentViewModel cVm)
        {
            await cVm.DidLoad();
            _navigationService.NavigateTo(ViewModelLocator.COMMENT_KEY, cVm);
        }

        private async void OnRequestHomePage(HomeViewModel hVm)
        {
            //Did Load will assign through Actions to Post view models, we need to assign this action early, but it is safe due to shared services
            hVm.RequestCommentPage = OnRequestCommentPage;

            await hVm.DidLoad();
            //TODO add back in the grow animation
            _navigationService.NavigateTo(ViewModelLocator.HOME_KEY, hVm, null, Shared.Common.AnimationFlag.Grow);
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "IsTwitterSelected":
                    _twitterButton.Selected = _viewModel.IsTwitterSelected;
                    break;
                case "IsFacebookSelected":
                    _facebookButton.Selected = _viewModel.IsFacebookSelected;
                    break;
            }
        }

        private void InitUi()
        {
            _whatAccounts.Alpha = 0;
            _facebookButton.Alpha = 0;
            _twitterButton.Alpha = 0;
            _goButton.Alpha = 0;
            _goButton.Enabled = false;
        }

        private void PrepareGoButtonStates()
        {
            int[][] states = new int[][]
                {
                    new int[] {Android.Resource.Attribute.StatePressed, Android.Resource.Attribute.StateEnabled}, // Pressed
                    new int[] {-Android.Resource.Attribute.StatePressed, Android.Resource.Attribute.StateEnabled}, // not pressed
                    new int[] {-Android.Resource.Attribute.StateEnabled} // disabled
                };

            int[] goTextColors =
                {
                    Resources.GetColor(Resource.Color.silver),
                    Resources.GetColor(Resource.Color.blue2),
                    Resources.GetColor(Resource.Color.silver),
                };

            var textColorStateList = new ColorStateList(states, goTextColors);

            _goButton.SetTextColor(textColorStateList);
        }


        private void PrepareMediaDrawable(Button mediaButton)
        {
            int[][] states = new int[][]
                {
                    new int[] {Android.Resource.Attribute.StateSelected }, // disabled
                    new int[] {Android.Resource.Attribute.StatePressed, Android.Resource.Attribute.StateEnabled}, // Pressed
                    new int[] {-Android.Resource.Attribute.StatePressed, Android.Resource.Attribute.StateEnabled}, // not pressed
                    new int[] {-Android.Resource.Attribute.StateEnabled} // disabled
                };

            int[] goTextColors =
                {
                    Resources.GetColor(Resource.Color.white),
                    Resources.GetColor(Resource.Color.carnation),
                    Resources.GetColor(Resource.Color.carnation),
                    Resources.GetColor(Resource.Color.silver),
                };

            var textColorStateList = new ColorStateList(states, goTextColors);

            mediaButton.SetTextColor(textColorStateList);
            mediaButton.RequestLayout();
        }

        private async void ViewTreeObserverOnGlobalLayout(object sender, EventArgs eventArgs)
        {
            if (_init)
                return;
            _init = true;
            PrepareGoButtonStates();
            PrepareMediaDrawable(_twitterButton);
            PrepareMediaDrawable(_facebookButton);

            await _splashViewModel.DidLoad();
            await _splashViewModel.DidAppear();
        }

        private void AnimationInitUI()
        {
            _twitterButton.Selected = _viewModel.IsTwitterSelected;
            _facebookButton.Selected = _viewModel.IsFacebookSelected;    
        }

        private async void OnRequestOnboardingPage(OnboardingViewModel oVm)
        {
            _viewModel = oVm;
            AnimationInitUI();
            BindEvents();
            
            var mainAnimatorSet = new AnimatorSet();

            var appNameLayoutFinalTopSpace = TypedValue.ApplyDimension(ComplexUnitType.Dip, 55.0f, Application.Context.Resources.DisplayMetrics);

            var decelerateInterpolator = new DecelerateInterpolator(1.0f);

            var appNameLayoutAnimator = new ValueAnimator();
            appNameLayoutAnimator.SetDuration(750);
            appNameLayoutAnimator.SetInterpolator(decelerateInterpolator);
            appNameLayoutAnimator.SetFloatValues(_appNameLayout.GetY(), appNameLayoutFinalTopSpace);
            appNameLayoutAnimator.Update += (o, args) => { _appNameLayout.SetY((float)args.Animation.AnimatedValue); };

            var whatAccountsAnimator = new ValueAnimator();
            whatAccountsAnimator.SetDuration(750);
            whatAccountsAnimator.SetInterpolator(decelerateInterpolator);
            whatAccountsAnimator.SetFloatValues(0.0f, 1.0f);
            whatAccountsAnimator.Update += (o, args) => { _whatAccounts.Alpha = (float)args.Animation.AnimatedValue; };

            var appNameAnimationSet = new AnimatorSet();
            appNameAnimationSet.PlayTogether(appNameLayoutAnimator, whatAccountsAnimator);


            var socialButtonsAnimatorSet = new AnimatorSet();

            var overshootInterpolator = new OvershootInterpolator(3f);

            var facebookButtonAnimator = new ValueAnimator();
            facebookButtonAnimator.SetDuration(500);
            facebookButtonAnimator.SetInterpolator(overshootInterpolator);
            facebookButtonAnimator.SetFloatValues(_facebookButton.GetY() + _facebookButton.Height, _facebookButton.GetY());
            facebookButtonAnimator.Update += (o, args) =>
                {
                    _facebookButton.SetY((float)args.Animation.AnimatedValue);
                    _facebookButton.Alpha = args.Animation.AnimatedFraction;
                };

            var twitterButtonAnimator = new ValueAnimator();
            twitterButtonAnimator.SetDuration(500);
            twitterButtonAnimator.SetInterpolator(overshootInterpolator);
            twitterButtonAnimator.SetFloatValues(_facebookButton.GetY() + _facebookButton.Height, _facebookButton.GetY());
            twitterButtonAnimator.Update += (o, args) =>
                {
                    _twitterButton.SetY((float)args.Animation.AnimatedValue);
                    _twitterButton.Alpha = args.Animation.AnimatedFraction;
                };
            socialButtonsAnimatorSet.PlaySequentially(facebookButtonAnimator, twitterButtonAnimator);
            socialButtonsAnimatorSet.StartDelay = 500;

            var letsGoButtonAnimator = new ValueAnimator();
            letsGoButtonAnimator.SetDuration(500);
            letsGoButtonAnimator.SetInterpolator(decelerateInterpolator);
            letsGoButtonAnimator.SetFloatValues(0.0f, 1.0f);
            letsGoButtonAnimator.Update += (sender, args) =>
                {
                    _goButton.Alpha = (float)args.Animation.AnimatedValue;
                };

            mainAnimatorSet.PlaySequentially(appNameAnimationSet, socialButtonsAnimatorSet, letsGoButtonAnimator);

            await _viewModel.DidLoad();

            await Task.Delay(2000);

            mainAnimatorSet.Start();
            await _viewModel.DidAppear();
        }
    }
}