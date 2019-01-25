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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EditHWPidListGUI.ViewModel;

namespace EditHWPidListGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel.ViewModel CurrentViewModel;
        public MainWindow()
        {
            InitializeComponent();
            //Connect view with viewmodel
            CurrentViewModel = new ViewModel.ViewModel();
            this.DataContext = CurrentViewModel;
        }

        //Funktion som pratar med ViewModelen och skickar key-down events
        void Window_KeyDown_KodeBehind(object sender, KeyEventArgs e)
        {
            if (CurrentViewModel == null) return;

            CurrentViewModel.Window_KeyDown(e);
        }
    }
}
