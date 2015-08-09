﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SQLite;
namespace DAL
{
    public static class DatabaseFileUtilities
    {
        private static string filename;
        public static String Filename
        {
            get { return filename ?? (filename = Path.GetRandomFileName() + ".db"); }
        }

        public static void CreateFile()
        {
            if (File.Exists(Filename))
            {
                DeleteFile();
            }
            SQLiteConnection.CreateFile(filename);
            var sqLiteConnection = new SQLiteConnection("Data Source="+filename+";Version=3;");
            string tableCreates = File.ReadAllText("DatabaseRevisions.sql");
            sqLiteConnection.Open();

            sqLiteConnection.Close();




        }

        /*
        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            sqlite.Close();
            return dt;
        }*/

        public static void DeleteFile()
        {
            if (File.Exists(Filename))
            {
                File.Delete(Filename);
            }
        }

        public static void ClearDatabase()
        {
        }

        public static void FillWithRandomData()
        {
        }

    }
}