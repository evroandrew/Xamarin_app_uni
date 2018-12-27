using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using Android.Content;

namespace App2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DateTime start;
        DateTime stop;
        string commentMessage;
        string tUser = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Auth();

            StartActivity(new Intent(this, typeof(LoginActivity)));

            SetContentView(Resource.Layout.layout1);

            FindViewById<Button>(Resource.Id.button1).Click += btnClick;
            FindViewById<Button>(Resource.Id.button2).Click += btnClick_Export;
            FindViewById<Button>(Resource.Id.button3).Click += btnClick_Log;
            FindViewById<EditText>(Resource.Id.txtComment).Visibility = Android.Views.ViewStates.Invisible;
        }

        private void Auth()
        {
            if (!ReadTU())
            {
                String lName = Intent.GetStringExtra("tUser");
                if (lName == null)
                    StartActivity(new Intent(this, typeof(LoginActivity)));
                else
                {
                    tUser = lName;
                    SaveTUAsync();
                }
            }
        }

        private void btnClick_Log(object sender, EventArgs e)
        {
            tUser = null;
            SaveTUAsync();
            StartActivity(new Intent(this, typeof(LoginActivity)));
        }

        private void btnClick_Export(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityTime));
            intent.PutExtra("tUser", tUser);
            StartActivity(intent);
        }

        private bool ReadTU()
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cc.txt");
            if (backingFile == null || !File.Exists(backingFile))
                return false;
            string temp = null;
            using (var rd = new StreamReader(backingFile, true))
            {
                string line;
                while ((line = rd.ReadLine()) != null)
                    temp += line;
            }
            if (temp == null || temp == "")
                return false;
            else
            {
                tUser = temp;
                return true;
            }
        }

        private void SaveTUAsync()
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cc.txt");
            using (var writter = File.CreateText(backingFile))
                writter.WriteLine(tUser);
        }

        private void btnClick(object sender, EventArgs e)
        {
            Button btnMain = sender as Button;
            btnMain.Text = btnMain.Text == "Start" ? "Stop" : "Start";
            if (btnMain.Text == "Stop")
            {
                Toast.MakeText(this, "Timer started", ToastLength.Short).Show();
                start = DateTime.Now;
                FindViewById<EditText>(Resource.Id.txtComment).Visibility = Android.Views.ViewStates.Visible;
            }
            if (btnMain.Text == "Start")
            {
                Toast.MakeText(this, "Timer stoped", ToastLength.Short).Show();
                stop = DateTime.Now;
                EditText comment = FindViewById<EditText>(Resource.Id.txtComment);
                comment.Visibility = Android.Views.ViewStates.Invisible;
                commentMessage = comment.Text;
                comment.Text = null;
                SendInfo();
            }
        }

        private void SendInfo()
        {
            RestClient client = new RestClient("http://85.214.153.219:8081/myHour/account/work/start");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "58488692-7edb-4802-ba59-e81694907e4e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("appToken", "HAWCJ1myHours");
            request.AddHeader("userToken", tUser);
            request.AddParameter("undefined",
                "{\n\t\"start\":\"" + DtoS(start) + "\",\n\t\"end\":\"" + DtoS(stop) +
                "\",\n\t\"comment\":\"" + commentMessage + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Toast.MakeText(this, response.ToString(), ToastLength.Short).Show();
        }

        private string DtoS(DateTime timer)
        {
            return timer.Hour.ToString() + "." + timer.Minute.ToString();
        }
    }
}
