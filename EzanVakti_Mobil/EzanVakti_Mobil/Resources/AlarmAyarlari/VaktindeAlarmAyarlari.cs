using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EzanVakti_Mobil.Resources.Ayarlar;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzanVakti_Mobil.Resources.AlarmAyarlari
{
    [Table("VaktindeAlarmAyarlari")]
    public class VaktindeAlarmAyarlari
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        public bool imsakAlarm { get; set; }
        public bool gunesAlarm { get; set; }
        public bool ogleAlarm { get; set; }
        public bool ikindiAlarm { get; set; }
        public bool aksamAlarm { get; set; }
        public bool yatsiAlarm { get; set; }

        public DateTime haftalikUpdate { get; set; }
    }

    public class VaktindeAlarmDatabase
    {
        static string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDataBaseVaktindeAlarm()
        {

           // try
            //{
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
                {
                    connection.CreateTable<VaktindeAlarmAyarlari>();

                    return true;
                }
            /*}
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEx", e.Message);
                return false;
            }*/
        }
        public bool InsertIntoTableVaktindeAlarm(object vaktindeAlarm)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
                {
                    connection.Insert(vaktindeAlarm);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLite Ex", e.Message);
                return false;
            }
        }
        public static VaktindeAlarmAyarlari selectTable()
        {



            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                return connection.Table<VaktindeAlarmAyarlari>().First();
            }


        }
        public static void selectTableVaktindeAlarm(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
               
                connection.Query<VaktindeAlarmAyarlari>("SELECT * from VaktindeAlarmAyarlari where Id=1");
            }
        }
        public void UpdateTableAll(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                connection.Query<VaktindeAlarmAyarlari>("UPDATE VaktindeAlarmAyarlari set imsakAlarm=?,gunesAlarm=?,ogleAlarm=?,ikindiAlarm=?,aksamAlarm=?,yatsiAlarm=? where Id=1", vaktindeAlarm.imsakAlarm, vaktindeAlarm.gunesAlarm, vaktindeAlarm.ogleAlarm, vaktindeAlarm.ikindiAlarm, vaktindeAlarm.aksamAlarm, vaktindeAlarm.yatsiAlarm);
            }
        }
        public void UpdateTableImsak(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                connection.Query<VaktindeAlarmAyarlari>("UPDATE VaktindeAlarmAyarlari set imsakAlarm=? where Id=1", vaktindeAlarm.imsakAlarm);
            }
        }
        public void UpdateTableGunes(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                connection.Query<VaktindeAlarmAyarlari>("UPDATE VaktindeAlarmAyarlari set gunesAlarm=? where Id=1", vaktindeAlarm.gunesAlarm);
            }
        }
        public void UpdateTableOgle(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                connection.Query<VaktindeAlarmAyarlari>("UPDATE VaktindeAlarmAyarlari set ogleAlarm=? where Id=1", vaktindeAlarm.ogleAlarm);
            }
        }
        public void UpdateTableIkindi(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                connection.Query<VaktindeAlarmAyarlari>("UPDATE VaktindeAlarmAyarlari set ikindiAlarm=? where Id=1", vaktindeAlarm.ikindiAlarm);
            }
        }
        public void UpdateTableAksam(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                connection.Query<VaktindeAlarmAyarlari>("UPDATE VaktindeAlarmAyarlari set aksamAlarm=? where Id=1", vaktindeAlarm.aksamAlarm);
            }
        }
        public void UpdateTableYatsi(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                connection.Query<VaktindeAlarmAyarlari>("UPDATE VaktindeAlarmAyarlari set yatsiAlarm=? where Id=1", vaktindeAlarm.yatsiAlarm);
            }
        }

        public void UpdateTableTarih(VaktindeAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
            {
                connection.Query<VaktindeAlarmAyarlari>("UPDATE VaktindeAlarmAyarlari set haftalikUpdate=? where Id=1", vaktindeAlarm.haftalikUpdate);
            }
        }
        public bool deleteTable(object obj)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VaktindeAlarmAyarlari.db")))
                {
                    connection.Delete(obj);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLite Ex", e.Message);
                return false;
            }
        }
    }
}