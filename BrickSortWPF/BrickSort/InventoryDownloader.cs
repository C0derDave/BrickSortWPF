using BrickSortWPF.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrickSortWPF.BrickSort
{
    public class InventoryDownloader
    {
        private string _apiKey;
        private string ApiKey
        {
            get
            {
                if (_apiKey == null)
                    _apiKey = ReadAPIKey();

                return _apiKey;
            }
        }

        private string ReadAPIKey()
        {
            StreamReader streamReader = new StreamReader("key.config");
            return streamReader.ReadLine();
        }

        public Inventory DownloadInventoryFromId(string setId)
        {
            Inventory inventory = new Inventory();
            try
            {
                string url = "https://rebrickable.com/api/v3/lego/sets/" + setId + "/parts/";
                var jsons = DownloadJSON(url);
                inventory.Initialize(jsons);
                return inventory;
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private List<JObject> DownloadJSON(string url)
        {
            var jsons = new List<JObject>();

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.Headers["Authorization"] = "key " + ApiKey;
            webRequest.Accept = "application/json";
            WebResponse webResponse = webRequest.GetResponse();
            StreamReader reader = new StreamReader(webResponse.GetResponseStream());

            string line = "";
            string content = "";
            while (line != null)
            {
                content += line;
                line = reader.ReadLine();
            }

            JObject obj = JObject.Parse(content);
            jsons.Add(obj);

            JToken next = obj["next"];
            if (next.Type != JTokenType.Null)
            {
                var downloadedJsons = DownloadJSON(next.ToString());
                jsons.AddRange(downloadedJsons);
            }

            return jsons;
        }
    }
}
