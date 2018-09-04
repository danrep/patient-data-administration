using System;
using System.Collections.Generic;

namespace PatientDataAdministration.Core.DataTranslation
{
    public class InfoBipIngresUserPush
    {
        public List<Result> Results { get; set; }
        public int MessageCount { get; set; }
        public int PendingMessageCount { get; set; }
    }

    public class Price
    {
        public int PricePerMessage { get; set; }
        public string Currency { get; set; }
    }

    public class Result
    {
        public string MessageId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public string CleanText { get; set; }
        public string Keyword { get; set; }
        public DateTime ReceivedAt { get; set; }
        public int SmsCount { get; set; }
        public Price Price { get; set; }
        public string CallbackData { get; set; }
    }
}