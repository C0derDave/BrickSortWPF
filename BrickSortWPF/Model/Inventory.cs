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

        /// <summary>
        /// Create an Inventory object with a list of Part objects.
        /// </summary>
        /// <param name="setID">The LEGO® set ID to make an inventory for.</param>
        public Inventory(string setID)
        {
            Parts = new List<Part>();
            _apiKey = LoadAPIKey();

            string url = "https://rebrickable.com/api/v3/lego/sets/" + setID + "/parts/";
            ParseJSON(DownloadJSON(url));
        }

        private string LoadAPIKey()
        {
            StreamReader streamReader = new StreamReader("key.config");
            return streamReader.ReadLine();
        }

        private string DownloadJSON(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.Headers["Authorization"] = "key " + _apiKey;
            webRequest.Accept = "application/json";
            Stream stream = webRequest.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            string line = "";
            string content = "";
            while (line != null)
            {
                content += line;
                line = reader.ReadLine();
            }

            return content;
        }

        private void ParseJSON(string json)
        {
            JObject obj = JObject.Parse(json);

            JToken next = obj["next"];
            if(next.Type != JTokenType.Null)
            {
                ParseJSON(DownloadJSON(next.ToString()));
            }

            JArray parts = (JArray)obj["results"];
            foreach(JToken partToken in parts)
            {
                Part part = new Part(partToken["part"]["name"].ToString(), 
                                partToken["id"].ToString() + partToken["color"].ToString(), 
                                (int)partToken["quantity"], 
                                partToken["part"]["part_img_url"].ToString());
                Parts.Add(part);
            }
        }
    }
}
