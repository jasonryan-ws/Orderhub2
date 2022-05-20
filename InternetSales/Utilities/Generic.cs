using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Generic
    {
        public static string GetEnumDescription(Enum enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = null;

                foreach (var att in attribArray)
                {
                    if (att is DescriptionAttribute)
                        attrib = att as DescriptionAttribute;
                }

                if (attrib != null)
                    return attrib.Description;

                return enumObj.ToString();
            }
        }
        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
            // or return default(T);
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        // Orignal Source https://www.codegrepper.com/code-examples/csharp/C%23+today%2C+yesterday%2C+last+week%2C+last+month
        public static string ToTimeSinceString(this DateTime? value)
        {
            if (value != null)
            {
                const int SECOND = 1;
                const int MINUTE = 60 * SECOND;
                const int HOUR = 60 * MINUTE;
                const int DAY = 24 * HOUR;
                const int MONTH = 30 * DAY;

                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - ((DateTime)value).Ticks);
                double seconds = ts.TotalSeconds;

                // Less than one minute
                if (seconds < 1 * MINUTE)
                    return ts.Seconds == 1 ? "a second ago" : ts.Seconds <= 0 ? "just now" : ts.Seconds + " seconds ago";

                if (seconds < 60 * MINUTE)
                    return ts.Minutes <= 1 ? "a minute ago" : ts.Minutes + " minutes ago";

                if (seconds < 120 * MINUTE)
                    return "an hour ago";

                if (seconds < 24 * HOUR)
                    return ts.Hours + " hours ago";

                if (seconds < 48 * HOUR)
                    return "yesterday";

                if (seconds < 30 * DAY)
                    return ts.Days <= 1 ? "a day ago" : ts.Days + " days ago";

                if (seconds < 12 * MONTH)
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "a month ago" : months + " months ago";
                }

                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "a year ago" : years + " years ago";
            }
            return "---";
        }
        public static string GetRandomColorHex()
        {
            var random = new Random();
            return String.Format("#{0:X6}", random.Next(0x1000000));
        }

        public static int GetRandomColorInt()
        {
            return int.Parse(GetRandomColorHex().Replace("#", string.Empty), System.Globalization.NumberStyles.HexNumber);
        }

        public static DataTable GetDataTableFromCSV(string path, bool isFirstRowHeader = true)
        {
            try
            {
                var hasHeader = isFirstRowHeader ? "Yes" : "No";
                var directory = Path.GetDirectoryName(path);
                var fileName = Path.GetFileName(path);
                var sql = $@"SELECT * FROM [{fileName}]";

                using (var connection = new OleDbConnection($@"
                        Provider=Microsoft.Jet.OLEDB.4.0;
                        Data Source={directory};
                        Extended Properties=Text;
                        HDR={hasHeader}"))
                {
                    var command = new OleDbCommand(sql, connection);
                    var adapter = new OleDbDataAdapter(command);
                    var dataTable = new DataTable { Locale = CultureInfo.CurrentCulture };
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<string> SplitTextToWords(string text)
        {
            var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = (text.Split().Select(x => x.Trim(punctuation))).ToList();

            return words;
        }

        public static string Abbreviate(string text, bool separatedByPeriod = false)
        {
            char[] tempArray = new char[text.Length];
            string abbr = "";
            int loop = 0;


            tempArray = text.ToCharArray();

            abbr += (char)((int)tempArray[0] ^ 32);
            if (separatedByPeriod)
                abbr += '.';

            for (loop = 0; loop < text.Length - 1; loop++)
            {
                if (tempArray[loop] == ' ' || tempArray[loop] == '\t' || tempArray[loop] == '\n')
                {

                    abbr += (char)((int)tempArray[loop + 1] ^ 32);
                    if (separatedByPeriod)
                        abbr += '.';
                }
            }

            return abbr;
        }
        

        /// <summary>
        /// Converts generic list into DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable GenericListToDataTable<T>(List<T> items)
        {
            try
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
