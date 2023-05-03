using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EzanVakti_Mobil.Resources;
using EzanVakti_Mobil.Resources.Ayarlar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EzanVakti_Mobil
{
    [Activity(Label = "@string/app_name", MainLauncher =true , Theme ="@style/SplashScreen", NoHistory =true)]
    public class SplashActivity1 : Activity
    {
        
        DateTime dt = DateTime.Now;
        CancellationTokenSource cts;
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
            if(dbCurrentExists())
            {
                Current current= new Current();
                // CurrentDatabase.selectTableCurrent(current);
                current = CurrentDatabase.selectTable();
                if(dt.Year==current.year&&dt.Month==current.month)
                {
                    StartActivity(new Intent(ApplicationContext, typeof(MainActivity)));
                }
                else
                {
                    var location = await GetCurrentLocation();
                    namazVaktiApi.enlem = location.Latitude.ToString();
                    namazVaktiApi.boylam = location.Longitude.ToString();
                    namazVaktiApi.ay = dt.Month;
                    namazVaktiApi.yil = dt.Year;
                    namazVaktiApi namazVakti = new namazVaktiApi();

                    await namazVakti.EzanSqlite();
                    namazVakti.CurrentUpdateTable();
                    StartActivity(new Intent(ApplicationContext, typeof(MainActivity)));
                }
               
            }
            else
            {
                StartActivity(new Intent(ApplicationContext, typeof(GirişEkrani)));
            }
            
        }
        public static bool dbCurrentExists()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(folder, "Current.db");
            return System.IO.File.Exists(path);
        }
        async Task<Location> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                return location;

            }

            catch (Exception ex)
            {
                return null;
                // Unable to get location
            }
        }
    }
}