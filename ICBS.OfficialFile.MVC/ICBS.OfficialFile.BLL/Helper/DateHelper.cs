using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace ICBS.OfficialFile.BLL.Helper
{
    public static class DateHelper
    {
            public static bool IsValidJalaliDate(string dateTime)
            {
                //d:\ICBS\LawLegal\ICBS.LawLegalSystem.MVC\packages\MD.PersianDateTime.3.8.1\lib\MD.PersianDateTime.dll
                try
                {
                    var a = GetGregorianFromSlashStringJalali(dateTime);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public static bool IsValidJalaliDateAndNotEmpty(string dateTime)
            {
                try
                {
                    //
                }
                catch
                {
                    return false;
                }

                return true;
            }

            public static DateTime? ToGeoDateTime(this string persianDate)
            {
                try
                {
                    return GetGregorianFromSlashStringJalali(persianDate);
                }
                catch
                {
                    return null;
                }

            }

            public static string ToPerSlashStringDate(this DateTime geoDate)
            {
                return GetJalaliFromDateTimeGregorian(geoDate);
            }
            public static string ToPerSlashStringDate(this DateTime? geoDate)
            {
                return GetJalaliFromDateTimeGregorian(geoDate);
            }

            public static int? GetIntDateFromSlashString(string date)
            {
                int? result;
                try
                {
                    result = int.Parse(date.Replace("/", string.Empty));
                }
                catch
                {
                    result = null;
                }
                return result;
            }
            public static string GetSlashStringDateFromInt(int date)
            {
                try
                {
                    var result = date.ToString();
                    if (result.Count() != 8) return "";
                    result = result.Insert(4, "/");
                    result = result.Insert(7, "/");
                    return result;
                }
                catch
                {
                    return string.Empty;
                }
            }
            public static string GetSlashStringDateFromInt(int? date)
            {
                return date == null ? "" : GetSlashStringDateFromInt((int)date);
            }

            public static System.DateTime GetGregorianFromSlashStringJalali(string dateTime)
            {
                var cal = new PersianCalendar();
                var dateDelim = dateTime.Trim().Split('/');

                try
                {
                    if (dateDelim[2].Length != 4 && dateDelim[0].Length != 4) throw new Exception("Wrong Date !");
                    if (dateDelim[2].Length == 4)
                    {
                        if (int.Parse(dateDelim[2]) < 1300) throw new Exception("Wrong Date !");
                        return cal.ToDateTime(Convert.ToInt32(dateDelim[2]), Convert.ToInt32(dateDelim[1]), Convert.ToInt32(dateDelim[0]), 0, 0, 0, 0);
                    }
                    else
                    {
                        if (int.Parse(dateDelim[0]) < 1300) throw new Exception("Wrong Date !");
                        return cal.ToDateTime(Convert.ToInt32(dateDelim[0]), Convert.ToInt32(dateDelim[1]), Convert.ToInt32(dateDelim[2]), 0, 0, 0, 0);
                    }
                }
                catch
                {
                    throw new Exception(string.Format("Error in convert date: {0} , {1}", dateTime, dateDelim));
                }
            }
            public static bool TryGetGregorianFromSlashStringJalali(string dateTime)
            {
                try
                {
                    GetGregorianFromSlashStringJalali(dateTime);
                }
                catch
                {
                    return false;
                }

                return true;
            }
            public static string GetJalaliFromDateTimeGregorian(System.DateTime? gerigorianDate)
            {
                return gerigorianDate == null ? "" : GetJalaliFromDateTimeGregorian((System.DateTime)gerigorianDate);
            }

            public static string GetJalaliFromDateTimeGregorian(System.DateTime gerigorianDate)
            {
                var cal = new PersianCalendar();
                try
                {
                    var month = cal.GetMonth(gerigorianDate).ToString().PadLeft(2, '0');

                    var day = cal.GetDayOfMonth(gerigorianDate).ToString().PadLeft(2, '0');

                    return cal.GetYear(gerigorianDate) + "/" + month + "/" + day;
                }
                catch
                {
                    return "";
                }
            }

            public static System.DateTime GetGregorianDatePlusWeeksFromDateTimeGregorian(System.DateTime gerigorianDateFirst, int weekCount)
            {

                var days = (weekCount * 7) - 1;
                if (days < 0)
                {
                    days = 0;
                }
                return gerigorianDateFirst.AddDays(days);
            }
            public static bool IsFirstDayOfWeekFromDateTimeGregorian(System.DateTime gerigorianDate)
            {
                return GetEnglishNameOfDayFromDateTimeGregorian(gerigorianDate).ToLower() == "saturday";
            }
            public static string GetEnglishNameOfDayFromDateTimeGregorian(System.DateTime gerigorianDate)
            {
                return gerigorianDate.ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
            }
            //static public string GetPersianNameOfDayFromDateTimeGregorian(System.DateTime gerigorianDate)
            //{
            //    var enName = GetEnglishNameOfDayFromDateTimeGregorian(gerigorianDate);
            //    switch (enName.ToLower())
            //    {
            //        case "saturday":
            //            return "شنبه";
            //        case "sunday":
            //            return "یکشنبه";
            //        case "monday":
            //            return "دو شنبه";
            //        case "tuesday":
            //            return "سه شنبه";
            //        case "wednesday":
            //            return "چهار شنبه";
            //        case "thursday":
            //            return "پنج شنبه";
            //        case "friday":
            //            return "آدینه";
            //        default:
            //            return "";
            //    }
            //}
            //static public int GetFrom0NumberOfDayFromDateTimeGregorianPersianSort(System.DateTime gerigorianDate)
            //{
            //    var enName = GetEnglishNameOfDayFromDateTimeGregorian(gerigorianDate);
            //    switch (enName.ToLower())
            //    {
            //        case "saturday":
            //            return 0;
            //        case "sunday":
            //            return 1;
            //        case "monday":
            //            return 2;
            //        case "tuesday":
            //            return 3;
            //        case "wednesday":
            //            return 4;
            //        case "thursday":
            //            return 5;
            //        case "friday":
            //            return 6;
            //        default:
            //            return -1;
            //    }
            //}
            //static public int GetFrom1NumberOfDayFromDateTimeGregorianPersianSort(System.DateTime gerigorianDate)
            //{
            //    var enName = GetEnglishNameOfDayFromDateTimeGregorian(gerigorianDate);
            //    switch (enName.ToLower())
            //    {
            //        case "saturday":
            //            return 1;
            //        case "sunday":
            //            return 2;
            //        case "monday":
            //            return 3;
            //        case "tuesday":
            //            return 4;
            //        case "wednesday":
            //            return 5;
            //        case "thursday":
            //            return 6;
            //        case "friday":
            //            return 7;
            //        default:
            //            return -1;
            //    }
            //}
            //static public List<DayModel> GetListOfAllDays()
            //{
            //    return new List<DayModel>
            //    {
            //        new DayModel
            //        {
            //            Id = 1,
            //            Row = 0,
            //            EnglishName = "saturday",
            //            PersianName = "شنبه"
            //        },
            //        new DayModel
            //        {
            //            Id = 2,
            //            Row = 1,
            //            EnglishName = "sunday",
            //            PersianName = "یکشنبه"
            //        },
            //        new DayModel
            //        {
            //            Id = 3,
            //            Row = 2,
            //            EnglishName = "monday",
            //            PersianName = "دو شنبه"
            //        },
            //        new DayModel
            //        {
            //            Id = 4,
            //            Row = 3,
            //            EnglishName = "tuesday",
            //            PersianName = "سه شنبه"
            //        },
            //        new DayModel
            //        {
            //            Id = 5,
            //            Row = 4,
            //            EnglishName = "wednesday",
            //            PersianName = "چهار شنبه"
            //        },
            //        new DayModel
            //        {
            //            Id = 6,
            //            Row = 5,
            //            EnglishName = "thursday",
            //            PersianName = "پنج شنبه"
            //        },
            //        new DayModel
            //        {
            //            Id = 7,
            //            Row = 6,
            //            EnglishName = "friday",
            //            PersianName = "آدینه"
            //        }
            //    };
            //}

            public static int? GetIntNowJalaliDate()
            {
                return GetIntDateFromSlashString(GetJalaliFromDateTimeGregorian(System.DateTime.Now));
            }
            public static string GetSlashStringNowJalaliDate()
            {
                return GetJalaliFromDateTimeGregorian(System.DateTime.Now);
            }


        }
    }
