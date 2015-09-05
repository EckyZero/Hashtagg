using System;
using Shared.Common;

namespace UnitTests
{
    public class MockExtendedDialogService : IExtendedDialogService
    {
        #region IDialogService implementation

        public System.Threading.Tasks.Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return null;
        }
        public System.Threading.Tasks.Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            return null;
        }
        public System.Threading.Tasks.Task ShowMessage(string message, string title)
        {
            return null;
        }
        public System.Threading.Tasks.Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            return null;
        }
        public System.Threading.Tasks.Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            return null;
        }
        public System.Threading.Tasks.Task ShowMessageBox(string message, string title)
        {
            return null;
        }
        #endregion
        
    }
}

