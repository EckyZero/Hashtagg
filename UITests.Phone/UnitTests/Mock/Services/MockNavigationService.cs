using System;
using Shared.Common;

namespace UnitTests
{
    public class MockNavigationService : IExtendedNavigationService
    {
        #region INavigationService implementation

        public void GoBack()
        {
            
        }

        public void NavigateTo(string pageKey)
        {
            
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            
        }

        public string CurrentPageKey
        {
            get { return string.Empty; }
        }

        #endregion

    }
}

