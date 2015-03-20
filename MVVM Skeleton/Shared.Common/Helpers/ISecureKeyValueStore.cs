namespace Shared.Common
{
	public interface ISecureKeyValueStore
	{
		void Store (string key, string value);
		string Retrieve (string key);
	}
}

