namespace Services.DialogView.SetupData
{
    public class NotificationSetupData
    {
        public string NotificationText { get; }
        public float Duration { get; }

        public NotificationSetupData(string notificationText, float duration)
        {
            NotificationText = notificationText;
            Duration = duration;
        }
    }
}