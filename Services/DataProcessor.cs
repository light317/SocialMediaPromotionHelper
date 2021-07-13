using Newtonsoft.Json.Linq;
using Services.Abstractions;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Services
{
    public class DataProcessor : IDataProcessor
    {
        public async Task<JObject> GetJsonFromURLAsync(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            WebResponse response = await request.GetResponseAsync();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need
            string textData = new WebClient().DownloadString(url);
            JObject data = JObject.Parse(textData);

            return data;
        }

        public JObject GetJsonStringFromFileAsync(string filePath)
        {
            var fileData = File.ReadAllText(filePath);

            JObject data = JObject.Parse(fileData);

            return data;
        }
    }
}