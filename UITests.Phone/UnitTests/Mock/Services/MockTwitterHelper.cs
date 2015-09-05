using System;
using Shared.Common;

namespace UnitTests
{
    public class MockTwitterHelper : ITwitterHelper
    {
        #region ITwitterHelper implementation
        public System.Threading.Tasks.Task<string> ExecuteRequest(string method, Uri uri, System.Collections.Generic.IDictionary<string, string> parameters = null)
        {
            return null;
        }
        public void Authenticate(Action callback)
        {
            
        }
        public System.Threading.Tasks.Task<bool> AccountExists()
        {
            return null;
        }
        public void DeleteAccount()
        {
            
        }
        public SocialAccount GetAccount()
        {
            return new SocialAccount();
        }
        public void Synchronize(SocialAccount socialAccount)
        {
            
        }
        #endregion
        
    }
}

