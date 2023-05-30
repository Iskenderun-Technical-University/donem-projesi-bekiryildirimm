using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EzanVakti_Mobil.Resources.VeriTabani;
using EzanVakti_Mobil.Resources;
using Google.Android.Material.BottomSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidX.RecyclerView.Widget;

namespace EzanVakti_Mobil
{
   // [Activity(Theme = "@style/Theme.Design.BottomSheetDialog")]
    public class FragmentMonthly : BottomSheetDialogFragment
    {
        bool hangisi=false;
        namazVaktiData ezan;
        List<namazVaktiData> data;
        veritabani veriTabani;
        RecyclerView recyclerView;
        recyclerViewMonthly adapter;
        public async override void OnCreate(Bundle savedInstanceState)
        {
          //  SetStyle(Android.App.DialogFragmentStyle.Normal, Resource.Style.AppTheme_BottomSheetFragmentStyle);
            base.OnCreate(savedInstanceState);
            //SetStyle()
            // SetStyle(Android.App.DialogFragmentStyle.NoTitle, Resource.Style.BottomSheetFragmentStyle);
            SetStyle(BottomSheetDialogFragment.StyleNormal,Resource.Style.BottomSheetFragmentStyle);
           // await Task.Delay(TimeSpan.FromSeconds(3));
            hangisi = Arguments.GetBoolean("status");
            ezan = new namazVaktiData();
            data = new List<namazVaktiData>();

            veriTabani = new veritabani();
            data = veriTabani.selectTable("NamazVakti.db");
            adapter = new recyclerViewMonthly(this, data, hangisi);
 

            // Create your fragment here
        }
        public override void OnResume()
        {
            
            base.OnResume();
            
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

           base.OnCreateView(inflater, container, savedInstanceState);
           
            var view = inflater.Inflate(Resource.Layout.fragment_bottomsheet_monthly_prayer_times, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.bsMonthlyRvPrayerTimes);
            recyclerView.SetAdapter(adapter);
            view.FindViewById<TextView>(Resource.Id.bsMonthlyTvLocation).Text = namazVaktiApi.city;
      
            view.FindViewById<RelativeLayout>(Resource.Id.bsMonthlyLlytWait).Visibility = ViewStates.Gone;
   
            return view;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
          //  Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.Animation_Design_BottomSheetDialog;
            
        }
    }
}