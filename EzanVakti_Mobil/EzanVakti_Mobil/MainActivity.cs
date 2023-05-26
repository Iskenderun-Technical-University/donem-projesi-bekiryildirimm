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
using HtmlAgilityPack;
using System.Net.Http;
using AndroidX.ConstraintLayout.Widget;
using Android.Views;
using Android.Graphics.Drawables;
using Java.Interop;
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
        namazVaktiData ezan,ezan1;
        TextView text,kalanVakit,kalanZaman;
        AppCompatTextView diyanet,ayetTitle;
        ConstraintLayout gunescons, imsakcons, oglecons, ikindicons, aksamcons, yatsicons;
        RelativeLayout menubtn;
        View v;
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
            kalanVakit= FindViewById<TextView>(Resource.Id.KalanVakit);
            kalanZaman= FindViewById<TextView>(Resource.Id.KalanZaman);
            ikindicons = FindViewById<ConstraintLayout>(Resource.Id.clytAsr);
        
            menubtn = FindViewById<RelativeLayout>(Resource.Id.mainRlytMenuBtn);
            menubtn.Click += delegate
            {
                onMainClick(v);
                /*Drawable backgnd = menubtn.Background;
                backgnd.SetTint(Resource.Color.colorPrimary);*/
            };
            
            /*  HttpClient client = new HttpClient();
              var response = await client.GetStringAsync("https://www.diyanet.gov.tr/tr-TR");
              HtmlDocument html = new HtmlDocument();
              html.LoadHtml(response);
              var Links = html.DocumentNode;

            string ayet= Links.SelectNodes("/html/body/div[2]/div/div[10]/div[1]/div[1]/a/p")[0].InnerText;
              FindViewById<AppCompatTextView>(Resource.Id.frgLocTvVerseContent).Text = ayet;*/
            // string link = "https://www.diyanethaber.com.tr" + a.Attributes["href"].Value;
            //List<string> DLink = new List<string>();
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
                    ezan1 = item;
                }
            }

            text.Text = "  " + ezan.GregDay + "\n" + ezan.GregAylar + "\n" + ezan.GregYear;
            FindViewById<TextView>(Resource.Id.tvImsak).Text = ezan.imsak;
            FindViewById<TextView>(Resource.Id.tvGunes).Text = ezan.gunes;
            FindViewById<TextView>(Resource.Id.tvOgle).Text = ezan.ogle;
            FindViewById<TextView>(Resource.Id.tvIkindi).Text = ezan.ikindi;
            FindViewById<TextView>(Resource.Id.tvAksam).Text = ezan.aksam;
            FindViewById<TextView>(Resource.Id.tvYatsi).Text = ezan.yatsi;
            FindViewById<TextView>(Resource.Id.mainHicriTakvim).Text = " " + ezan.HijriDay + "\n" + ezan.HijriMonthTr + "\n" + ezan.HijriYear;
            rcData = FindViewById<RecyclerView>(Resource.Id.recyclerViewHaftalikVakitler);
            /*ada = new RecycleAdapter(this, data);
             rcData.SetAdapter(ada);*/
            //RecyclerView.Adapter
            LoadHaftalikEzanVakti();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("https://www.diyanethaber.com.tr/diyanet-takvimi");
                HtmlDocument html = new HtmlDocument();
                html.LoadHtml(response);
                var Links = html.DocumentNode;

                var a = Links.SelectNodes("/html/body/main/div/div[2]/div[3]/div/div[1]/div[1]/div[1]/div[1]/a")[0];
                string link = "https://www.diyanethaber.com.tr" + a.Attributes["href"].Value;
                List<string> DLink = new List<string>();
                var response2 = await client.GetStringAsync(link);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response2);
                var programmerLinks = htmlDoc.DocumentNode;

                string ba = programmerLinks.SelectNodes("/html/body/main/div/div[3]/div[2]/div/div/div[1]/div[3]/div/p[1]")[0].InnerText;
                var baslik = programmerLinks.SelectNodes("/html/head/meta[8]")[0];
                string TakvimTitle = baslik.Attributes["content"].Value;
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarBackTitle).Text = TakvimTitle.ToUpper();
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarBackContent).Text = ba;
                diyanet = FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarBack);
                diyanet.Visibility = Android.Views.ViewStates.Visible;
            }
            catch (Exception ex)
            {
            }
            try
            {
                HttpClient client = new HttpClient();
                HtmlDocument html5 = new HtmlDocument();
                var response3 = await client.GetStringAsync("https://www.diyanethaber.com.tr/bir-ayet");
                html5.LoadHtml(response3);

                var Links2 = html5.DocumentNode;
                var ay = Links2.SelectNodes("/html/body/main/div/div[2]/div[3]/div/div[1]/div[1]/div[1]/div[1]/a")[0];
                string link7 = "https://www.diyanethaber.com.tr" + ay.Attributes["href"].Value;
                HtmlDocument html7 = new HtmlDocument();
                var response7 = await client.GetStringAsync(link7);
                html7.LoadHtml(response7);
                var Links7 = html7.DocumentNode;
                var ayet = Links7.SelectNodes("/html/body/main/div/div[3]/div[2]/div/div/div[1]/div[3]/div/p[1]/strong")[0].InnerText;
                int inde2 = ayet.IndexOf("(", ayet.Length - 30);
                string ayet2 = ayet.Remove(inde2);
                string ayet3 = ayet.Remove(0, inde2);
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvVerseContent).Text = ayet2;
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvVerseSource).Text = ayet3;
                ayetTitle =FindViewById<AppCompatTextView>(Resource.Id.frgLocTvVerseTitle);
                ayetTitle.Visibility = Android.Views.ViewStates.Visible;
            }
            catch(Exception ex) { }
        }

        /*        private void Timer_Elapsed1(object sender, ElapsedEventArgs e)
                {
                    DateTime simdi = DateTime.Now;
                    FindViewById<TextSwitcher>(Resource.Id.KalanVakit).SetCurrentText(vakit[0]);
                    FindViewById<TextSwitcher>(Resource.Id.KalanZaman).SetCurrentText(simdi.ToString("HH:mm:ss tt"));
                    FindViewById<TextView>(Resource.Id.mainYerelSaat).Text = DateTime.Now.ToString("HH:mm");
                }*/
       
        public void onMainClick(View v)
        {
            //AndroidX.AppCompat.Widget.PopupMenu menu = new AndroidX.AppCompat.Widget.PopupMenu(this, menubtn);
            //menu.MenuInflater.Inflate(Resource.Layout.activity_first_screen);
            StartActivity(new Intent(ApplicationContext, typeof(MenuActivity)));

        }
        void bekir(object sender, EventArgs e)
        {
         //   DateTime simdi = DateTime.Now;
          // FindViewById<TextSwitcher>(Resource.Id.KalanVakit).SetCurrentText("TT");
        // FindViewById<TextSwitcher>(Resource.Id.KalanZaman).SetCurrentText(simdi.ToString("HH:mm:ss tt"));
            FindViewById<AppCompatTextView>(Resource.Id.mainYerelSaat).Text = DateTime.Now.ToString("HH:mm");

            DateTime simdi = DateTime.Now;
            if (simdi.Hour <= Int32.Parse(ezan1.imsak.Remove(2)) && (simdi.Hour < Int32.Parse(ezan1.imsak.Remove(2)) || simdi.Minute < Int32.Parse(ezan1.imsak.Remove(0, 3))))
            {
                kalanVakit.Text ="İMSAK VAKTİNE KALAN";
                TimeSpan d = DateTime.Parse(ezan1.imsak).Subtract(DateTime.Parse(simdi.ToString("HH:mm:ss tt")));
                kalanZaman.Text = d.ToString();
                
            }
            else if (simdi.Hour <= Int32.Parse(ezan1.gunes.Remove(2)) && (simdi.Hour < Int32.Parse(ezan1.gunes.Remove(2)) || simdi.Minute < Int32.Parse(ezan1.gunes.Remove(0, 3))))
            {
                kalanVakit.Text = "GÜNES VAKTİNE KALAN";
                TimeSpan d = DateTime.Parse(ezan1.gunes).Subtract(DateTime.Parse(simdi.ToString("HH:mm:ss tt")));
                kalanZaman.Text = d.ToString();
            }
            else if (simdi.Hour <= Int32.Parse(ezan1.ogle.Remove(2)) && (simdi.Hour < Int32.Parse(ezan1.ogle.Remove(2)) || simdi.Minute < Int32.Parse(ezan1.ogle.Remove(0, 3))))
            {
                kalanVakit.Text = "ÖĞLE EZANINA KALAN";
                TimeSpan d = DateTime.Parse(ezan1.ogle).Subtract(DateTime.Parse(simdi.ToString("HH:mm:ss tt")));
                kalanZaman.Text = d.ToString();

            }
            else if (simdi.Hour <= Int32.Parse(ezan1.ikindi.Remove(2)) && (simdi.Hour < Int32.Parse(ezan1.ikindi.Remove(2)) || simdi.Minute < Int32.Parse(ezan1.ikindi.Remove(0, 3))))
            {
                kalanVakit.Text = "İKİNDİ EZANINA KALAN";
                TimeSpan d = DateTime.Parse(ezan1.ikindi).Subtract(DateTime.Parse(simdi.ToString("HH:mm:ss tt")));
                kalanZaman.Text = d.ToString();
                
                    



            }
            else if (simdi.Hour <= Int32.Parse(ezan1.aksam.Remove(2)) && (simdi.Hour < Int32.Parse(ezan1.aksam.Remove(2)) || simdi.Minute < Int32.Parse(ezan1.aksam.Remove(0, 3))))
            {
                kalanVakit.Text = "AKŞAM EZANINA KALAN";
                TimeSpan d = DateTime.Parse(ezan1.aksam).Subtract(DateTime.Parse(simdi.ToString("HH:mm:ss tt")));
                kalanZaman.Text = d.ToString();


            }
            else if (simdi.Hour <= Int32.Parse(ezan1.yatsi.Remove(2)) && (simdi.Hour < Int32.Parse(ezan1.yatsi.Remove(2)) || simdi.Minute < Int32.Parse(ezan1.yatsi.Remove(0, 3))))
            {
                kalanVakit.Text = "YATSI EZANINA KALAN";
                TimeSpan d = DateTime.Parse(ezan1.yatsi).Subtract(DateTime.Parse(simdi.ToString("HH:mm:ss tt")));
                kalanZaman.Text = d.ToString();
            }
            else
            {
                DateTime saat = new DateTime(simdi.Year, simdi.Month, simdi.Day, 0, 0, 0);
                DateTime saat1 = new DateTime(simdi.Year, simdi.Month, simdi.Day, 23, 59, 59);

                TimeSpan d = (DateTime.Parse(ezan1.imsak).Subtract(DateTime.Parse(saat.ToString("HH:mm:ss tt")))).Add(DateTime.Parse(saat1.ToString("HH:mm:ss tt")).Subtract(DateTime.Parse(simdi.ToString("HH:mm:ss tt"))));
              kalanZaman.Text = d.ToString();
                kalanVakit.Text = "İMSAK VAKTİNE KALAN";


            }

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