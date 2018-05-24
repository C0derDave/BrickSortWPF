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

        /// <summary>
        /// The ID of the part, so, in this case, the actual numerical ID of the part + the part color in order to keep it unique.
        /// </summary>
        public string ID { get; set; }

        public string ImageURL { get; set; }

        public int Quantity { get; set; }

        public int RequiredQuantity { get; set; }

        /// <summary>
        /// Create a Part object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id">The id of the part, so in this case, the numerical ID + the part color to keep it unique.</param>
        /// <param name="requiredQuantity"></param>
        /// <param name="imageURL"></param>
        public Part(string name, string id, int requiredQuantity, string imageURL)
        {
            Name = name;
            ID = id;
            ImageURL = imageURL;
            RequiredQuantity = requiredQuantity;
            Quantity = 0;
        }
    }
}
