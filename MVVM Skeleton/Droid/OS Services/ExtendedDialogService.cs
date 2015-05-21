using System;
using System.Threading.Tasks;
using Android.App;
using Shared.Common;

namespace Droid
{
	public class ExtendedDialogService : BaseService, IExtendedDialogService
	{
        /// <summary>
        /// Displays information about an error.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        /// <remarks>Displaying dialogs in Android is synchronous. As such,
        /// this method will be executed synchronously even though it can be awaited
        /// for cross-platform compatibility purposes.</remarks>
        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            var tcs = new TaskCompletionSource<bool>();
            var builder = CreateBuilder(
                message, 
                title, 
                buttonText, 
                null, 
                afterHideCallback,
                null,
                tcs.SetResult);

            builder.Show();
            return tcs.Task;
        }

        /// <summary>
        /// Displays information about an error.
        /// </summary>
        /// <param name="error">The exception of which the message must be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        /// <remarks>Displaying dialogs in Android is synchronous. As such,
        /// this method will be executed synchronously even though it can be awaited
        /// for cross-platform compatibility purposes.</remarks>
        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            var tcs = new TaskCompletionSource<bool>();
            var builder = CreateBuilder(
                error.Message, 
                title, 
                buttonText, 
                null, 
                afterHideCallback,
                null,
                tcs.SetResult);

            builder.Show();
            return tcs.Task;
        }

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button with the text "OK".
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        /// <remarks>Displaying dialogs in Android is synchronous. As such,
        /// this method will be executed synchronously even though it can be awaited
        /// for cross-platform compatibility purposes.</remarks>
        public Task ShowMessage(string message, string title)
        {
            var tcs = new TaskCompletionSource<bool>();
            var builder = CreateBuilder(
                message, 
                title,
                "OK",
                null,
                null,
                null,
                null);

            builder.Show();
            return tcs.Task;
        }

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        /// <remarks>Displaying dialogs in Android is synchronous. As such,
        /// this method will be executed synchronously even though it can be awaited
        /// for cross-platform compatibility purposes.</remarks>
        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            var tcs = new TaskCompletionSource<bool>();
            var builder = CreateBuilder(
                message, 
                title, 
                buttonText, 
                null, 
                afterHideCallback,
                null,
                tcs.SetResult);

            builder.Show();
            return tcs.Task;
        }

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonConfirmText">The text shown in the "confirm" button
        /// in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="buttonCancelText">The text shown in the "cancel" button
        /// in the dialog box. If left null, the text "Cancel" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after
        /// the dialog box is closed by the user. The callback method will get a boolean
        /// parameter indicating if the "confirm" button (true) or the "cancel" button
        /// (false) was pressed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited. The task will return
        /// true or false depending on the dialog result.</returns>
        /// <remarks>Displaying dialogs in Android is synchronous. As such,
        /// this method will be executed synchronously even though it can be awaited
        /// for cross-platform compatibility purposes.</remarks>
        public Task<bool> ShowMessage(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            var tcs = new TaskCompletionSource<bool>();
            var builder = CreateBuilder(
                message,
                title,
                buttonConfirmText,
                buttonCancelText ?? "Cancel",
                null,
                afterHideCallback,
                tcs.SetResult);

            builder.Show();
            return tcs.Task;
        }

        /// <summary>
        /// Displays information to the user in a simple dialog box. The dialog box will have only
        /// one button with the text "OK". This method should be used for debugging purposes.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        /// <remarks>Displaying dialogs in Android is synchronous. As such,
        /// this method will be executed synchronously even though it can be awaited
        /// for cross-platform compatibility purposes.</remarks>
        public Task ShowMessageBox(string message, string title)
        {
            var tcs = new TaskCompletionSource<bool>();
            var builder = CreateBuilder(
                message,
                title,
                "OK",
                null,
                null,
                null,
                null);
            builder.Show();
            return tcs.Task;
        }

        private AlertDialog.Builder CreateBuilder(
            string message,
            string title,
            string buttonConfirmText = "OK",
            string buttonCancelText = null,
            Action afterHideCallback = null,
            Action<bool> afterHideCallbackWithResponse = null,
            Action<bool> afterHideInternal = null)
        {
            var builder = new AlertDialog.Builder(_activity);

            builder.SetMessage(message);

            if (!string.IsNullOrEmpty(title))
            {
                builder.SetTitle(title);
            }

            if (!string.IsNullOrEmpty(buttonConfirmText))
            {
                builder.SetPositiveButton(
                    buttonConfirmText,
                    (s, e) =>
                    {
                        if (afterHideCallback != null)
                        {
                            afterHideCallback();
                        }

                        if (afterHideCallbackWithResponse != null)
                        {
                            afterHideCallbackWithResponse(true);
                        }

                        if (afterHideInternal != null)
                        {
                            afterHideInternal(true);
                        }
                    });
            }

            if (!string.IsNullOrEmpty(buttonCancelText))
            {
                builder.SetNegativeButton(
                    buttonCancelText,
                    (s, e) =>
                    {
                        if (afterHideCallbackWithResponse != null)
                        {
                            afterHideCallbackWithResponse(false);
                        }

                        if (afterHideInternal != null)
                        {
                            afterHideInternal(false);
                        }
                    });
            }

            return builder;
        }
	}
}

