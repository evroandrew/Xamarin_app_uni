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
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);

            FindViewById<Button>(Resource.Id.btnLogin).Click += btnClick;
        }
        
        private void btnClick(object sender, EventArgs e)
        {
            var pwd = FindViewById<EditText>(Resource.Id.txtPassword).Text;
            var log = FindViewById<EditText>(Resource.Id.txtLogin).Text;
            if (pwd == "std" && log == "std")
                Login();
        }

        private void Login()
        {
            StartActivity(new Intent(this, typeof(ActivityStart)));
        }

        private void Login(string log, string pwd)
        {
            var client = new RestClient("http://85.214.153.219:8081/myHour/account/login");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "66bfdce6-9f59-449d-92c1-3848f35b6b4c");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("app-token", "HAWCJ1myHours");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined",
                "{\n\t\"email\":\"" + log + "\",\n\t\"password\":\"" + pwd + "\"\n}",
                ParameterType.RequestBody);
            var response = client.Execute(request);
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                string tUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content)["content"];
                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("tUser", tUser);
                StartActivity(intent);
            }
            else
                Toast.MakeText(this, "Login Failed", ToastLength.Short).Show();
        }
    }
}