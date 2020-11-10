﻿using SQLite;
using System.Collections.Generic;
using System.IO;

namespace ClinicApp.Resources
{
    public class Data
    {
        public int ID { set; get; }
        public int ver { set; get; }
        public string name { set; get; }
        public string discription { set; get; }
    }

    public class DataBase
    {
        private string Path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Data.db");
        private const string nameTable = "Doctors";

        public DataBase()
        {
            CreateTable();
        }

        public bool CreateTable()
        {
            try
            {
                if (!File.Exists(Path))
                {
                    File.Create(Path);
                }

                var db = new SQLiteConnection(Path);
                var cmd = new SQLiteCommand(db);

                cmd.CommandText = "CREATE TABLE " + nameTable + " (ID INTEGER NOT NULL PRIMARY KEY, name STRING NOT NULL, discription STRING NOT NULL );";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE ver (ver INTEGER NOT NULL);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO ver (ver) VALUES (0)";
                cmd.ExecuteNonQuery();
                
                db.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int ver
        {
            set
            {
                var db = new SQLiteConnection(Path);
                var cmd = new SQLiteCommand(db);
                cmd.CommandText = "UPDATE ver SET ver=" + value;
                cmd.ExecuteNonQuery();
                db.Close();
            }

            get
            {
                var db = new SQLiteConnection(Path);
                var cmd = new SQLiteCommand(db);
                cmd.CommandText = "SELECT * FROM ver";
                List<Data> vs = cmd.ExecuteQuery<Data>();
                db.Close();
                return vs[0].ver;
            }
        }

        public void Insert(int ID, string name, string discription)
        {
            var db = new SQLiteConnection(Path);
            var cmd = new SQLiteCommand(db);
            cmd.CommandText = "INSERT INTO " + nameTable + " (ID, name, discription) VALUES (" + ID + ", '" + name + "', '" + discription + "')";
            cmd.ExecuteNonQuery();
            db.Close();
        }

        public void Delete(string name)
        {
            var db = new SQLiteConnection(Path);
            var cmd = new SQLiteCommand(db);
            cmd.CommandText = "DELETE FROM " + nameTable + " WHERE Name='" + name + "'";
            cmd.ExecuteNonQuery();
            db.Close();
        }

        public void Delete(int ID)
        {
            var db = new SQLiteConnection(Path);
            var cmd = new SQLiteCommand(db);
            cmd.CommandText = "DELETE FROM " + nameTable + " WHERE ID=" + ID;
            cmd.ExecuteNonQuery();
            db.Close();
        }

        public void Delete()
        {
            var db = new SQLiteConnection(Path);
            var cmd = new SQLiteCommand(db);
            cmd.CommandText = "DELETE FROM " + nameTable;
            cmd.ExecuteNonQuery();
            db.Close();
        }

        public void Update(int ID, string name, string discription)
        {
            var db = new SQLiteConnection(Path);
            var cmd = new SQLiteCommand(db);
            cmd.CommandText = "UPDATE " + nameTable + " SET ID=" + ID + " discription='" + discription + "' WHERE name='" + name + "'";
            cmd.ExecuteNonQuery();
            db.Close();
        }

        public List<Data> GetData()
        {
            var db = new SQLiteConnection(Path);
            var cmd = new SQLiteCommand(db);

            cmd.CommandText = "SELECT * FROM " + nameTable;
            List<Data> name = cmd.ExecuteQuery<Data>();

            db.Close();
            return name;
        }
    }
}