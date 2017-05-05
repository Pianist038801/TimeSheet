using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views;

namespace TimeSheet
{
	[Activity(Label = "Help", Theme = "@style/AppTheme")]
	public class HelpActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
		}

	}
}
