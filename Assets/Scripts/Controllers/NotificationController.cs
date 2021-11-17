using System;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationController: BaseController
{
    private const string AndroidNotificationId = "android_notification_id";

    public void CreateNotificationAndroid(int minute)
    {
        var androidSettingsChannel = new AndroidNotificationChannel
        {
            Id = AndroidNotificationId,
            Name = "Notifier",
            Description = "Description Notifier",
            Importance = Importance.High,
            CanBypassDnd = true,
            EnableLights = false,
            CanShowBadge = true,
            EnableVibration = false,
            LockScreenVisibility = LockScreenVisibility.Public
        };

        AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChannel);

        var androidNotification = new AndroidNotification
        {
            Color = Color.black,
            RepeatInterval = TimeSpan.FromMinutes(minute),
            Title = "Revard is Ready",
            Text = "Your revard is ready"
        };

        AndroidNotificationCenter.SendNotification(androidNotification, AndroidNotificationId);

    }

    public void DelleteAllNotification()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }
}

