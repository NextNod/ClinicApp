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

namespace ClinicApp.Resources
{
    [Activity(Label = "CreateOrder")]
    public class CreateOrder : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Order);
            Bundle data = Intent.GetBundleExtra("data");
            Server server = new Server();

            Button orderDate = FindViewById<Button>(Resource.Id.orderDate);
            Button birthday = FindViewById<Button>(Resource.Id.birthday);

            List<string> list = new List<string>();
            list.Add("10.11.2020");
            list.Add("11.11.2020");
            list.Add("12.11.2020");

            orderDate.Click += (o, e) =>
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Выберите желаемую дату: ");
                builder.SetItems(list.ToArray(), (sender, even) => 
                {
                    int selected = even.Which;
                    orderDate.Text = list[selected];
                    orderDate.SetTextColor(Android.Graphics.Color.Green);
                    Toast toast = Toast.MakeText(BaseContext, selected + ":" + list[selected], ToastLength.Long);
                    toast.Show();
                });
                builder.Show();
            };
        }
    }
}