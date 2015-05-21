using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BL
{
    public enum BLResponseType
    {
        SUCCESS,
        NO_CONNECTION,
        ERROR
    }

    public class BLResponse<T>
    {
        public BLResponseType ResponseType { get; private set; }
        public T Result { get; private set; }

        public BLResponse(T result, BLResponseType responseType)
        {
            Result = result;
            ResponseType = responseType;
        }

        public BLResponse()
        {
            Result = default(T);
            ResponseType = BLResponseType.ERROR;
        }
    }
}
