using System;
using Shared.Api;
using System.IO;
using Newtonsoft.Json;

namespace Droid.Phone
{
    public class FileReader : IFileReader
    {
        public FileReader () { }

        public T ReadFile<T> (string fileName, string fileType, JsonSerializerSettings settings = null)
        {
            var stream = MainApplication.Context.Assets.Open (string.Format("{0}.{1}", fileName, fileType));
            var reader = new StreamReader(stream);
            var json = reader.ReadToEnd ();
            var results = JsonConvert.DeserializeObject<T>(json);

            return results;
        }
    }
}

