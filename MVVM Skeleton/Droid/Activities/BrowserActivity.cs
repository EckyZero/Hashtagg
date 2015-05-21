using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Droid.UIHelpers;
using Shared.Common;
using Java.Net;

namespace Droid.Phone.Activities
{
    [Activity(Label = "BrowserActivity")]
    public class BrowserActivity : BaseActivity
    {
        private ImageButton _refreshButton;
        private ImageButton _closeButton;
        private ImageButton _backButton;
        private ImageButton _forwardButton;
        private ProgressBar _loadingBar;
        private TextView _title;
        private WebView _webView;
        private CustomWebViewClient _webViewClient;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Browser);

            _refreshButton = FindViewById<ImageButton>(Resource.Id.browser_refresh);
            _closeButton = FindViewById<ImageButton>(Resource.Id.browser_close);
            _backButton = FindViewById<ImageButton>(Resource.Id.browser_backButon);
            _forwardButton = FindViewById<ImageButton>(Resource.Id.browser_forwardButton);
            _loadingBar = FindViewById<ProgressBar>(Resource.Id.browser_acitivityIndicator);
            _title = FindViewById<TextView>(Resource.Id.browser_title);
            _webView = FindViewById<WebView>(Resource.Id.browser_webView);
            _webViewClient = new CustomWebViewClient();

            _refreshButton.Click += RefreshButtonOnClick;
            _closeButton.Click += CloseButtonOnClick;
            _backButton.Click += BackButtonOnClick;
            _forwardButton.Click += ForwardButtonOnClick;
            _webViewClient.LoadStarted += WebViewClientOnLoadStarted;
            _webViewClient.LoadFinished += WebViewClientOnLoadFinished;

            _webView.Settings.JavaScriptEnabled = true;
            _webView.SetWebViewClient(_webViewClient);

            _loadingBar.Visibility = ViewStates.Invisible;

            _forwardButton.Enabled = false;
            _forwardButton.SetColorFilter(SharedColors.Gray2.ToDroidColor());

            _backButton.Enabled = false;
            _backButton.SetColorFilter(SharedColors.Gray2.ToDroidColor());

            string url = Intent.GetStringExtra("URL");

            string title = Intent.GetStringExtra("Title");

            if (!string.IsNullOrWhiteSpace(title))
            {
                SetPageTitle(title);
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                GoTo(url);
            }
        }

        private void WebViewClientOnLoadFinished(object sender, EventArgs eventArgs)
        {
            _loadingBar.Visibility = ViewStates.Invisible;

            _forwardButton.Enabled = _webView.CanGoForward();
            _forwardButton.SetColorFilter(_forwardButton.Enabled ? SharedColors.White.ToDroidColor() : SharedColors.Gray2.ToDroidColor());

            _backButton.Enabled = _webView.CanGoBack();
            _backButton.SetColorFilter(_backButton.Enabled ? SharedColors.White.ToDroidColor() : SharedColors.Gray2.ToDroidColor());


        }

        private void WebViewClientOnLoadStarted(object sender, EventArgs eventArgs)
        {
            _loadingBar.Visibility = ViewStates.Visible;
        }

        private void BackButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (_webView.CanGoBack())
            {
                _webView.StopLoading();
                _webView.GoBack();
            }
        }

        private void CloseButtonOnClick(object sender, EventArgs eventArgs)
        {
            _webView.StopLoading();
            GoBack();
        }

        private void RefreshButtonOnClick(object sender, EventArgs eventArgs)
        {
            _webView.Reload();
        }

        private void ForwardButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (_webView.CanGoForward())
            {
                _webView.StopLoading();
                _webView.GoForward();
            }
        }

        public void GoTo(string url)
        {
            _webView.LoadUrl(url);
        }

        public void SetPageTitle(string title)
        {
            _title.Text = title;
        }
    }
    
    public class CustomWebViewClient : WebViewClient
    {
        public event EventHandler LoadStarted;
        public event EventHandler LoadFinished;

        public override void OnPageStarted(WebView view, string url, Bitmap favicon)
        {
            base.OnPageStarted(view, url, favicon);

            if (LoadStarted != null)
            {
                LoadStarted(this, new EventArgs());
            }
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);

            if (LoadFinished != null)
            {
                LoadFinished(this, new EventArgs());
            }
        }
    }
}