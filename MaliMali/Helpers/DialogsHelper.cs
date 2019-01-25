using System;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace MaliMali.Helpers
{
    // This class uses UserDialogs plugin https://github.com/aritchie/userdialogs
    public class DialogsHelper
    {
        public enum Errors
        {
            NetworkError,
            Defined,
            UndefinedError,
            InputError
        }

        readonly string UndefinedError = "Something went wrong, try again later.";
        readonly string NetworkError = "Connection error.";
        readonly string InputError = " is required.";

        public void HandleError(Errors error, string message)
        {
            switch (error)
            {
                case Errors.NetworkError:
                    message = "    " + NetworkError + "    ";
                    break;
                case Errors.UndefinedError:
                    message = "    " + UndefinedError + "    ";
                    break;
                case Errors.Defined:
                    message = "    " + message + "    ";
                    break;
                case Errors.InputError:
                    message = "    " + message + InputError + "    ";
                    break;
            }
            UserDialogs.Instance.Toast(new ToastConfig(message)
            .SetBackgroundColor(Color.FromHex("#333333"))
            .SetMessageTextColor(Color.White)
            .SetDuration(TimeSpan.FromSeconds(3))
            .SetPosition(ToastPosition.Bottom)
            .SetAction(x => x
            .SetText("OK")
            .SetTextColor(Color.FromHex("#03a9f5"))
            ));
        }

        public IProgressDialog progressDialog = UserDialogs.Instance.Progress(new ProgressDialogConfig
        {
            AutoShow = false,
            CancelText = null,
            IsDeterministic = false,
            MaskType = MaskType.Black,
            Title = null
        });

        public static DialogsHelper Instance { get; } = new DialogsHelper();
    }
}
