using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace OperatorManagementBL.Extensions
{
    //Extension Methods
    public static class ExtensionMethods
    {
        //تبدیل تاریخ میلادی به شمسی
        public static string ToJalaliDateTime(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return (string.Format("{3} - {0}/{1}/{2}", pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date),date.ToString("HH:mm:ss")));
        }

        //تبدیل تایم‌استمپ یونیکس به تاریخ میلادی
        //استفاده شده در پلاگین PersianDateTimePicker
        public static DateTime ToGeorgianDateTimeFromUnixTimeStamp(this long unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        //هش کردن پسورد
        public static string ToSha256Hash(this string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashedBytes = sha256.ComputeHash(inputBytes);
                string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedString;
            }
        }
    }
}
