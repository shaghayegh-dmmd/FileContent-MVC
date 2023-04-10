using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICBS.OfficialFile.WCF.Helper
{
    public static class SubArrayHelper
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }
    }
}