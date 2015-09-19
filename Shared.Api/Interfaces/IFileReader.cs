using System;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace Shared.Api
{
    public interface IFileReader
    {
        T ReadFile<T>(string fileName, string fileType, JsonSerializerSettings settings = null);
    }
}

