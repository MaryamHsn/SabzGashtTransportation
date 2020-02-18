using System;
using System.Globalization; 
using Sabz.ServiceLayer.Exception;

namespace Sabz.ServiceLayer.Utils
{
    [Serializable]
    public class JalaliCalendar : System.Globalization.HijriCalendar
    {
        private readonly double GYearOff;
        private readonly double Solar;

        private long[,] JDayTab;
        private long[,] GDayTab;

        public JalaliCalendar()
        {
            GYearOff = 226894;
            Solar = 365.25;

            JDayTab = new long[2, 13];
            GDayTab = new long[2, 13];

            JDayTab[0, 0] = 0; JDayTab[1, 0] = 0;
            JDayTab[0, 1] = 31; JDayTab[1, 1] = 31;
            JDayTab[0, 2] = 31; JDayTab[1, 2] = 31;
            JDayTab[0, 3] = 31; JDayTab[1, 3] = 31;
            JDayTab[0, 4] = 31; JDayTab[1, 4] = 31;
            JDayTab[0, 5] = 31; JDayTab[1, 5] = 31;
            JDayTab[0, 6] = 31; JDayTab[1, 6] = 31;
            JDayTab[0, 7] = 30; JDayTab[1, 7] = 30;
            JDayTab[0, 8] = 30; JDayTab[1, 8] = 30;
            JDayTab[0, 9] = 30; JDayTab[1, 9] = 30;
            JDayTab[0, 10] = 30; JDayTab[1, 10] = 30;
            JDayTab[0, 11] = 30; JDayTab[1, 11] = 30;
            JDayTab[0, 12] = 29; JDayTab[1, 12] = 30;

            GDayTab[0, 0] = 0; GDayTab[1, 0] = 0;
            GDayTab[0, 1] = 31; GDayTab[1, 1] = 31;
            GDayTab[0, 2] = 28; GDayTab[1, 2] = 29;
            GDayTab[0, 3] = 31; GDayTab[1, 3] = 31;
            GDayTab[0, 4] = 30; GDayTab[1, 4] = 30;
            GDayTab[0, 5] = 31; GDayTab[1, 5] = 31;
            GDayTab[0, 6] = 30; GDayTab[1, 6] = 30;
            GDayTab[0, 7] = 31; GDayTab[1, 7] = 31;
            GDayTab[0, 8] = 31; GDayTab[1, 8] = 31;
            GDayTab[0, 9] = 30; GDayTab[1, 9] = 30;
            GDayTab[0, 10] = 31; GDayTab[1, 10] = 31;
            GDayTab[0, 11] = 30; GDayTab[1, 11] = 30;
            GDayTab[0, 12] = 31; GDayTab[1, 12] = 31;
        }

        public override DateTime AddMonths(DateTime time, int months)
        {
            DateTime dt = new DateTime(time.Year, time.Month, time.Day,
                                    time.Hour, time.Minute, time.Second, time.Millisecond);

            var addMonths = dt.AddMonths(months);
            return addMonths;
        }

        public override DateTime AddYears(DateTime time, int years)
        {
            DateTime dt = new DateTime(time.Year, time.Month, time.Day,
                time.Hour, time.Minute, time.Second, time.Millisecond);

            var addYears = dt.AddYears(years);
            return addYears;
        }

        public override int[] Eras
        {
            get
            {
                int[] tempEra = new int[1];
                tempEra[0] = 1;

                return tempEra;
            }
        }

        public override int GetDayOfMonth(DateTime time)
        {
            long year = time.Year;
            long month = time.Month;
            long day = time.Day;

            long JYear, JMonth, JDay;
            JalaliDate(year, month, day, out JYear, out JMonth, out JDay);
            return (int)JDay;
        }

        public override DayOfWeek GetDayOfWeek(DateTime time)
        {
            return time.DayOfWeek;
        }

        public override int GetDayOfYear(DateTime time)
        {
            return time.DayOfYear;
        }

        public override int GetDaysInMonth(int year, int month)
        {
            long leap;

            if (IsLeapYear(year))
                leap = 1;
            else
                leap = 0;

            return (int)JDayTab[leap, month];
        }

        public override int GetDaysInMonth(int year, int month, int era)
        {
            return GetDaysInMonth(year, month);
        }

        public override int GetDaysInYear(int year)
        {
            if (IsLeapYear(year))
                return 366;
            else
                return 365;
        }

        public override int GetDaysInYear(int year, int era)
        {
            return GetDaysInYear(year);
        }

        public override int GetEra(DateTime time)
        {
            return Eras[0];
        }

        public override int GetMonth(DateTime time)
        {
            long year = time.Year;
            long month = time.Month;
            long day = time.Day;

            long JYear, JMonth, JDay;
            JalaliDate(year, month, day, out JYear, out JMonth, out JDay);
            return (int)JMonth;
        }

        public override int GetMonthsInYear(int year)
        {
            return 12;
        }

        public override int GetMonthsInYear(int year, int era)
        {
            return GetMonthsInYear(year);
        }

        public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
        {
            return base.GetWeekOfYear(time, rule, firstDayOfWeek);
        }

        public override int GetYear(DateTime time)
        {
            long year = time.Year;
            long month = time.Month;
            long day = time.Day;

            long JYear, JMonth, JDay;
            JalaliDate(year, month, day, out JYear, out JMonth, out JDay);
            return (int)JYear;
        }

        public override bool IsLeapDay(int year, int month, int day)
        {
            if (IsLeapYear(year) && IsLeapMonth(year, month) && (day == 30))
                return true;
            else
                return false;
        }

        public override bool IsLeapDay(int year, int month, int day, int era)
        {
            return IsLeapDay(year, month, day);
        }

        public override bool IsLeapMonth(int year, int month)
        {
            if (IsLeapYear(year) && (month == 12))
                return true;
            else
                return false;
        }

        public override bool IsLeapMonth(int year, int month, int era)
        {
            return IsLeapMonth(year, month);
        }

        public override bool IsLeapYear(int year)
        {
            int temp;

            temp = year % 33;
            if ((temp == 1) || (temp == 5) || (temp == 9) ||
                (temp == 13) || (temp == 17) || (temp == 22) ||
                (temp == 26) || (temp == 30))
                return true;
            else
                return false;
        }

        public override bool IsLeapYear(int year, int era)
        {
            return IsLeapYear(year);
        }

        public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            long GYear, GMonth, GDay;
            ValidateDate(year, month, day);
            GregorianDate(year, month, day, out GYear, out GMonth, out GDay);
            return new DateTime((int)GYear, (int)GMonth, (int)GDay, hour, minute, second, millisecond);
        }

        public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
        {
            return ToDateTime(year, month, day, hour, minute, second, millisecond);
        }

        public override int ToFourDigitYear(int year)
        {
            return base.ToFourDigitYear(year);
        }

        public override int TwoDigitYearMax
        {
            get
            {
                return base.TwoDigitYearMax;
            }
            set
            {
                base.TwoDigitYearMax = value;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }


        #region Private and Added section
        private long GDayOfYear(long lngYear, long lngMonth, long lngDay)
        {
            long lngLeap;
            long i;

            lngLeap = GLeap(lngYear);
            for (i = 1; i < lngMonth; i++)
                lngDay = lngDay + GDayTab[lngLeap, i];

            return lngDay;
        }

        private long GLeap(long lngYear)
        {
            if ((lngYear % 4 == 0) || ((lngYear % 100 == 0) && (lngYear % 400 == 0)))
                return 1;
            else
                return 0;
        }

        private void GMonthDay(long lngGYear, long lngGDayOfYear, out long lngMonth, out long lngDay)
        {
            long i, lngLeap;

            lngLeap = GLeap(lngGYear);
            i = 1;
            while (lngGDayOfYear > GDayTab[lngLeap, i])
            {
                lngGDayOfYear = lngGDayOfYear - GDayTab[lngLeap, i];
                i++;
            }

            lngMonth = i;
            lngDay = lngGDayOfYear;
        }

        private long GregDays(long lngGYear, long lngGMonth, long lngGDay)
        {
            long lngDiv4;
            long lngDiv100;
            long lngDiv400;
            long lngTotalDays;
            long lngTmp;

            lngDiv4 = Convert.ToInt32(Math.Floor((double)(lngGYear - 1) / 4));
            lngDiv100 = Convert.ToInt32(Math.Floor((double)(lngGYear - 1) / 100));
            lngDiv400 = Convert.ToInt32(Math.Floor((double)(lngGYear - 1) / 400));
            lngTmp = GDayOfYear(lngGYear, lngGMonth, lngGDay);
            lngTotalDays = (lngGYear - 1) * 365 + lngTmp + lngDiv4 - lngDiv100 + lngDiv400;
            return lngTotalDays;
        }

        private void GregorianDate(long lngJYear, long lngJMonth, long lngJDay, out long lngGYear, out long lngGMonth, out long lngGDay)
        {
            long lngTotalDays;

            lngTotalDays = JalaliDays(lngJYear, lngJMonth, lngJDay);
            GregorianYMD(lngTotalDays, out lngGYear, out lngGMonth, out lngGDay);
        }

        private void GregorianYMD(long lngTotalDays, out long lngGYear, out long lngGMonth, out long lngGDay)
        {
            long lngDiv4, lngDiv100, lngDiv400;
            long lngGDays;

            lngTotalDays = lngTotalDays + Convert.ToInt32(GYearOff);
            lngGYear = Convert.ToInt32(Math.Floor(lngTotalDays / (Solar - 0.25 / 33)));
            lngDiv4 = Convert.ToInt32(Math.Floor((double)lngGYear / 4));
            lngDiv100 = Convert.ToInt32(Math.Floor((double)lngGYear / 100));
            lngDiv400 = Convert.ToInt32(Math.Floor((double)lngGYear / 400));

            lngGDays = lngTotalDays - (365 * lngGYear) - (lngDiv4 - lngDiv100 + lngDiv400);
            lngGYear = lngGYear + 1;

            if (lngGDays == 0)
            {
                lngGYear = lngGYear - 1;
                if (GLeap(lngGYear) != 0)
                    lngGDays = 366;
                else
                    lngGDays = 365;
            }
            else if ((lngGDays == 366) && (GLeap(lngGYear) == 0))
            {
                lngGDays = 1;
                lngGYear++;
            }

            GMonthDay(lngGYear, lngGDays, out lngGMonth, out lngGDay);
        }

        private void JalaliDate(long lngGYear, long lngGMonth, long lngGDay, out long lngJYear, out long lngJMonth, out long lngJDay)
        {
            long lngTotalDays;

            lngTotalDays = GregDays(lngGYear, lngGMonth, lngGDay);
            JalaliYMD(lngTotalDays, out lngJYear, out lngJMonth, out lngJDay);
        }

        private long JalaliDays(long lngJYear, long lngJMonth, long lngJDay)
        {
            long lngTotalDays;
            long lngLeap, lngTmp;

            lngLeap = JLeapYears(lngJYear - 1);
            lngTmp = JDayOfYear(lngJYear, lngJMonth, lngJDay);
            lngTotalDays = (lngJYear - 1) * 365 + lngLeap + lngTmp;

            return lngTotalDays;
        }

        private void JalaliYMD(long lngTotalDays, out long lngJYear, out long lngJMonth, out long lngJDay)
        {
            long lngJDays;
            long lngLeap;

            lngTotalDays = lngTotalDays - Convert.ToInt32(GYearOff);
            lngJYear = Convert.ToInt32(Math.Floor(lngTotalDays / (Solar - 0.25 / 33)));
            lngLeap = JLeapYears(lngJYear);

            lngJDays = lngTotalDays - (365 * lngJYear + lngLeap);
            lngJYear = lngJYear + 1;

            if (lngJDays == 0)
            {
                lngJYear = lngJYear - 1;
                if (JLeap(lngJYear) != 0)
                    lngJDays = 366;
                else
                    lngJDays = 365;
            }
            else if ((lngJDays == 366) && (JLeap(lngJYear) == 1))
            {
                lngJDays = 1;
                lngJYear = lngJYear + 1;
            }

            JMonthDay(lngJYear, lngJDays, out lngJMonth, out lngJDay);
        }

        private long JDayOfYear(long lngYear, long lngMonth, long lngDay)
        {
            long lngLeap;
            long i;

            lngLeap = JLeap(lngYear);
            for (i = 1; i < lngMonth; i++)
                lngDay = lngDay + JDayTab[lngLeap, i];

            return lngDay;
        }

        private long JLeap(long lngYear)
        {
            long lngTmp;

            lngTmp = lngYear % 33;
            if ((lngTmp == 1) || (lngTmp == 5) || (lngTmp == 9) || (lngTmp == 13) || (lngTmp == 17) || (lngTmp == 22) || (lngTmp == 26) || (lngTmp == 30))
                return 1;
            else
                return 0;
        }

        private long JLeapYears(long lngJYear)
        {
            long lngLeap, lngCurrentCycle, lngDiv33;
            long i;

            lngDiv33 = Convert.ToInt32(Math.Floor((double)lngJYear / 33));
            lngCurrentCycle = lngJYear - (lngDiv33 * 33);
            lngLeap = lngDiv33 * 8;

            if (lngCurrentCycle > 0)
            {
                i = 1;
                while (i <= 18 && i <= lngCurrentCycle)
                {
                    lngLeap = lngLeap + 1;
                    i = i + 4;
                }
            }

            if (lngCurrentCycle > 21)
            {
                i = 22;
                while (i <= 30 && i <= lngCurrentCycle)
                {
                    lngLeap = lngLeap + 1;
                    i = i + 4;
                }
            }

            return lngLeap;
        }

        private void JMonthDay(long lngJYear, long lngJDayOfYear, out long lngMonth, out long lngDay)
        {
            long lngLeap;
            long i;

            lngLeap = JLeap(lngJYear);
            i = 1;
            while (lngJDayOfYear > JDayTab[lngLeap, i])
            {
                lngJDayOfYear = lngJDayOfYear - JDayTab[lngLeap, i];
                i = i + 1;
            }

            lngMonth = i;
            lngDay = lngJDayOfYear;
        }

        public void ValidateDate(int year, int month, int day)
        {
            int leap = IsLeapYear(year) ? 1 : 0;

            if (day > JDayTab[leap, month])
                throw new DateTimeFormatException();
        }
        #endregion


    }
}
