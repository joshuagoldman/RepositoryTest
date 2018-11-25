using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Interop;
using System.Windows.Forms;
using WpfApp1.Models;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            var info = new Information(InputDateWithIndexValue.Name,
                                       CriteriaReferenceWithRevisionValue.Name,
                                       ResponsibleValue.Name,
                                       ReasonValue.Name);
            var host = new HostWindow();
            host.Show();
            Process p = Process.Start("notepad++.exe", @"C:\Users\DELL\Documents\GitRepoJosh\HWLogCriteria.xml");
            Thread.Sleep(500);
            p.WaitForInputIdle();
            var helper = new WindowInteropHelper(GetWindow(host.HostForm));
            SetParent(p.MainWindowHandle, helper.Handle);
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);
        [DllImport("user32.dll")]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll")]
        internal static extern long SetWindowLongA(IntPtr hWnd, int nIndex, long dwNewLong);

    }
}
