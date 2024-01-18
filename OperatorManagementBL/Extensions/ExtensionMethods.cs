using System;
using System.Globalization;

namespace OperatorManagementBL.Extensions
{
    public static class ExtensionMethods
    {
        public static string ToJalaliDateTime(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return (string.Format("{3} - {0}/{1}/{2}", pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date),date.ToString("HH:mm:ss")));
        }

        public static DateTime ToGeorgianDateTimeFromUnixTimeStamp(this long unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
