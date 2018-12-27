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
using Newtonsoft.Json;
using RestSharp;

namespace App2
{
    [Activity(Label = "ActivityTime")]
    public class ActivityTime : Activity
    {
        string tUser;
        DateTime dateS;
        DateTime dateE;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout2);

            FindViewById<Button>(Resource.Id.btnForm).Click += btnClick_Form;
            FindViewById<Button>(Resource.Id.btnF).Click += btnClick_Date;
            FindViewById<Button>(Resource.Id.btnL).Click += btnClick_Date;
            FindViewById<Button>(Resource.Id.btnShow).Click += btnClick_Show;

            tUser = Intent.GetStringExtra("tUser");
        }

        private void btnClick_Show(object sender, EventArgs e)
        {
            var client = new RestClient("http://85.214.153.219:8081/myHour/account/work/amount");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "50398737-125b-4f2e-aa51-9d43a0bc8c24");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("appToken", "HAWCJ1myHours");
            request.AddHeader("userToken", tUser);
            request.AddParameter("undefined", "{\n\t\"startDate\":\"" + dateS.Date.ToString() + "" +
                "\",\n\t\"endDate\":\"" + dateE.Date.ToString() + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                string tTime = Math.Round(Double.Parse(
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content)["content"]), 2).ToString();
                FindViewById<TextView>(Resource.Id.txtDateTime).Text = "Time: " + tTime;
            }
        }

        private void btnClick_Date(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DateTime date = FindViewById<DatePicker>(Resource.Id.datePicker1).DateTime;
            if (button.Id == Resource.Id.btnL)
                dateE = date;
            else
                dateS = date;
        }

        private void btnClick_Form(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("tUser", tUser);
            StartActivity(intent);
        }
    }
}