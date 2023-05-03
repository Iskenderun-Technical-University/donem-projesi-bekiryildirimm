﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EzanVakti_Mobil.Resources.Ayarlar;
using EzanVakti_Mobil.Resources.EzanVakitleri;
using EzanVakti_Mobil.Resources.sehir;
using EzanVakti_Mobil.Resources.VeriTabani;
using Java.Lang;
using Picasso.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzanVakti_Mobil.Resources
{
    public class namazVaktiApi
    {
        public static string enlem;
        public static string boylam;
        public static int ay;
        public static int yil;
        public static string city;
        veritabani vt;
        public namazVaktiApi()
        {

        }
        public namazVaktiApi(string enlem,string boylam,int ay, int yil)
        {
            enlem = enlem;
            boylam = boylam;
            ay = ay;
            yil = yil;
        }
        public namazVaktiApi(string enlem,string boylam)
        {
            enlem = enlem;
            boylam = boylam;
        }
        public static string ApiLink(string enlem,string boylam,int yil,int ay)
        {
              return $"https://api.aladhan.com/v1/calendar?latitude={enlem}&longitude={boylam}&&month={ay}&year={yil}&adjustment=1&tune=0,0,-7,5,4,6,6,0";
           // return $"https://api.aladhan.com/v1/calendar/2023/4?latitude=37.017448347669024&longitude=37.34085951963926&method=13&month=4&year=2023";
        }
        public static string GpsApi(string enlem, string boylam)
        {
            return $"https://api.openweathermap.org/geo/1.0/reverse?lat={enlem}&lon={boylam}&local_names=tr&appid=f0a46c1cf8fe28d014ed0783f9884b23";
        }
        private async Task<EzanVakti> EzanApi()
        {
            var ezanapi = ApiLink(enlem, boylam, yil, ay);
            var httpService = new CLientModel();
            var result = await httpService.ProcessApi<EzanVakti>(ezanapi);
            return result;
        }
        private async Task<List<GpsSehir>> SehirApi()
        {
            var sehirapi = GpsApi(enlem, boylam);
            var httpService = new CLientModel();
            var result = await httpService.ProcessApi<List<GpsSehir>>(sehirapi);
            return result;
        }
        public async Task EzanApiCall(List<namazVaktiData> ls)
        {
            string[] Aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayis", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasim", "Aralik" };//ay isimlerini string olarak tutan dizi 
            string[] GunlerKisa = { "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt", "Paz" };//haftanın günlerinin kısaltılmış isimlerini tutan dizi
            string[] GunlerUzun = { "Pazartesi", "Sali", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };//haftanın günlerinin isimlerini tutan dizi
            string gunUzun = "";//gün ismini tutan string değişken
            string gunKisa = "";//kısaltılmış gün ismini tutan string değişken 
            var res = await EzanApi();

            foreach(var item in res.data)
            {
                if (item.date.gregorian.weekday.en == "Monday")
                {
                    gunKisa = GunlerKisa[0];  //api den çekilen haftanın günleri ingilizce olduğu için türkçeye çevrilerek dosyaya türkçe olarak yazdırılıyor.
                    gunUzun = GunlerUzun[0];
                }
                else if (item.date.gregorian.weekday.en == "Tuesday")
                {
                    gunKisa = GunlerKisa[1];
                    gunUzun = GunlerUzun[1];
                }
                else if (item.date.gregorian.weekday.en == "Wednesday")
                {
                    gunKisa = GunlerKisa[2];
                    gunUzun = GunlerUzun[2];
                }
                else if (item.date.gregorian.weekday.en == "Thursday")
                {
                    gunKisa = GunlerKisa[3];
                    gunUzun = GunlerUzun[3];
                }
                else if (item.date.gregorian.weekday.en == "Friday")
                {
                    gunKisa = GunlerKisa[4];
                    gunUzun = GunlerUzun[4];
                }
                else if (item.date.gregorian.weekday.en == "Saturday")
                {
                    gunKisa = GunlerKisa[5];
                    gunUzun = GunlerUzun[5];
                }
                else if (item.date.gregorian.weekday.en == "Sunday")
                {
                    gunKisa = GunlerKisa[6];
                    gunUzun = GunlerUzun[6];
                }

                ls.Add(new namazVaktiData
                {
                    imsak = item.timings.Fajr.Remove(5, 6),
                    gunes = item.timings.Sunrise.Remove(5, 6),
                    ogle = item.timings.Dhuhr.Remove(5, 6),
                    ikindi = item.timings.Asr.Remove(5, 6),
                    aksam = item.timings.Maghrib.Remove(5, 6),
                    yatsi = item.timings.Isha.Remove(5, 6),
                    readable = item.date.readable,
                    GregDate=item.date.gregorian.date,
                    GregDay=int.Parse(item.date.gregorian.day),
                    GregWeekdayEn=item.date.gregorian.weekday.en,
                    GregHaftaninGunuKisa=gunKisa,
                    GregHaftaninGunuUzun=gunUzun,
                    GregMonthNumber=int.Parse(item.date.gregorian.month.number.ToString()),
                    GregMonthEn=item.date.gregorian.month.en,
                    GregAylar= Aylar[(item.date.gregorian.month.number) - 1],
                    GregYear=item.date.gregorian.year,
                    HijriDate=item.date.hijri.date,
                    HijriDay=int.Parse(item.date.hijri.day),
                    HijriWeekdayEn=item.date.hijri.weekday.en,
                    HijriMonthNumber=item.date.hijri.month.number,
                    HijriMonthEn= item.date.hijri.month.en,
                    HijriYear=item.date.hijri.year,
                });
            }

        }

        public async Task EzanSqlite()
        {
            vt = new veritabani();
            vt.createDataBase("NamazVakti.db");
            List<namazVaktiData> list = new List<namazVaktiData>();
            await EzanApiCall(list);
            var cityRes = await SehirApi();
            foreach (var item in cityRes)
            {
                namazVaktiApi.city = item.name;
            }
            foreach (var item in list)
            {
                vt.InsertIntoTableEzanVakti(item, "NamazVakti.db");
            }
        }
        public void CurrentInsertTable()
        {
            CurrentDatabase cdb=new CurrentDatabase();
            Current current=new Current();
            current.year = yil;
            current.month = ay;
            current.Sehir = city;
            cdb.createDataBaseCurrent();
            cdb.InsertIntoTableCurrent(current);
        }
        public void CurrentUpdateTable()
        {
            CurrentDatabase cdb = new CurrentDatabase();
            Current current = new Current();
            current.year = yil;
            current.month = ay;
            current.Sehir = city;
            cdb.UpdateTableCurrent(current);
        }
    }
}
// Api Adresi--> https://api.aladhan.com/v1/calendar/2023/4?latitude=37.017448347669024&longitude=37.34085951963926&&month=4&year=2023&tune=0,0,-7,5,4,6,6,0