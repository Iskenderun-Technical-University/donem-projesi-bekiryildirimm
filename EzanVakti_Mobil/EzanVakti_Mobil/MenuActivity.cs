using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzanVakti_Mobil
{
    [Activity(Label = "MenuActivity",Theme = "@style/AppTheme.CustomTheme")]
    public class MenuActivity : AppCompatActivity
    {
        LinearLayoutCompat settingbtn;
        RelativeLayout closebtn;
        View v;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
     SetContentView(Resource.Layout.activity_menu);
            // Create your application here
            closebtn = FindViewById<RelativeLayout>(Resource.Id.menuRlytCloseBtn);
            settingbtn = FindViewById<LinearLayoutCompat>(Resource.Id.menuLlytSettingsBtn);
            closebtn.Click += delegate
            {
                onMenuClick(v);
            };
            settingbtn.Click += delegate
            {
                onSettingClick(v);
            };

        }
        public void onMenuClick(View v) { 
       this.Finish();
        }
        public void onSettingClick(View v)
        {
            
           
            Bundle bundle = new Bundle();
            bundle.PutBoolean("status", true);

            FragmentMonthly aylikfragment = new FragmentMonthly();
            AndroidX.Fragment.App.FragmentManager manager = this.SupportFragmentManager;
            aylikfragment.Arguments = bundle;
            
            aylikfragment.Show(manager, "dialog");
            
           // Finish();

        }
    }
}