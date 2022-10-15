using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitectureCodeGenerator.Helpers
{
    public static class ProgressDialogHelper
    {
        public async static void ShowDialog(Action action)
        {
            //Dialog instance
            var fac = await VS.Services.GetThreadedWaitDialogAsync() as IVsThreadedWaitDialogFactory;
            IVsThreadedWaitDialog4 twd = fac.CreateInstance();

            //Start dialog
            twd.StartWaitDialog("nArchitecture Code Generator", "Please wait...", "", null, "", 1, true, true);

            //Wait 2 sec(sanki arkada deli işler yapıyormuş gibi)
            Thread.Sleep(2000);
            //Verilen işlem ne olursa olsun çalıştır diyoruz
            action.Invoke();

            //End dialog
            twd.EndWaitDialog();

        }
    }
}
