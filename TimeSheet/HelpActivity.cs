using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views;
using Android.Webkit;

namespace TimeSheet
{
	[Activity(Label = "Help", Icon = null, Theme = "@style/AppTheme")]
	public class HelpActivity : Activity
	{
		 
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Help);
            WebView localWebView = FindViewById<WebView>(Resource.Id.LocalWebView);
            localWebView.LoadUrl("file:///android_asset/help.html");
        }

	}
}
