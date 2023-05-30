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
    [Table("VakitOncesiAlarmAyarlari")]
    public class VakitOncesiAlarmAyarlari
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public bool imsakAlarm { get; set; }
        public bool gunesAlarm { get; set; }
        public bool ogleAlarm { get; set; }
        public bool ikindiAlarm { get; set; }
        public bool aksamAlarm { get; set; }
        public bool yatsiAlarm { get; set; }

        public int imsakdkOncesi { get; set; }
        public int gunesdkOncesi { get; set; }
        public int ogledkOncesi { get; set; }
        public int ikindidkOncesi { get; set; }
        public int aksamdkOncesi { get; set; }
        public int yatsidkOncesi { get; set; }
    }

    public class VakitOncesiAlarmDatabase
    {
        static string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDataBaseVaktindeAlarm()
        {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
                {
                    connection.CreateTable<VakitOncesiAlarmAyarlari>();

                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEx", e.Message);
                return false;
            }
        }
        public bool InsertIntoTableVaktindeAlarm(object vaktindeAlarm)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
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
        public static VakitOncesiAlarmAyarlari selectTable()
        {



            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {
                return connection.Table<VakitOncesiAlarmAyarlari>().First();
            }


        }
        public static void selectTableVaktindeAlarm(VakitOncesiAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {

                connection.Query<VakitOncesiAlarmAyarlari>("SELECT * from VakitOncesiAlarmAyarlari where Id=1");
            }
        }
        public void UpdateTableAll(VakitOncesiAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {
                connection.Query<VakitOncesiAlarmAyarlari>("UPDATE VakitOncesiAlarmAyarlari set imsakAlarm=?,gunesAlarm=?,ogleAlarm=?,ikindiAlarm=?,aksamAlarm=?,yatsiAlarm=?,imsakdkOncesi=?,gunesdkOncesi=?,ogledkOncesi=?,ikindidkOncesi=?,aksamdkOncesi=?,yatsidkOncesi=? where Id=1", vaktindeAlarm.imsakAlarm, vaktindeAlarm.gunesAlarm, vaktindeAlarm.ogleAlarm, vaktindeAlarm.ikindiAlarm, vaktindeAlarm.aksamAlarm, vaktindeAlarm.yatsiAlarm, vaktindeAlarm.imsakdkOncesi, vaktindeAlarm.gunesdkOncesi, vaktindeAlarm.ogledkOncesi, vaktindeAlarm.ikindidkOncesi, vaktindeAlarm.aksamdkOncesi, vaktindeAlarm.yatsidkOncesi);
            }
        }
        public void UpdateTableImsak(VakitOncesiAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {
                connection.Query<VakitOncesiAlarmAyarlari>("UPDATE VakitOncesiAlarmAyarlari set imsakAlarm=?,imsakdkOncesi=? where Id=1", vaktindeAlarm.imsakAlarm, vaktindeAlarm.imsakdkOncesi);
            }
        }
        public void UpdateTableGunes(VakitOncesiAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {
                connection.Query<VakitOncesiAlarmAyarlari>("UPDATE VakitOncesiAlarmAyarlari set gunesAlarm=?,gunesdkOncesi=? where Id=1", vaktindeAlarm.gunesAlarm, vaktindeAlarm.gunesdkOncesi);
            }
        }
        public void UpdateTableOgle(VakitOncesiAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {
                connection.Query<VakitOncesiAlarmAyarlari>("UPDATE VakitOncesiAlarmAyarlari set ogleAlarm=?,ogledkOncesi=? where Id=1", vaktindeAlarm.ogleAlarm, vaktindeAlarm.ogledkOncesi);
            }
        }
        public void UpdateTableIkindi(VakitOncesiAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {
                connection.Query<VakitOncesiAlarmAyarlari>("UPDATE VakitOncesiAlarmAyarlari set ikindiAlarm=?,ikindidkOncesi=? where Id=1", vaktindeAlarm.ikindiAlarm, vaktindeAlarm.ikindidkOncesi);
            }
        }
        public void UpdateTableAksam(VakitOncesiAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {
                connection.Query<VakitOncesiAlarmAyarlari>("UPDATE VakitOncesiAlarmAyarlari set aksamAlarm=?,aksamdkOncesi=? where Id=1", vaktindeAlarm.aksamAlarm, vaktindeAlarm.aksamdkOncesi);
            }
        }
        public void UpdateTableYatsi(VakitOncesiAlarmAyarlari vaktindeAlarm)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
            {
                connection.Query<VakitOncesiAlarmAyarlari>("UPDATE VakitOncesiAlarmAyarlari set yatsiAlarm=?,yatsidkOncesi=? where Id=1", vaktindeAlarm.yatsiAlarm, vaktindeAlarm.yatsidkOncesi);
            }
        }
        public bool deleteTable(object obj)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "VakitOncesiAlarmAyarlari.db")))
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