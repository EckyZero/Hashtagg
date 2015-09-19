using System;
using System.IO;
using Newtonsoft.Json;
using Shared.Api;
using Foundation;

namespace iOS
{
    public class FileReader : IFileReader
    {
        public FileReader () { }

        public T ReadFile<T> (string fileName, string fileType, JsonSerializerSettings settings = null)
        {
            var filePath = NSBundle.MainBundle.PathForResource(fileName, fileType);
            var data = NSData.FromFile(filePath);
            var json = new NSString(data, NSStringEncoding.UTF8);
            var results = JsonConvert.DeserializeObject<T>(json, settings);

            return results;
        }
    }
}

