using System;
using Shared.Common;
using Security;
using Foundation;

namespace iOS
{
    public class iOSSecureKeyValueStore : BaseService, ISecureKeyValueStore
	{
		public void Store (string key, string value)
		{
			var recordToQueryWith = new SecRecord (SecKind.GenericPassword) {
				Generic = NSData.FromString (key)
			};

			var recordToAdd = new SecRecord (SecKind.GenericPassword) {
				Generic = NSData.FromString (key),
				ValueData = NSData.FromString (value),
				Account = key,
				Service = key
			};


			SecStatusCode queryStatus;
			SecKeyChain.QueryAsRecord (recordToQueryWith, out queryStatus);
			if (queryStatus == SecStatusCode.Success) //existing found
			{
				SecStatusCode removeStatus = SecKeyChain.Remove (recordToQueryWith);

				if (removeStatus != SecStatusCode.Success) {
					throw new Exception ("Could not remove existing key."); //TODO: need to use another exception?
				} 
			} 

			var addStatus = SecKeyChain.Add (recordToAdd);

			if (addStatus != SecStatusCode.Success && addStatus != SecStatusCode.DuplicateItem) {
				throw new Exception ("Could not add key successfully."); //TODO: need to use another exception?
			}

		}

		public string Retrieve (string key)
		{
			var recordToQueryWith = new SecRecord (SecKind.GenericPassword) {
				Generic = NSData.FromString (key)
			};

			SecStatusCode queryStatus;
			var queriedRecord = SecKeyChain.QueryAsRecord (recordToQueryWith, out queryStatus);
			if (queryStatus == SecStatusCode.Success) { 
				return queriedRecord.ValueData.ToString ();
			} 
			else 
			{
				return null;
			}

		}
	}
}

