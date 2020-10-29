using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using System.Net.Sockets;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util.Zip;
using Java.Security;

namespace ClinicApp
{
    [Activity(Label = "EnterToDoc")]
    public class EnterToDoc : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Description);

            Bundle data = Intent.GetBundleExtra("data");
            string name = data.GetString("name");
            string discription = data.GetString("description");

            TextView nameText = FindViewById<TextView>(Resource.Id.name);
            TextView discriptionText = FindViewById<TextView>(Resource.Id.description);
            Button button = FindViewById<Button>(Resource.Id.button_deal);

            nameText.Text = name;
            discriptionText.Text = discription;
            button.Click += (e, o) => 
            {
                string number = "null";
                
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                View view = this.LayoutInflater.Inflate(Resource.Layout.dialog_save, null);

                EditText phoneNumber = (EditText)view.FindViewById<EditText>(Resource.Id.phoneNumber);
                builder.SetView(view);
                builder.SetPositiveButton("Send Request", (afk, kfa) => 
                {
                    number = phoneNumber.Text;
                    Toast toast = Toast.MakeText(this, number, ToastLength.Long);
                    toast.Show();
                });
                builder.SetNegativeButton("Exit", (afk, kfa) => { });
                builder.Show();
            };
        }
    }
}