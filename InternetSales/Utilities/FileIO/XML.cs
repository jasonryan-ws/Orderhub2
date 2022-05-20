using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Utilities.FileIO
{
    public static class XML
    {
        public static string Read(string path, string element, string attribute)
        {
            try
            {
                if (File.Exists(path))
                {
                    var xml = XDocument.Load(path);

                    var query = (from c in xml.Root.Descendants(element)
                                 select c.Element(attribute).Value).FirstOrDefault();

                    return query;
                }
                else
                    throw new Exception("File doesn't exists");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Write(string path, string element, string attribute, string value)
        {
            try
            {
                if (File.Exists(path))
                {
                    var xml = XDocument.Load(path);
                    foreach (var d in xml.Descendants(element))
                    {
                        d.SetAttributeValue(attribute, value);
                    }
                }
                else
                    throw new Exception("File doesn't exists");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
