using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views;

namespace TimeSheet
{
    [Activity(Label = "TimeSheet", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			LinearLayout btnTime = FindViewById<LinearLayout>(Resource.Id.btnSales);
			btnTime.Click += (object sender, EventArgs e) =>
			{
				Console.WriteLine("Time");
			};

            LinearLayout btnChart = FindViewById<LinearLayout>(Resource.Id.btnCharts);
			btnChart.Click += (object sender, EventArgs e) =>
			{
				Console.WriteLine("Chart");
			};

            LinearLayout btnInvoice = FindViewById<LinearLayout>(Resource.Id.btnInvoice);
			btnInvoice.Click += (object sender, EventArgs e) =>
			{
				Console.WriteLine("Invoice");
			};

            LinearLayout btnSetting = FindViewById<LinearLayout>(Resource.Id.btnSetting);
			btnSetting.Click += (object sender, EventArgs e) =>
			{
				Console.WriteLine("Setting");
			};

            LinearLayout btnData = FindViewById<LinearLayout>(Resource.Id.btnData);
			btnData.Click += (object sender, EventArgs e) =>
			{
				Console.WriteLine("Data");
			};

            LinearLayout btnExit = FindViewById<LinearLayout>(Resource.Id.btnExit);
			btnExit.Click += (object sender, EventArgs e) =>
			{
				Console.WriteLine("Exit");
			};
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
            MenuInflater.Inflate(Resource.Menu.main, menu);        
			return true;
		}
 
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
                case Resource.Id.menu_help:
                    Console.WriteLine("Help");
					return true;
				default:
					return base.OnOptionsItemSelected(item);
			}
		}

         
	}
}
    