using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading;
using Android.Views;
using Android.Content;
using Java.Util;

namespace TimeSheet
{
    
    [Activity(Label = "TimeSheet", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : Activity
    {
        bool bPaused;

        System.Timers.Timer timer;

        TextView textDuration;
        TextView textBreak;
		TextView textStartTime ;
		TextView punchPause ;
		Button btnPopupMenu ;
        LinearLayout btnPunchIn, btnPunchOut;

        int t_duration, t_break;

        int startHour, startMinute;

		const int TIME_DIALOG_ID = 10;
		const int NOTE_DIALOG_ID = 11;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			textDuration = FindViewById<TextView>(Resource.Id.textDuration);
			textBreak = FindViewById<TextView>(Resource.Id.textBreak);
			btnPunchIn = FindViewById<LinearLayout>(Resource.Id.btnPunchIn);
			btnPunchOut = FindViewById<LinearLayout>(Resource.Id.btnPunchOut);
			textStartTime = FindViewById<TextView>(Resource.Id.tvStartTime);
			punchPause = FindViewById<TextView>(Resource.Id.punchPause);
			btnPopupMenu = FindViewById<Button>(Resource.Id.popupMenu);

            t_duration = t_break = 0;

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

			punchPause.Click += (object sssender, EventArgs se) =>
			{

				RunOnUiThread(() =>
				{
					 textBreak.Visibility = ViewStates.Visible;
				});
				
				bPaused = !bPaused;
                if (bPaused)
                {
                    if (Resources != null)
                    {
                        punchPause.SetTextColor(Resources.GetColor(Resource.Color.green));
                    }
                }
                else
                {
                    if (Resources != null)
                    {
                        punchPause.SetTextColor(Resources.GetColor(Resource.Color.menuCount));
                    }
                }
			};


            btnPopupMenu.Click += (object sender, EventArgs e) =>
            {
				PopupMenu menu = new PopupMenu(this, btnPopupMenu);

				// with Android 4 Inflate can be called directly on the menu
				menu.Inflate(Resource.Menu.popup_punch);
                var self = this;
				menu.MenuItemClick += (s1, arg1) =>
				{
                    if(arg1.Item.TitleFormatted.ToString() == "Punch in")
                    {
						EditText et = new EditText(this);
                        TimePicker tP = new TimePicker(this);
						AlertDialog.Builder ad = new AlertDialog.Builder(this);
						ad.SetTitle("Punch In");
						ad.SetView(tP).SetPositiveButton("Confirm", (ss, aa) =>
						{
                            TimePickerCallback(tP);
						}).SetNegativeButton("Cancel", (senders, es) => { });
						ad.Show();
 
                        //self.ShowDialog(TIME_DIALOG_ID); 
                         
                    }
					if (arg1.Item.TitleFormatted.ToString() == "Note")
					{
						EditText et = new EditText(this);
						AlertDialog.Builder ad = new AlertDialog.Builder(this);
						ad.SetTitle("Add Note");
						ad.SetView(et).SetPositiveButton("Confirm", (ss, aa) =>
				        {
                            Console.WriteLine(et.Text);
                        }).SetNegativeButton("Cancel",(senders, es) => {});
						ad.Show();
					}

				};

				menu.Show();
            };

			btnPunchIn.Click += (object sender, EventArgs e) =>
            {
				timer = new System.Timers.Timer();
				timer.Interval = 1000;
				timer.Elapsed += OnTimedEvent;
				timer.Enabled = true;

                textDuration.Visibility = ViewStates.Visible;
                btnPunchOut.Visibility = ViewStates.Visible;
                btnPunchIn.Visibility = ViewStates.Gone;
                startHour = DateTime.Now.Hour;
                startMinute = DateTime.Now.Minute;
                setStartTime();
            };

        }

        void setStartTime()
        {
			string time = string.Format("{0}:{1}", startHour.ToString().PadLeft(2, '0'), startMinute.ToString().PadLeft(2, '0'));
			textStartTime.Text = time;

            int nowHour = DateTime.Now.Hour;
            int nowMinute = DateTime.Now.Minute;

            Console.WriteLine(nowHour + ":"  + nowMinute);
            Console.WriteLine(startHour + ":" + startMinute);

            t_duration = 60*( (nowHour - startHour) * 60 + (nowMinute - startMinute) );


        }

		private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
		{
            if(bPaused) {
                t_break++;
            }
            else {
                t_duration++;
            }
			int hr = t_duration / 3600;
			int mi = (t_duration % 3600) / 60;
			int sec = t_duration % 60;
			string temp1 = string.Format("{0}:{1}:{2}", hr.ToString().PadLeft(2, '0'), mi.ToString().PadLeft(2, '0'), sec.ToString().PadLeft(2, '0'));
			

			hr = t_break / 3600;
			mi = (t_break % 3600) / 60;
			sec = t_break % 60;
			string temp = string.Format("{0}:{1}:{2}", hr.ToString().PadLeft(2, '0'), mi.ToString().PadLeft(2, '0'), sec.ToString().PadLeft(2, '0'));
            RunOnUiThread(()=>{
				textBreak.Text = temp;
				textDuration.Text = temp1;
            });

		}   
 

        private void TimePickerCallback(TimePicker tp)
		{
            int _hour, _minute;

            _hour = tp.Hour;
            _minute = tp.Minute;
            if(_hour > DateTime.Now.Hour || (_hour == DateTime.Now.Hour && _minute > DateTime.Now.Minute))
            {
				new AlertDialog.Builder(this)
				.SetPositiveButton("OK", (ss, aa) =>
				{
					// User pressed yes
				})
				.SetMessage("Start time cannot be after current time")
				.Show();

				return;
            }
            startHour = _hour;
            startMinute = _minute;
            setStartTime();
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
					Intent intent = new Intent(this, typeof(HelpActivity));  
					StartActivity(intent);


					return true;
				default:
					return base.OnOptionsItemSelected(item);
			}
		}

         
	}
}
