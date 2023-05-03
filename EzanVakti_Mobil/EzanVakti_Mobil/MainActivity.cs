using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using EzanVakti_Mobil.Resources;
using EzanVakti_Mobil.Resources.VeriTabani;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Essentials;
using static System.Net.Mime.MediaTypeNames;
using AndroidX.RecyclerView.Widget;
using System.Timers;
using AndroidX.AppCompat.Widget;
//using System.Timers;
//using System.Threading.Timer;

namespace EzanVakti_Mobil
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        // Api Adresi--> https://api.aladhan.com/v1/calendar/2023/4?latitude=37.017448347669024&longitude=37.34085951963926&&month=4&year=2023&tune=0,0,-7,5,4,6,6,0
        CancellationTokenSource cts;
        RecyclerView rcData;
        RecycleAdapter ada;
        List<namazVaktiData> data,weeklydata;
        veritabani veriTabani;
        DateTime bugun = DateTime.Now;
        namazVaktiData ezan;
        TextView text;
       System.Timers.Timer timer;
        // CountDownTimer count;
        string[] vakit = {
        "İMSAK VAKTİNE KALAN ",
        " GÜNEŞ VAKTİNE KALAN ",
        "ÖĞLE EZANINA KALAN ",
        " İKİNDİ EZANINA KALAN ",
        "AKŞAM EZANINA KALAN ",
         "YATSI EZANINA KALAN"
    };
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            text = FindViewById<TextView>(Resource.Id.AnaSayfaTarih);
            //   text.Text = "merhaba";
           /* timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += bekir;
            timer.Start();*/
            DateTime dt = DateTime.Now;
            //timer=new Timer()
           // Java.Util.Timer t = new Java.Util.Timer();
            
            
            //TimerCallback callback = new TimerCallback(CheckStatus);


            //   var location = await GetCurrentLocation();
   
            ezan = new namazVaktiData();
             data = new List<namazVaktiData>();

            veriTabani = new veritabani();
            data = veriTabani.selectTable("NamazVakti.db");

            

            foreach (var item in data)
            {
                if (item.GregDay == bugun.Day&&item.GregMonthNumber==bugun.Month)
                {
                    ezan = item;
                }
            }

            text.Text = "  " + ezan.GregDay + "\n" + ezan.GregAylar + "\n" + ezan.GregYear;
            FindViewById<TextView>(Resource.Id.tvImsak).Text = ezan.imsak;
            FindViewById<TextView>(Resource.Id.tvGunes).Text = ezan.gunes;
            FindViewById<TextView>(Resource.Id.tvOgle).Text = ezan.ogle;
            FindViewById<TextView>(Resource.Id.tvIkindi).Text = ezan.ikindi;
            FindViewById<TextView>(Resource.Id.tvAksam).Text = ezan.aksam;
            FindViewById<TextView>(Resource.Id.tvYatsi).Text = ezan.yatsi;
            FindViewById<TextView>(Resource.Id.mainHicriTakvim).Text = " " + ezan.HijriDay + "\n" + ezan.HijriMonthEn + "\n" + ezan.HijriYear;
            rcData = FindViewById<RecyclerView>(Resource.Id.recyclerViewHaftalikVakitler);
            /*ada = new RecycleAdapter(this, data);
             rcData.SetAdapter(ada);*/
            //RecyclerView.Adapter
            LoadHaftalikEzanVakti();
        }

/*        private void Timer_Elapsed1(object sender, ElapsedEventArgs e)
        {
            DateTime simdi = DateTime.Now;
            FindViewById<TextSwitcher>(Resource.Id.KalanVakit).SetCurrentText(vakit[0]);
            FindViewById<TextSwitcher>(Resource.Id.KalanZaman).SetCurrentText(simdi.ToString("HH:mm:ss tt"));
            FindViewById<TextView>(Resource.Id.mainYerelSaat).Text = DateTime.Now.ToString("HH:mm");
        }*/
        void bekir(object sender, EventArgs e)
        {
         //   DateTime simdi = DateTime.Now;
          // FindViewById<TextSwitcher>(Resource.Id.KalanVakit).SetCurrentText("TT");
        // FindViewById<TextSwitcher>(Resource.Id.KalanZaman).SetCurrentText(simdi.ToString("HH:mm:ss tt"));
            FindViewById<AppCompatTextView>(Resource.Id.mainYerelSaat).Text = DateTime.Now.ToString("HH:mm");
        }
        private void LoadHaftalikEzanVakti()
        {  
            weeklydata = new List<namazVaktiData>();
            int i = 0;

        foreach(var item in data)
            {
                if(bugun.Day<=item.GregDay&&bugun.Day+7>=item.GregDay&&bugun.Month==item.GregMonthNumber)
                    weeklydata.Add(item);
            }
            ada = new RecycleAdapter(this, weeklydata);
            rcData.SetAdapter(ada);
        }
        protected override void OnResume()
        {
            base.OnResume(); // Always call the superclass first.
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += bekir;
            timer.Start();


            /*      text = FindViewById<TextView>(Resource.Id.AnaSayfaTarih);
                  //   text.Text = "merhaba";
                  DateTime dt = DateTime.Now;

                  //TimerCallback callback = new TimerCallback(CheckStatus);


                  //   var location = await GetCurrentLocation();
                  /*   namazVaktiApi namazVakti = new namazVaktiApi(location.Latitude.ToString(), location.Longitude.ToString(), dt.Month, dt.Year);
                       // FindViewById<TextView>(Resource.Id.AnaSayfaTarih).Text = namazVaktiApi.enlem + " " + namazVaktiApi.boylam + " " + namazVaktiApi.ay + " " + namazVaktiApi.yil;
                      namazVakti.EzanSqlite();
                  ezan = new namazVaktiData();
                  List<namazVaktiData> data = new List<namazVaktiData>();

                  veriTabani = new veritabani();
                  data = veriTabani.selectTable("NamazVakti.db");



                  foreach (var item in data)
                  {
                      if (item.GregDay == bugun.Day)
                      {
                          ezan = item;
                      }
                  }

                  text.Text = "  " + ezan.GregDay + "\n" + ezan.GregAylar + "\n" + ezan.GregYear;
                  FindViewById<TextView>(Resource.Id.tvImsak).Text = ezan.imsak;
                  FindViewById<TextView>(Resource.Id.tvGunes).Text = ezan.gunes;
                  FindViewById<TextView>(Resource.Id.tvOgle).Text = ezan.ogle;
                  FindViewById<TextView>(Resource.Id.tvIkindi).Text = ezan.ikindi;
                  FindViewById<TextView>(Resource.Id.tvAksam).Text = ezan.aksam;
                  FindViewById<TextView>(Resource.Id.tvYatsi).Text = ezan.yatsi;
                  //FindViewById<TextView>(Resource.Id.mainYerelSaat).Text =;*/
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //DateTime simdi = DateTime.Now;
           // FindViewById<TextSwitcher>(Resource.Id.KalanVakit).SetCurrentText(vakit[0]);
           // FindViewById<TextSwitcher>(Resource.Id.KalanZaman).SetCurrentText(simdi.ToString("HH:mm:ss tt"));
            FindViewById<TextView>(Resource.Id.mainYerelSaat).Text= DateTime.Now.ToString("HH:mm");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
       /* public static void CheckStatus(Object state)
        {
            DebugText debug = new DebugText(
                "System.Threading.Timer"
                , 5
            );
            TokenDataBase databaseRunTask = TempDataSource();
            databaseRunTask.SaveDebugTextAsync(debug);
        }*/
    /*    protected override void OnResume()
        {
            base.OnResume();
         //   timer = new Timer(TimerCallback);
           // timer.inter
        }*/
        async Task<Location> GetCurrentLocation()
        {
 
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

        
                    return location;
                
 
            


        }
    }
}