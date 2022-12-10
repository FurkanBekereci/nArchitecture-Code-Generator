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
        public async static Task ShowDialogAsync(Action action)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            //Dialog instance
            var fac = (IVsThreadedWaitDialogFactory)await VS.Services.GetThreadedWaitDialogAsync();
            IVsThreadedWaitDialog4 twd = fac.CreateInstance();

            //Start dialog
            twd.StartWaitDialog("nArchitecture Code Generator", "Please wait...", "", null, "", 1, true, true);

            //Given action will run
            action.Invoke();

            //End dialog
            twd.EndWaitDialog();

        }
    }
}
