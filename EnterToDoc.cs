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
using ClinicApp.Resources;

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
            int ID = data.GetInt("ID");
            string name = data.GetString("name");
            string discription = data.GetString("description");

            TextView nameText = FindViewById<TextView>(Resource.Id.name);
            TextView discriptionText = FindViewById<TextView>(Resource.Id.description);
            Button button = FindViewById<Button>(Resource.Id.button_deal);
            Server server = new Server();

            nameText.Text = name;
            discriptionText.Text = discription;

            string number = "null";

            button.Click += (e, o) => 
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                View view = this.LayoutInflater.Inflate(Resource.Layout.dialogAlert, null);

                EditText nameClient = (EditText)view.FindViewById<EditText>(Resource.Id.name);
                builder.SetView(view);
                builder.SetPositiveButton("Send Request", (afk, kfa) =>
                {
                    number = nameClient.Text;
                    Toast toast = Toast.MakeText(this, ID + number, ToastLength.Long);
                    toast.Show();

                    server.sendNote(ID, number);
                });
                builder.SetNegativeButton("Exit", (afk, kfa) => { });
                builder.Show();
            };
        }
    }
}