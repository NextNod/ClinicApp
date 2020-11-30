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
            Button buttonDone = FindViewById<Button>(Resource.Id.doneButton);
            EditText editName = FindViewById<EditText>(Resource.Id.name);
            EditText editPhone = FindViewById<EditText>(Resource.Id.phone);
            CheckBox checkFirst = FindViewById<CheckBox>(Resource.Id.firstOrder);
            CheckBox checkSave = FindViewById<CheckBox>(Resource.Id.saveData);
            TextView nullEditName = FindViewById<TextView>(Resource.Id.nullEditName);
            TextView nullEditPhone = FindViewById<TextView>(Resource.Id.nullEditPhone);

            Date dateGet = new Date();
            List<string> list = server.getOrderDate(data.GetInt("ID"));
            int[] dates;
            
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

            birthday.Click += (o, e) => 
            {
                DatePickerDialog date = new DatePickerDialog(this, dateGet, 2020, 0, 0);
                date.DismissEvent += (sender, even) => 
                {
                    dates = dateGet.getDate();
                    birthday.Text = dates[0] + "." + dates[1] + "." + dates[2];
                    birthday.SetTextColor(Android.Graphics.Color.Green);
                };
                date.Show();
            };

            buttonDone.Click += (o, e) => 
            {
                if (checkSave.Checked)
                {
                    if (editName.Text != "" && editPhone.Text != "")
                    {
                        DataBase dataBase = new DataBase();
                    }
                    else 
                    {
                        if (editName.Text == "")
                        {
                            nullEditName.Visibility = ViewStates.Visible;
                        }
                        else 
                        {
                            nullEditPhone.Visibility = ViewStates.Visible;
                        }
                    }
                }
            };
        }
    }

    class Date : Activity, DatePickerDialog.IOnDateSetListener 
    {
        int[] dates = new int[3];
        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            dates[0] = dayOfMonth;
            dates[1] = month;
            dates[2] = year;
        }

        public int[] getDate() 
        {
            return dates;
        }
    }
}