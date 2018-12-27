using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RestSharp;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FSharpUtils.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App2
{
    [Activity(Label = "RegActivity")]
    public class RegActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_reg);

            FindViewById<Button>(Resource.Id.btnLogin).Click += btnClick;
            FindViewById<Button>(Resource.Id.button3).Click += btnClick_Log;
        }

        private void btnClick_Log(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(LoginActivity)));
        }

        private void btnClick(object sender, EventArgs e)
        {
            var pwd = FindViewById<EditText>(Resource.Id.txtPassword).Text;
            var log = FindViewById<EditText>(Resource.Id.txtLogin).Text;
            var fName = FindViewById<EditText>(Resource.Id.txtFName).Text;
            var lName = FindViewById<EditText>(Resource.Id.txtLName).Text;
            var comp = FindViewById<EditText>(Resource.Id.txtComp).Text;
            if (!log.Equals("") && !pwd.Equals("") &&
                !fName.Equals("") && !lName.Equals("") && !comp.Equals(""))
                Reg(log, pwd, fName, lName, comp);
            else
                Toast.MakeText(this, "Fill all fields", ToastLength.Short).Show();
        }

        private void Reg(string log, string pwd, string fName, string lName, string comp)
        {
            var client = new RestClient("http://85.214.153.219:8081/myHour/account/register");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "21a3cba1-8bb0-4b3e-b550-3c9be66a22d8");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("appToken", "HAWCJ1myHours");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined",
                "{\n\t\"email\":\"" + log + "\",\n\t\"password\":\"" + pwd + "\"" +
                ",\n\t\"firstName\":\"" + fName + "\",\n\t\"lastName\":\"" +
                lName + "\",\n\t\"company\":\"" + comp + "\"\n}",
                ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK) && response.Content.Contains("true"))
                StartActivity(new Intent(this, typeof(LoginActivity)));
            else
                Toast.MakeText(this, "Register Failed", ToastLength.Short).Show();
        }
    }
}