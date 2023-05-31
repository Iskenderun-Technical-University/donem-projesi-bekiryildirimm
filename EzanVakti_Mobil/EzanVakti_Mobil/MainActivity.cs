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
using Android.Renderscripts;
using AndroidX.Transitions;
using EzanVakti_Mobil.Resources.AlarmAyarlari;
using Java.Lang;
using Android.Media;
using Android.Content.Res;
using System.Runtime.Remoting.Contexts;
using Java.Security;
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
        List<namazVaktiData> data,weeklydata,alarmweeklydata,alarmdata;
        veritabani veriTabani;
        DateTime bugun = DateTime.Now;
        namazVaktiData ezan,ezan1;
        TextView kalanVakit,kalanZaman;
        AppCompatTextView diyanet,ayetTitle,hicritxt,miladitxt,dhurur;
        ConstraintLayout gunesclyt, imsakclyt, ogleclyt, ikindiclyt, aksamclyt, yatsiclyt;
        RelativeLayout menubtn;
        View v;
       System.Timers.Timer timer;


        VakitOncesiAlarmAyarlari vakitOncesi;
        VakitOncesiAlarmDatabase vakitOncesiDatabase;
        VaktindeAlarmAyarlari vaktinde;
        VaktindeAlarmDatabase vaktindeDatabase;


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
            miladitxt = FindViewById<AppCompatTextView>(Resource.Id.AnaSayfaTarih);
            kalanVakit = FindViewById<TextView>(Resource.Id.KalanVakit);
            kalanZaman = FindViewById<TextView>(Resource.Id.KalanZaman);
            hicritxt = FindViewById<AppCompatTextView>(Resource.Id.mainHicriTakvim);
            menubtn = FindViewById<RelativeLayout>(Resource.Id.mainRlytMenuBtn);
            imsakclyt = FindViewById<ConstraintLayout>(Resource.Id.clytFajr);
            gunesclyt = FindViewById<ConstraintLayout>(Resource.Id.clytSun);
            ogleclyt = FindViewById<ConstraintLayout>(Resource.Id.clytDhuhr);
            ikindiclyt = FindViewById<ConstraintLayout>(Resource.Id.clytAsr);
            aksamclyt = FindViewById<ConstraintLayout>(Resource.Id.clytMaghrib);
            yatsiclyt = FindViewById<ConstraintLayout>(Resource.Id.clytIsha);
            // dhurur = FindViewById<AppCompatTextView>(Resource.Id.tvDhuhrTitle);
            //   dhurur.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Black));
            //  imsakcons.bac
            /* MediaPlayer media = new MediaPlayer();
             AssetManager asset = this.Assets;
             AssetFileDescriptor desc = asset.OpenFd("sounds/uyaritonu.mp3");
            await media.SetDataSourceAsync(desc);
             media.Prepare();
             media.Start();*/

            imsakclyt.Click += delegate
            {
                imsakClick(v);

            };
            gunesclyt.Click += delegate
             {
                 gunesClick(v);

             };
            ogleclyt.Click += delegate
             {
                 ogleClick(v);

             };
            ikindiclyt.Click += delegate
             {
                 ikindiClick(v);

             };
            aksamclyt.Click += delegate
             {
                 aksamClick(v);

             };

            yatsiclyt.Click += delegate
            {
                yatsiClick(v);

            };
            menubtn.Click += delegate
            {
                onMainClick(v);
                /*Drawable backgnd = menubtn.Background;
                backgnd.SetTint(Resource.Color.colorPrimary);*/
            };

            hicritxt.Click += delegate
            {
                onHijriClick(v);
            };
            miladitxt.Click += delegate
            {
                onMiladiClick(v);
            };

            DateTime dt = DateTime.Now;


            ezan = new namazVaktiData();
            data = new List<namazVaktiData>();

            veriTabani = new veritabani();
            data = veriTabani.selectTable("NamazVakti.db");



            foreach (var item in data)
            {
                if (item.GregDay == bugun.Day && item.GregMonthNumber == bugun.Month)
                {
                    ezan = item;
                    ezan1 = item;
                }
            }

            miladitxt.Text = "  " + ezan.GregDay + "\n" + ezan.GregAylar + "\n" + ezan.GregYear;
            FindViewById<TextView>(Resource.Id.tvImsak).Text = ezan.imsak;
            FindViewById<TextView>(Resource.Id.tvGunes).Text = ezan.gunes;
            FindViewById<TextView>(Resource.Id.tvOgle).Text = ezan.ogle;
            FindViewById<TextView>(Resource.Id.tvIkindi).Text = ezan.ikindi;
            FindViewById<TextView>(Resource.Id.tvAksam).Text = ezan.aksam;
            FindViewById<TextView>(Resource.Id.tvYatsi).Text = ezan.yatsi;

            hicritxt.Text = " " + ezan.HijriDay + "\n" + ezan.HijriMonthTr + "\n" + ezan.HijriYear;
            rcData = FindViewById<RecyclerView>(Resource.Id.recyclerViewHaftalikVakitler);


            LoadHaftalikEzanVakti();
            VaktindeSetAlarm();
            try
            {

                Diyanet diyanetWeb = new Diyanet();
     
                await diyanetWeb.CallBirAyet();
       

     
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvVerseContent).Text = diyanetWeb.BirAyet;
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvVerseSource).Text = diyanetWeb.titleAyet;




                ayetTitle = FindViewById<AppCompatTextView>(Resource.Id.frgLocTvVerseTitle);
                ayetTitle.Visibility = Android.Views.ViewStates.Visible;

         
                FindViewById<ConstraintLayout>(Resource.Id.clytBirAyet).Visibility = Android.Views.ViewStates.Visible;


            
            }
            catch (System.Exception ex)
            {
            }
            try
            {
                Diyanet diyanetWeb = new Diyanet();
                await diyanetWeb.CallBirHadis();
                if(diyanetWeb.BirHadis==null||diyanetWeb.titleHadis==null)
                {
                    await diyanetWeb.CallBirHadis2();
                }
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvHadithContent).Text = diyanetWeb.BirHadis;
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvHadithSource).Text = diyanetWeb.titleHadis;
                FindViewById<ConstraintLayout>(Resource.Id.clytBirHadis).Visibility = Android.Views.ViewStates.Visible;
            }
            catch (System.Exception ex)
            {
 
            }
            try
            {
                Diyanet diyanetWeb = new Diyanet();
                await diyanetWeb.CallTakvimArka();
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarBackTitle).Text = diyanetWeb.titleTakvimarka;
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarBackContent).Text = diyanetWeb.TakvimArka;
              
                diyanet = FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarBack);
                
                FindViewById<ConstraintLayout>(Resource.Id.clytTakvimArka).Visibility = Android.Views.ViewStates.Visible;
                diyanet.Visibility = Android.Views.ViewStates.Visible;
                if(diyanetWeb.TakvimOn!=null)
                {
                    FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarFront).Text = diyanetWeb.TakvimOn;
                    FindViewById<ConstraintLayout>(Resource.Id.clytTakvimOn).Visibility = Android.Views.ViewStates.Visible;
                }


            }
            catch(System.Exception e)
            {
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarBackContent).Text = e.Message;

                diyanet = FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarBack);

                FindViewById<ConstraintLayout>(Resource.Id.clytTakvimArka).Visibility = Android.Views.ViewStates.Visible;
                diyanet.Visibility = Android.Views.ViewStates.Visible;
            }

                //Diyanet diyanetWeb1 = new Diyanet();
                //await diyanetWeb1.CallTakvimArka();
            //    FindViewById<AppCompatTextView>(Resource.Id.frgLocTvCalendarFront).Text = diyanetWeb1.TakvimOn;
              //  FindViewById<ConstraintLayout>(Resource.Id.clytTakvimOn).Visibility = Android.Views.ViewStates.Visible;

            try
            {
                Diyanet diyanetWeb = new Diyanet();
                await diyanetWeb.CallBirDua();
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvPrayerContent).Text = diyanetWeb.BirDua;
                FindViewById<AppCompatTextView>(Resource.Id.frgLocTvPrayerSource).Text = diyanetWeb.titleDua;
                FindViewById<ConstraintLayout>(Resource.Id.clytBirDua).Visibility = Android.Views.ViewStates.Visible;
            }
            catch (System.Exception ex)
            {

            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(data.GetStringExtra("menu")=="miladi")
            {
                            Bundle bundle = new Bundle();
            bundle.PutBoolean("status", true);
            
            FragmentMonthly aylikfragment = new FragmentMonthly();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            aylikfragment.Arguments = bundle;
            aylikfragment.Show(manager, "dialog");
            }
            else if (data.GetStringExtra("menu") == "hicri")
            {
                Bundle bundle = new Bundle();
                bundle.PutBoolean("status", false);

                FragmentMonthly aylikfragment = new FragmentMonthly();
                AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
                aylikfragment.Arguments = bundle;
                aylikfragment.Show(manager, "dialog");
            }
            else if (data.GetStringExtra("menu") == "konum")
            {
                Bundle bundle = new Bundle();
                bundle.PutBoolean("status", false);

                FragmentLocation aylikfragment = new FragmentLocation();
                AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
                aylikfragment.Arguments = bundle;
                aylikfragment.Show(manager, "dialog");
            }
        }
        public void imsakClick(View v)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("vakit", "imsak");
            FragmentAlarm fragmentAlarm = new FragmentAlarm();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            fragmentAlarm.Arguments = bundle;
            fragmentAlarm.Show(manager, "dialog");
        }

       /* public static void cal()
        {
                        MediaPlayer media = new MediaPlayer();
            AssetManager asset = this.Assets;
            AssetFileDescriptor desc = asset.OpenFd("sounds/uyaritonu.mp3");
           await media.SetDataSourceAsync(desc);
            media.Prepare();
            media.Start();
        }*/

        
        public void gunesClick(View v)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("vakit", "güneş");
            FragmentAlarm fragmentAlarm = new FragmentAlarm();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            fragmentAlarm.Arguments = bundle;
            fragmentAlarm.Show(manager, "dialog");
        }
        public void ogleClick(View v)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("vakit", "öğle");
            FragmentAlarm fragmentAlarm = new FragmentAlarm();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            fragmentAlarm.Arguments = bundle;
            fragmentAlarm.Show(manager, "dialog");
           
        }
        public void ikindiClick(View v)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("vakit", "ikindi");
            FragmentAlarm fragmentAlarm = new FragmentAlarm();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            fragmentAlarm.Arguments = bundle;
            fragmentAlarm.Show(manager, "dialog");
           
        }
        public void aksamClick(View v)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("vakit", "akşam");
            FragmentAlarm fragmentAlarm = new FragmentAlarm();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            fragmentAlarm.Arguments = bundle;
            fragmentAlarm.Show(manager, "dialog");
        }
        public void yatsiClick(View v)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("vakit", "yatsı");
            FragmentAlarm fragmentAlarm = new FragmentAlarm();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            fragmentAlarm.Arguments = bundle;
            fragmentAlarm.Show(manager, "dialog");
        }
        public void onMiladiClick(View v)
        {
            Bundle bundle = new Bundle();
            bundle.PutBoolean("status", true);
            
            FragmentMonthly aylikfragment = new FragmentMonthly();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            aylikfragment.Arguments = bundle;
            aylikfragment.Show(manager, "dialog");
        }


        public Task vaktindeDatabaseOlustur()
        {
            vaktinde = new VaktindeAlarmAyarlari();
            vaktindeDatabase = new VaktindeAlarmDatabase();
            //vaktindeDatabase.createDataBaseVaktindeAlarm();
            vaktinde.imsakAlarm = false;
            vaktinde.gunesAlarm = false;
            vaktinde.ogleAlarm = false;
            vaktinde.ikindiAlarm = false;
            vaktinde.aksamAlarm = false;
            vaktinde.yatsiAlarm = false;
            vaktindeDatabase.InsertIntoTableVaktindeAlarm(vaktinde);
            return Task.CompletedTask;
        }
        public Task vakitOncesiDataBaseOlustur()
        {
            vakitOncesi = new VakitOncesiAlarmAyarlari();
            vakitOncesiDatabase = new VakitOncesiAlarmDatabase();
            vakitOncesiDatabase.createDataBaseVaktindeAlarm();
            vakitOncesi.imsakAlarm = false;
            vakitOncesi.gunesAlarm = false;
            vakitOncesi.ogleAlarm = false;
            vakitOncesi.ikindiAlarm = false;
            vakitOncesi.aksamAlarm = false;
            vakitOncesi.yatsiAlarm = false;
            vakitOncesi.imsakdkOncesi = 5;
            vakitOncesi.gunesdkOncesi = 5;
            vakitOncesi.ogledkOncesi = 5;
            vakitOncesi.ikindidkOncesi = 5;
            vakitOncesi.aksamdkOncesi = 5;
            vakitOncesi.yatsidkOncesi = 5;
            vakitOncesiDatabase.InsertIntoTableVaktindeAlarm(vakitOncesi);
            return Task.CompletedTask;
        }
        public void onHijriClick(View v)
        {
            Bundle bundle = new Bundle();
            bundle.PutBoolean("status", false);

            FragmentMonthly aylikfragment = new FragmentMonthly();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            aylikfragment.Arguments = bundle;
            aylikfragment.Show(manager, "dialog");


        }
        public void onMainClick(View v)
        {
            //AndroidX.AppCompat.Widget.PopupMenu menu = new AndroidX.AppCompat.Widget.PopupMenu(this, menubtn);
            //menu.MenuInflater.Inflate(Resource.Layout.activity_first_screen);
         //   StartActivity(new Intent(ApplicationContext, typeof(MenuActivity)));
            StartActivityForResult(new Intent(ApplicationContext, typeof(MenuActivity)), 12);

        }

        private void VaktindeSetAlarm2()
        {
            vaktinde = new VaktindeAlarmAyarlari();
            vaktinde = VaktindeAlarmDatabase.selectTable();
            DateTime dt = new DateTime(Int32.Parse(ezan.GregYear), ezan1.GregMonthNumber,ezan1.GregDay, Int32.Parse(ezan1.ikindi.Remove(2)), Int32.Parse(ezan1.ikindi.Remove(0, 3)), 0);
            DateTimeOffset dateOffsetValue = DateTimeOffset.Parse(dt.ToString());
            var am = GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
            var millisec = dateOffsetValue.ToUnixTimeMilliseconds();
            var requestCod = (int)millisec / 1000;
            var textTimer = new StringBuilder();
            var intent=new Intent(ApplicationContext, typeof(MyAlarmReceiver));
            //intent.PutExtra("Request_Code", requestCod);
            intent.AddFlags(ActivityFlags.IncludeStoppedPackages);
            intent.AddFlags(ActivityFlags.ReceiverForeground);
            var pi = PendingIntent.GetBroadcast(this, requestCod, intent, 0);
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                am.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, millisec, pi);
            else
                am.SetExact(AlarmType.RtcWakeup,millisec, pi);
           
        }
        public long date2mSecond(int yil,int ay,int gun,int saat,int dakika,int saniye)
        {
            DateTime dt1 = new DateTime(yil, ay, gun, saat, dakika, saniye);
            DateTime simdi = DateTime.Now;
           if(simdi.CompareTo(dt1)>0)
            {
                return 0;
            }
            else
            {
                DateTimeOffset dateOffsetValue1 = DateTimeOffset.Parse(dt1.ToString());

                return dateOffsetValue1.ToUnixTimeMilliseconds();
            }

        }
        public long date2mSecond(int yil, int ay, int gun, int saat, int dakika, int saniye,int minusdk)
        {
            DateTime dt1 = new DateTime(yil, ay, gun, saat, dakika, saniye);
            //  DateTime.Parse(dt1).Subtract(TimeSpan.FromMinutes((double)minusdk));
            //DateTime.Parse(.ToString("HH:mm:ss tt")));
            DateTime simdi = DateTime.Now;
            if(simdi.CompareTo(dt1.Subtract(TimeSpan.FromMinutes((double)minusdk))) >0)
            {
                return 0;
            }
            else
            {
                DateTimeOffset dateOffsetValue1 = DateTimeOffset.Parse(dt1.Subtract(TimeSpan.FromMinutes((double)minusdk)).ToString());

                return dateOffsetValue1.ToUnixTimeMilliseconds();
            }


        }
        public void VaktindeSetAlarm()
        {
            vaktinde = new VaktindeAlarmAyarlari();
            vaktinde = VaktindeAlarmDatabase.selectTable();
            vakitOncesi = new VakitOncesiAlarmAyarlari();
            vakitOncesi=VakitOncesiAlarmDatabase.selectTable();
            alarmdata = new List<namazVaktiData>();
            alarmweeklydata = new List<namazVaktiData>();
            veriTabani = new veritabani();
            alarmdata = veriTabani.selectTable("NamazVakti.db");

            foreach (var item in alarmdata)
            {
                if (bugun.Day <= item.GregDay && bugun.Day + 6 >= item.GregDay && bugun.Month == item.GregMonthNumber)
                    alarmweeklydata.Add(item);
            }
            foreach (var ezan1 in alarmweeklydata)
                {
           // DateTime imsakDate = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.imsak.Remove(2)), Int32.Parse(ezan1.imsak.Remove(0, 3)), 0);
           // DateTimeOffset imsakOffset = DateTimeOffset.Parse(imsakDate.ToString());
            //long imsakMs = imsakOffset.ToUnixTimeMilliseconds();

                
                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.imsak.Remove(2)), Int32.Parse(ezan1.imsak.Remove(0, 3)), 0), vaktinde.imsakAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.gunes.Remove(2)), Int32.Parse(ezan1.gunes.Remove(0, 3)), 0), vaktinde.gunesAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ogle.Remove(2)), Int32.Parse(ezan1.ogle.Remove(0, 3)), 0), vaktinde.ogleAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ikindi.Remove(2)), Int32.Parse(ezan1.ikindi.Remove(0, 3)), 0), vaktinde.ikindiAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.yatsi.Remove(2)), Int32.Parse(ezan1.yatsi.Remove(0, 3)), 0), vaktinde.yatsiAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.aksam.Remove(2)), Int32.Parse(ezan1.aksam.Remove(0, 3)), 0), vaktinde.aksamAlarm);




                

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.imsak.Remove(2)), (Int32.Parse(ezan1.imsak.Remove(0, 3))), 0,vakitOncesi.imsakdkOncesi), vakitOncesi.imsakAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.gunes.Remove(2)), Int32.Parse(ezan1.gunes.Remove(0, 3)), 0,vakitOncesi.gunesdkOncesi), vakitOncesi.gunesAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ogle.Remove(2)), Int32.Parse(ezan1.ogle.Remove(0, 3)), 0,vakitOncesi.ogledkOncesi), vakitOncesi.ogleAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ikindi.Remove(2)), Int32.Parse(ezan1.ikindi.Remove(0, 3)), 0,vakitOncesi.ikindidkOncesi), vakitOncesi.ikindiAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.yatsi.Remove(2)), Int32.Parse(ezan1.yatsi.Remove(0, 3)), 0,vakitOncesi.aksamdkOncesi) , vakitOncesi.yatsiAlarm);

                setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.aksam.Remove(2)), Int32.Parse(ezan1.aksam.Remove(0, 3)), 0,vakitOncesi.yatsidkOncesi) , vakitOncesi.aksamAlarm);
                




                /* DateTime gunesDate = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.gunes.Remove(2)), Int32.Parse(ezan1.gunes.Remove(0, 3)), 0);
                 DateTimeOffset gunesOffset = DateTimeOffset.Parse(gunesDate.ToString());
                 long gunesMs = gunesOffset.ToUnixTimeMilliseconds();


                 DateTime ogleDate = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ogle.Remove(2)), Int32.Parse(ezan1.ogle.Remove(0, 3)), 0);
                 DateTimeOffset ogleOffset = DateTimeOffset.Parse(ogleDate.ToString());
                 long ogleMs = ogleOffset.ToUnixTimeMilliseconds();
                 setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ogle.Remove(2)), Int32.Parse(ezan1.ogle.Remove(0, 3)), 0), vaktinde.ogleAlarm);

                 DateTime ikindiDate = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ikindi.Remove(2)), Int32.Parse(ezan1.ikindi.Remove(0, 3)), 0);
                 DateTimeOffset ikindiOffset = DateTimeOffset.Parse(ikindiDate.ToString());
                 long ikindiMs = ikindiOffset.ToUnixTimeMilliseconds();


                 DateTime aksamDate = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.aksam.Remove(2)), Int32.Parse(ezan1.aksam.Remove(0, 3)), 0);
                 DateTimeOffset aksamOffset = DateTimeOffset.Parse(aksamDate.ToString());
                 long aksamMs = aksamOffset.ToUnixTimeMilliseconds();
                 setAlarm(date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.aksam.Remove(2)), Int32.Parse(ezan1.aksam.Remove(0, 3)), 0), vaktinde.aksamAlarm);

                 DateTime yatsiDate = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.yatsi.Remove(2)), Int32.Parse(ezan1.yatsi.Remove(0, 3)), 0);
                 DateTimeOffset yatsiOffset = DateTimeOffset.Parse(yatsiDate.ToString());
                 long yatsiMs = yatsiOffset.ToUnixTimeMilliseconds();



                 date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.gunes.Remove(2)), Int32.Parse(ezan1.gunes.Remove(0, 3)), 0);
                 date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ogle.Remove(2)), Int32.Parse(ezan1.ogle.Remove(0, 3)), 0);
                 date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ikindi.Remove(2)), Int32.Parse(ezan1.ikindi.Remove(0, 3)), 0);
                 date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.aksam.Remove(2)), Int32.Parse(ezan1.aksam.Remove(0, 3)), 0);
                 date2mSecond(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.yatsi.Remove(2)), Int32.Parse(ezan1.yatsi.Remove(0, 3)), 0);
                  * DateTime dt1 = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.gunes.Remove(2)), Int32.Parse(ezan1.gunes.Remove(0, 3)), 0);
                  DateTimeOffset dateOffsetValue1 = DateTimeOffset.Parse(dt1.ToString());
                  var am1 = GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                  var millisec1 = dateOffsetValue1.ToUnixTimeMilliseconds();
                  var requestCod1 = (int)millisec1 / 1000;
                  var textTimer1 = new StringBuilder();
                  var intent1 = new Intent(ApplicationContext, typeof(MyAlarmReceiver));
                  //intent.PutExtra("Request_Code", requestCod);
                  intent.AddFlags(ActivityFlags.IncludeStoppedPackages);
                  intent.AddFlags(ActivityFlags.ReceiverForeground);
                  var pi1 = PendingIntent.GetBroadcast(this, requestCod1, intent1, 0);
                  if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                      am1.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, millisec1, pi1);
                  else
                      am1.SetExact(AlarmType.RtcWakeup, millisec1, pi1);
                  if (!vaktinde.gunesAlarm)
                      am1.Cancel(pi1);

                  DateTime dt2 = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ogle.Remove(2)), Int32.Parse(ezan1.ogle.Remove(0, 3)), 0);
                  DateTimeOffset dateOffsetValue2 = DateTimeOffset.Parse(dt2.ToString());
                  var am2 = GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                  var millisec2 = dateOffsetValue2.ToUnixTimeMilliseconds();
                  var requestCod2 = (int)millisec2 / 1000;
                  var textTimer2 = new StringBuilder();
                  var intent2 = new Intent(ApplicationContext, typeof(MyAlarmReceiver));
                  //intent.PutExtra("Request_Code", requestCod);
                  intent2.AddFlags(ActivityFlags.IncludeStoppedPackages);
                  intent2.AddFlags(ActivityFlags.ReceiverForeground);
                  var pi2 = PendingIntent.GetBroadcast(this, requestCod2, intent2, 0);
                  if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                      am2.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, millisec2, pi2);
                  else
                      am2.SetExact(AlarmType.RtcWakeup, millisec2, pi2);
                  if (!vaktinde.ogleAlarm)
                      am2.Cancel(pi2);

                  DateTime dt3 = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ikindi.Remove(2)), Int32.Parse(ezan1.ikindi.Remove(0, 3)), 0);
                  DateTimeOffset dateOffsetValue3 = DateTimeOffset.Parse(dt3.ToString());
                  long millisec3 = dateOffsetValue3.ToUnixTimeMilliseconds();
                  var requestCod3 = (int)millisec3 / 1000;
                  var am3 = GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                  var textTimer3 = new StringBuilder();
                  var intent3 = new Intent(ApplicationContext, typeof(MyAlarmReceiver));
                  //intent.PutExtra("Request_Code", requestCod);
                  intent3.AddFlags(ActivityFlags.IncludeStoppedPackages);
                  intent3.AddFlags(ActivityFlags.ReceiverForeground);
                  var pi3 = PendingIntent.GetBroadcast(this, requestCod3, intent3, 0);
                  if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                      am3.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, millisec, pi);
                  else
                      am3.SetExact(AlarmType.RtcWakeup, millisec3, pi3);
                  if (!vaktinde.ikindiAlarm)
                      am3.Cancel(pi3);*/

                /*  DateTime dt3 = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.aksam.Remove(2)), Int32.Parse(ezan1.ikindi.Remove(0, 3)), 0);
                  DateTimeOffset dateOffsetValue3 = DateTimeOffset.Parse(dt3.ToString());
                  var millisec3 = dateOffsetValue.ToUnixTimeMilliseconds();
                  setAlarm()

                  var am3 = GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
                  long millisec3 = dateOffsetValue.ToUnixTimeMilliseconds();
                  var requestCod3 = (int)millisec / 1000;
                  var textTimer3 = new StringBuilder();
                  var intent3 = new Intent(ApplicationContext, typeof(MyAlarmReceiver));
                  //intent.PutExtra("Request_Code", requestCod);
                  intent3.AddFlags(ActivityFlags.IncludeStoppedPackages);
                  intent3.AddFlags(ActivityFlags.ReceiverForeground);
                  var pi3 = PendingIntent.GetBroadcast(this, requestCod3, intent3, 0);
                  if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                      am3.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, millisec, pi);
                  else
                      am3.SetExact(AlarmType.RtcWakeup, millisec3, pi3);
                  if (!vaktinde.ikindiAlarm)
                      am3.Cancel(pi3);*/

            }



            /* DateTime dt = new DateTime(Int32.Parse(ezan1.GregYear), ezan1.GregMonthNumber, ezan1.GregDay, Int32.Parse(ezan1.ikindi.Remove(2)), Int32.Parse(ezan1.ikindi.Remove(0, 3)), 0);
             DateTimeOffset dateOffsetValue = DateTimeOffset.Parse(dt.ToString());
             var am = GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
             var millisec = dateOffsetValue.ToUnixTimeMilliseconds();
             var requestCod = (int)millisec / 1000;
             var textTimer = new StringBuilder();
             var intent = new Intent(ApplicationContext, typeof(MyAlarmReceiver));
             //intent.PutExtra("Request_Code", requestCod);
             intent.AddFlags(ActivityFlags.IncludeStoppedPackages);
             intent.AddFlags(ActivityFlags.ReceiverForeground);
             var pi = PendingIntent.GetBroadcast(this, requestCod, intent, 0);
             if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                 am.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, millisec, pi);
             else
                 am.SetExact(AlarmType.RtcWakeup, millisec, pi);*/

        }

        private void setAlarm(long millisec,bool kontrol)
        {

            DateTime a = DateTime.Now;
           
            int requestCod = (int)millisec / 1000;
            StringBuilder textTimer = new StringBuilder();
            
            Intent intent = new Intent(ApplicationContext, typeof(MyAlarmReceiver));
            //intent.PutExtra("Request_Code", requestCod);
            intent.AddFlags(ActivityFlags.IncludeStoppedPackages);
            intent.AddFlags(ActivityFlags.ReceiverForeground);
            var pi = PendingIntent.GetBroadcast(this, requestCod, intent, 0);
            AlarmManager am = GetSystemService(Android.Content.Context.AlarmService) as AlarmManager;
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                am.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, millisec, pi);
            else
                am.SetExact(AlarmType.RtcWakeup, millisec, pi);
            if (!kontrol||millisec==0)
                am.Cancel(pi);
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
                dhurur.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Black));

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
                if(bugun.Day<=item.GregDay&&bugun.Day+6>=item.GregDay&&bugun.Month==item.GregMonthNumber)
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