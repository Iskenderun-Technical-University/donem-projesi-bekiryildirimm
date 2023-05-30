using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Icu.Text.CaseMap;

namespace EzanVakti_Mobil
{
    [BroadcastReceiver]
    public class MyAlarmReceiver : BroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
                  Android.Net.Uri notification = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            Ringtone r = RingtoneManager.GetRingtone(context, notification);
             r.Play();

           /* var pending = PendingIntent.GetActivity(context, 0,
    intent,
    PendingIntentFlags.CancelCurrent);

            var builder = new Notification.Builder(context)
                  .SetContentTitle("title")
  .SetContentText("message")  
  .SetDefaults(NotificationDefaults.All)
                 .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm));

            builder.SetContentIntent(pending);
            var notific = builder.Build();
            var manage = NotificationManager.FromContext(context);
            manage.Notify(0, notific);
            MediaPlayer player;
            player = MediaPlayer.Create(context, Resource.Raw.uyaritonu);
            player.Start();*/
            // var requestCod = intent.GetIntExtra("Request_Code", -1);
            Toast.MakeText(context, "Haydi Namaza!", ToastLength.Short).Show();
            
        }
    }
}