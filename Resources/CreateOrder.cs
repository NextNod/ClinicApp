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

            List<string> list = server.getOrderDate(data.GetInt("ID"));

            orderDate.Click += (o, e) =>
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Выберите желаемую дату: ");
                builder.SetItems(list.ToArray(), (sender, even) => 
                {
                    int selected = even.Which;
                    orderDate.Text = list[selected];
                    orderDate.SetTextColor(Android.Graphics.Color.Green);
                });
                builder.Show();
            };
        }
    }
}