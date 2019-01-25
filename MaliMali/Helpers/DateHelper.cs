using System;
namespace MaliMali.Helpers
{
    public class DateHelper
    {
        public enum Format
        {
            DayDateMonthHour,
            Day,
            Month,
            Year,
            DayMonth,
            Time,
            Time24Hour,
            FullDate,
            FullDateGMT,
            MonthYear
        }

        readonly string DayDateMonthHour = "ddd, dd MMM h:mm tt";
        readonly string Day = "ddd";
        readonly string Year = "yyyy";
        readonly string FullDate = "dddd, dd MMMM yyyy";
        readonly string DayMonth = "dd MMMM";
        readonly string Time = "hh:mm tt";
        readonly string Time24Hour = "HH:mm";
        readonly string FullDateGMT = "ddd, dd MMM yyy HH':'mm':'ss 'GMT'";
        readonly string MonthYear = "MMM yyyy";

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

        public long ReturnCurrentTimeInLong()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public long ReturnAddedDaysInLongFromLong(long date, int days)
        {
            DateTimeOffset oDate = DateTimeOffset.FromUnixTimeMilliseconds(date);
            return oDate.AddDays(days).ToUnixTimeMilliseconds();
        }

        public long ReturnDateLongFromString(string date)
        {
            DateTime oDate = Convert.ToDateTime(date);
            return ((DateTimeOffset)oDate).ToUnixTimeMilliseconds();
        }

        public long ReturnLongFromDateTime(DateTime date)
        {
            return (long)(date - UnixEpoch).TotalMilliseconds;
        }

        public DateTime ReturnDateTimeFromlong(long date)
        {
            return UnixEpoch.AddMilliseconds(date);
        }

        public long ReturnLongAfterAddingTimeSpan(TimeSpan time, long date)
        {
            var datetime = ReturnDateTimeFromlong(date);
            var newdatetime = datetime.Add(time);
            return ReturnLongFromDateTime(newdatetime);
        }

        public string ReturnDateStringFromLong(Format format, long date)
        {
            DateTime datetime = UnixEpoch.AddMilliseconds(date).ToLocalTime();
            string dateString = "";

            switch (format)
            {
                case Format.DayDateMonthHour:
                    dateString = datetime.ToString(DayDateMonthHour);
                    break;
                case Format.Day:
                    dateString = datetime.ToString(Day);
                    break;
                case Format.Year:
                    dateString = datetime.ToString(Year);
                    break;
                case Format.FullDate:
                    dateString = datetime.ToString(FullDate);
                    break;
                case Format.DayMonth:
                    dateString = datetime.ToString(DayMonth);
                    break;
                case Format.Time:
                    dateString = datetime.ToString(Time);
                    break;
                case Format.Time24Hour:
                    dateString = datetime.ToString(Time24Hour);
                    break;
                case Format.FullDateGMT:
                    dateString = datetime.ToString(FullDateGMT);
                    break;
            }

            return dateString;
        }

        public static DateHelper Instance { get; } = new DateHelper();
    }
}
