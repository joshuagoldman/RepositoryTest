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
using TreeBuilding;
using System.Xaml;
using SearchKey_GUI.Models;
using System.ComponentModel;

namespace SearchKey_GUI
{
    /// <summary>
    /// Interaction logic for VariablePage.xaml
    /// </summary>
    public partial class VariablePage : Window
    {

        public Definitions.UserControls VariableWindow { get; set; }

        public VariablePage()
        {
            InitializeComponent();


            VariableWindow = new Definitions.UserControls();

            this.MainGrid.DataContext = VariableWindow;

            VariableWindow.NameColumn.ItemsSource = new string[] { "hej", "Joshua2", "kattizar" };


        }

        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            VariableWindow.TextBoxVariables.Text = string.Join(",", VariableWindow.NameColumn.ItemsSource.Select(str => str + "\n")).Replace(",", "");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void NameVariable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                VariableWindow.NameColumn.ItemsSource = Functions.AddToArr(VariableWindow.NameColumn.ItemsSource, VariableWindow.NameColumn.Text);
            }
        }
    }
}
