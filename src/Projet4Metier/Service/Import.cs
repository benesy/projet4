using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet4Metier
{
    public static class Import
    {

        public static string[] ReadFile(string filePath)
        {
            return System.IO.File.ReadAllLines(@filePath);
        }

        public static List<MStatement> ConvertFile(string[] file)
        {
            string[] data;
            List<MStatement> mStatementslist = new List<MStatement>();
            for (int i = 0 ; i<file.Length ; i++)
            {
                MStatement entry = new MStatement();
                data  = file[i].Split(' ');
                entry.DateTime = GetDateTime(data[1], data[2]);
                entry.Temperature = GetTemp(data[3]);
                entry.Humidity = GetHumidity(data[4]);
                mStatementslist.Add(entry);
            }
            return mStatementslist;
        }

        public static DateTime GetDateTime(string date, string time) 
        {
           return DateTime.ParseExact(string.Format("{0} {1}", date, time), "yyyy-MM-dd HH:mm:ss", null);
        }
          
        public static double GetTemp(string temp)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            return Convert.ToDouble(temp, provider);

        }

        public static double GetHumidity(string humidity)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            return Convert.ToDouble(humidity.Replace("%", " "), provider);
        }

    }
}
