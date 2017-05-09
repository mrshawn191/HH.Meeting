namespace HH.Meeting.Internal.Models
{
    public class SmsMessage
    {
        public int Id { get; set; }

        public string To { get; set; }

        public string From { get; set; }

        public string Message { get; set; }

        public string UrlToDashboard { get; set; }

        public void Update(SmsMessage smsMessage)
        {
            To = smsMessage.To;
            From = smsMessage.From;
            Message = smsMessage.Message;
        }
    }
}