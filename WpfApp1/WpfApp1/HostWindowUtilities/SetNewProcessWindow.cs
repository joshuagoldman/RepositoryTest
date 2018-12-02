/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Interop;

namespace WpfApp1.HostWindowUtilities
{
    public class SetNewProcessWindow
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);
        [DllImport("user32.dll")]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll")]
        internal static extern long SetWindowLongA(IntPtr hWnd, int nIndex, long dwNewLong);

        public SetNewProcessWindow()
        {
            var host = new HostWindow();
            Process p = Process.Start("notepad++.exe", @"C:\Users\jogo\Documents\git_Test\HWLogCriteria.xml");
            Thread.Sleep(500);
            p.WaitForInputIdle();
            var helper = new WindowInteropHelper(Window.GetWindow(host.HostForm));
            SetParent(p.MainWindowHandle, helper.Handle);
        }
    }
}*/
