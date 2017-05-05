namespace HH.Meeting.Internal.Models
{
    public class SmsMessage
    {
        public int Id { get; set; }

        public int To { get; set; }

        public int From { get; set; }

        public string Message { get; set; }

        public void Update(SmsMessage smsMessage)
        {
            To = smsMessage.To;
            From = smsMessage.From;
            Message = smsMessage.Message;
        }
    }
}