using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrickSortWPF.Model
{
    public class Inventory
    {
        public List<Part> Parts { get; set; }

        private string _apiKey;

        public Inventory()
        {
            Parts = new List<Part>();
        }

        public void Initialize(List<JObject> jsons)
        {
            ParseJSONIntoPartList(jsons);
            SortByColor();
        }

        private void ParseJSONIntoPartList(List<JObject> jsons)
        {
            foreach (JObject jobj in jsons)
            {
                JArray parts = (JArray) jobj["results"];
                foreach (JToken partToken in parts)
                {
                    Part part = new Part(partToken["part"]["name"].ToString(),
                                    partToken["id"].ToString(),
                                    partToken["color"].ToString(),
                                    (int)partToken["quantity"],
                                    partToken["part"]["part_img_url"].ToString());
                    Parts.Add(part);
                }
            }
        }
        
        public void SortByQuantity()
        {
            Parts = Parts.OrderBy(p => p.RequiredQuantity).ToList();
        }

        public void SortByColor()
        {
            Parts = Parts.OrderBy(p => p.Color).ToList();
        }
    }
}
