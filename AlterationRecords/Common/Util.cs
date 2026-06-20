using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlterationRecords.Common
{
    public class Util
    {
        public static decimal CMoney(string money, decimal defaultValue)
        {
            string numberPart = "";
            money = money.Trim();
            if (money.Length > 0)
            {
                bool foundDollarSign = false;
                for (int i = 0; i < money.Length; i++)
                {
                    string ch = money.Substring(i, 1);
                    switch (ch)
                    {
                        case "$":
                            if (foundDollarSign)
                            {
                                foundDollarSign = false;
                            }
                            break;
                        default:
                            numberPart += ch;
                            break;
                    }
                }
            }
            return CDecimal(numberPart, defaultValue);
        }

        public static decimal CDecimal(object obj, decimal defaultValue)
        {
            decimal returnValue = defaultValue;
            try
            {
                returnValue = Convert.ToDecimal(obj);
            }
            catch { }
            return returnValue;
        }

        public static Int64 CInt64(object obj)
        {
            return CInt64(obj, int.MinValue);
        }

        public static Int64 CInt64(object obj, Int64 defaultValue)
        {
            Int64 returnValue = defaultValue;
            try
            {
                returnValue = Convert.ToInt64(obj);
            }
            catch { }
            return returnValue;
        }

        public static bool CBool(object obj)
        {
            return CBool(obj, false);
        }

        public static bool CBool(object obj, bool defaultValue)
        {
            bool returnValue = defaultValue;
            try
            {
                returnValue = Convert.ToBoolean(obj);
            }
            catch { }
            return returnValue;
        }

        public static double CDouble(object obj)
        {
            return CDouble(obj, double.MinValue);
        }

        public static double CDouble(object obj, double defaultValue)
        {
            double returnValue = defaultValue;
            try
            {
                returnValue = Convert.ToDouble(obj);
            }
            catch { }

            return returnValue;
        }

        public static DateTime CDate(object obj)
        {
            return CDate(obj, DateTime.MinValue);
        }

        public static DateTime CDate(object obj, DateTime defaultValue)
        {
            DateTime returnValue = defaultValue;
            if (obj != null)
            {
                try
                {
                    returnValue = Convert.ToDateTime(obj);
                }
                catch { }
            }
            return returnValue;
        }

        public static string CString(object obj)
        {
            return CString(obj, "");
        }

        public static string CString(object obj, bool useDefaultValueOnBlankString)
        {
            return CString(obj, "", useDefaultValueOnBlankString);
        }

        public static string CString(
            object obj,
            string defaultValue,
            bool useDefaultValueOnBlankString)
        {
            string returnValue = defaultValue;
            if (obj != null)
            {
                try
                {
                    returnValue = Convert.ToString(obj);
                    if (useDefaultValueOnBlankString)
                    {
                        if (returnValue.Length == 0)
                        {
                            returnValue = defaultValue;
                        }
                    }
                }
                catch { }
            }
            return returnValue;
        }

        public static string CString(object obj, string defaultValue)
        {
            return CString(obj, defaultValue, true);
        }
    }
}