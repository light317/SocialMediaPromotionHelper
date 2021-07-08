using Newtonsoft.Json.Linq;
using Services.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace Services
{
    public class DataProcessor : IDataProcessor
    {
        public JObject GetJsonStringFromFileAsync(string filePath)
        {
            var fileData = File.ReadAllText(filePath);

            JObject data = JObject.Parse(fileData);

            return data;
        }
    }
}