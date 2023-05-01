using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzanVakti_Mobil
{
    [Activity(Label = "@string/app_name", MainLauncher =true , Theme ="@style/SplashScreen", NoHistory =true)]
    public class SplashActivity1 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        protected override async void OnResume()
        {
            base.OnResume();
            await SimulateStartup();
        }

        async Task SimulateStartup()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            StartActivity(new Intent(ApplicationContext,typeof(GirişEkrani)));
        }
    }
}