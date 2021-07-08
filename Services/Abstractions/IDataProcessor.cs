using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IDataProcessor
    {
        JObject GetJsonStringFromFileAsync(string filePath);
    }
}