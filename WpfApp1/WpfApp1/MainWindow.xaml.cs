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
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading;
using System.Windows.Interop;
using System.Web;
using WpfApp1.Models;
using XmlFeatures.XmlDoc;
using Application = System.Windows.Application;
using WpfApp1.HostWindowUtilities;
using System.Reflection;



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>


        public ViewSettings TextBlockSettings { get; set; }
        public string FilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var xml = new ExEmEl(@"C:\Users\DELL\Documents\GitRepoJosh\HWLogCriteria.xml", ExEmEl.NewDocument.No);
            TextBlockSettings = new ViewSettings();
            this.DataContext = TextBlockSettings;
            //var ViewHost = new SetNewProcessWindow();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Loaded_Window(object sender, RoutedEventArgs e)
        {
            var CurrApp = Application.Current.MainWindow;

            ReasonValue.Text = "howdy";
            InputDateWithIndexValue.Text = "ehehe";

            var info = new Information();
            info.ReasonValue = "dlfkj";
            var sally = info.GetType().GetProperties().
                               Where(prop => prop.PropertyType.Name.Equals("String")).ToList();
            sally.ForEach(prop => prop.SetValue(prop,LogicalTreeHelper.FindLogicalNode(CurrApp, prop.Name) != null ?
                                                 TextSearch.GetText(LogicalTreeHelper.FindLogicalNode(CurrApp, prop.Name)): "NoneExistant"));

        }
    }
}
