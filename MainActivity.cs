using Android.App;
using ClinicApp.Resources;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.RecyclerView.Extensions;
using Android.Graphics;
using System.Linq;
using System.Collections;
using Android.Provider;
using System.Collections.Generic;
using System;
using System.Xml;
using Android.Content;
using Android.Views;
using System.Text;
using Android.Util;

namespace ClinicApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : ListActivity
    {   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            DataBase dataBase = new DataBase();
            Server server = new Server();
            List<string> list = new List<string>();
            List<Data> array = new List<Data>();

            if (dataBase.ver != server.ver)
            {
                dataBase.Delete();
                array = server.GetDatas();

                foreach (Data dat in array)
                {
                    dataBase.Insert(dat.name, dat.discription);
                    list.Add(dat.name);
                }
            }
            else
            {
                array = dataBase.GetData();

                foreach (Data data in array)
                {
                    list.Add(data.name);
                }
            }

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, list);

            ListView.ItemClick += (e, o) =>
            {
                int pos = o.Position;
                Toast toast = Toast.MakeText(this, list[pos], ToastLength.Long);
                toast.Show();

                Bundle data = new Bundle();
                data.PutString("name", array[pos].name);
                data.PutString("description", array[pos].discription);

                Intent intent = new Intent(this, typeof(EnterToDoc));
                intent.PutExtra("data", data);
                StartActivity(intent);
            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}