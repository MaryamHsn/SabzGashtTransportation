using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Utils
{ 
    public static class ConvertDate
    {
        public static string GetTimeSpanStringInMonthsAndDays(this TimeSpan timeSpanParam)
        {
            var months = timeSpanParam.Days / 30;
            var days = timeSpanParam.Days % 30;
            return $"{months} ماه و {days} روز";
        }

        //public static SqlDateTime ToSqlserverDateTime(this DateTime dateTimeParm)
        //{
        //    string dateTimeString = dateTimeParm.ToString("yyyy-MM-dd HH:mm:ss:ffff");
        //    var sqlDateTime = SqlDateTime.Parse(dateTimeString);
        //    return sqlDateTime;
        //}

        public static DateTime ToSqlserverDateTime(this DateTime dateTimeParm)
        {
            string dateTimeString = dateTimeParm.ToString("yyyy-MM-dd HH:mm:ss:ffff");
            var sqlDateTime = DateTime.Parse(dateTimeString);
            return sqlDateTime;
        }

        public static string ToPersianDateStringWithoutSeparator(this DateTime dateTimeParam)
        {
            var jalaliCalendar = new JalaliCalendar();
            int year = jalaliCalendar.GetYear(dateTimeParam);
            int month = jalaliCalendar.GetMonth(dateTimeParam);
            int day = jalaliCalendar.GetDayOfMonth(dateTimeParam);
            return $"{year}{month.ToString().PadLeft(2, '0')}{day.ToString().PadLeft(2, '0')}";
        }

        public static DateTime ToMiladiDate(this string persianDateStringParam)
        {
            // Convert to Miladi
            DateTime dt = DateTime.Parse(persianDateStringParam, new CultureInfo("fa-IR"));
            // Get Utc Date
            var dt_utc = dt.ToUniversalTime();


            string[] stringPersianDateArray = persianDateStringParam.Split('/');
            if (stringPersianDateArray.Length != 3)
                return DateTime.MinValue;
            if (stringPersianDateArray[0].Length == 2)
                persianDateStringParam = "13" + persianDateStringParam;

            DateTime resultDateTime = persianDateStringParam.Substring(0, 10).ExtractMiladiDate();
            return resultDateTime;
        }

        public static DateTime ToGeorgianDate(this string persianDateStringParam)
        {
            var dateParts = persianDateStringParam.Split(new[] { '/' });
            JalaliCalendar persianCalendar = new JalaliCalendar();
            DateTime resultDateTime = new DateTime();
            if (dateParts[0].Length == 4)
            {
                resultDateTime = persianCalendar.ToDateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]), DateTime.Now.Hour, DateTime.Now.Minute, 0, 0);
            }
            else
            {
                resultDateTime = persianCalendar.ToDateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[0]), DateTime.Now.Hour, DateTime.Now.Minute, 0, 0);
            }
            return resultDateTime;
        }

        public static DateTime ToGeorgianDate_old(this string persianDateStringParam)
        {
            DateTime dt = DateTime.Parse(persianDateStringParam, new CultureInfo("fa-IR"));
            return dt.ToUniversalTime();
        }

        public static DateTime ExtractMiladiDate(this string persianDateStringParam)
        {
            string[] stringPersianDateArray = persianDateStringParam.Split('/');
            if (stringPersianDateArray.Length != 3)
                return DateTime.MinValue;
            if (stringPersianDateArray[0].Length == 2)
                stringPersianDateArray[0] = "13" + stringPersianDateArray[0];
            JalaliCalendar calender = new JalaliCalendar();
            return calender.ToDateTime(Convert.ToInt32(stringPersianDateArray[0]), Convert.ToInt32(stringPersianDateArray[1]), Convert.ToInt32(stringPersianDateArray[2]), 0, 0, 0, 0);
        }

        public static string ToPersianDateString(this DateTime georgianDate)
        {
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();

            string year = persianCalendar.GetYear(georgianDate).ToString();
            string month = persianCalendar.GetMonth(georgianDate).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(georgianDate).ToString().PadLeft(2, '0');
            string persianDateString = string.Format("{0}/{1}/{2}", year, month, day);
            return persianDateString;
        }

    }
}
