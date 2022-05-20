using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.FileIO
{
    public static class Text
    {
        public static string Read(string path)
        {

            try
            {
                if (File.Exists(path))
                {
                    using (var sr = File.OpenText(path))
                    {
                        var data = sr.ReadToEnd();
                        sr.Close();
                        return data;
                    }
                }
                else
                {
                    throw new Exception("File doesn't exist.");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Write(string path, string data)
        {
            try
            {
                using (var sw = File.AppendText(path))
                {
                    sw.WriteLine(data);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetValueString(string text, string property, string separator = "=")
        {
            try
            {
                if (!string.IsNullOrEmpty(property))
                {
                    using (var reader = new StringReader(text))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains($"{property}{separator}"))
                                return line.Replace($"{property}{separator}", string.Empty);
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string GetValueTextFile(string path, string property, string separator = "=")
        {
            try
            {
                if (!string.IsNullOrEmpty(property))
                {
                    var text = Read(path);
                    using (var reader = new StringReader(text))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains($"{property}{separator}"))
                                return line.Replace($"{property}{separator}", string.Empty);
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
