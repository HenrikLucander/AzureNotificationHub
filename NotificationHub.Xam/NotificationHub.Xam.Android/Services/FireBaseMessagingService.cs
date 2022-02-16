using Android.App;
using Android.Content;
using AndroidX.Core.App;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationHub.Xam.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFireBaseMessagingService : FirebaseMessagingService
    {
        internal static readonly string NOTIFICATION_CHANNEL_ID = "default_notification_channel_id";

        public override void OnNewToken(string newToken)
        {
            if (string.IsNullOrWhiteSpace(newToken))
            {
                return;
            }

            if (!(Xamarin.Forms.Application.Current?.MainPage is MainPage mainPage))
            {
                return;
            }

            mainPage.PnsToken = newToken;
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);

            string messageBody = remoteMessage.GetNotification()?.Body;

            if (messageBody is null)
            {
                messageBody = remoteMessage.Data.Values.FirstOrDefault() ?? string.Empty;
            }

            Intent intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.PutExtra("message", messageBody);

            // Unique require code to avoid PendingIntent collision 
            int requestCode = new Random().Next();
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, requestCode, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID)
                .SetContentTitle("Learning Notification Hubs")
                .SetSmallIcon(Resource.Mipmap.icon)
                .SetContentText(messageBody)
                .SetContentIntent(pendingIntent);

            NotificationManager notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}
