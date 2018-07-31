using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickSortWPF.Model
{
    public class Part
    {
        public string Name { get; set; }

        public string ID { get; set; }

        public string Color { get; set; }

        public string ImageURL { get; set; }

        public int Quantity { get; set; }

        public int RequiredQuantity { get; set; }

        public Part(string name, string id, string color, int requiredQuantity, string imageURL)
        {
            Name = name;
            ID = id;
            Color = color;
            ImageURL = imageURL;
            RequiredQuantity = requiredQuantity;
            Quantity = 0;
        }

        public string GetUniqueID()
        {
            return ID + Color;
        }
    }
}
