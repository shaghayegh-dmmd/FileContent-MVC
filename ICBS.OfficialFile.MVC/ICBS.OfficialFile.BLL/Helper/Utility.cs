using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICBS.OfficialFile.BLL.Helper
{
    public static class Utility
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

        public static string PersianNumbersToEnglish(this string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return
                str.Replace("۰", "0")
                    .Replace("۱", "1")
                    .Replace("۲", "2")
                    .Replace("۳", "3")
                    .Replace("۴", "4")
                    .Replace("۵", "5")
                    .Replace("۶", "6")
                    .Replace("۷", "7")
                    .Replace("۸", "8")
                    .Replace("۹", "9");
        }
        public static string EnglishNumbersToPersian(this string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return
                str.Replace("0", "۰")
                    .Replace("1", "۱")
                    .Replace("2", "۲")
                    .Replace("3", "۳")
                    .Replace("4", "۴")
                    .Replace("5", "۵")
                    .Replace("6", "۶")
                    .Replace("7", "۷")
                    .Replace("8", "۸")
                    .Replace("9", "۹");
        }
    }
}
