using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipWorks.Models
{
    public class Item
    {
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageURL { get; set; }
        public string ThumbnailURL { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal SubTotal { get { return Price * Quantity; } }

        public Dictionary<string, int> Locations
        {
            get
            {
                var commaSplit = Location.Split(',');
                var locations = new Dictionary<string, int>();
                foreach (var c in commaSplit)
                {
                    try
                    {
                        var parenthesisSplit = c.Split('(');
                        var item = new string[2];
                        var name = parenthesisSplit[0].Replace(" ", string.Empty);
                        var quantity = int.Parse(parenthesisSplit[1].Replace(")", string.Empty));
                        locations.Add(name, quantity);
                    }
                    catch { }
                }
                return locations;
            }
        }
    }
}
