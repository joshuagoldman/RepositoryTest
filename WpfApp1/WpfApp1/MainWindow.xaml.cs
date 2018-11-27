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
using System.Xml.Linq;
using WpfApp1.Models;
using WpfApp1.Methods;
using XmlFeatures.XmlDoc;
using Application = System.Windows.Application;
using WpfApp1.HostWindowUtilities;
using System.Reflection;
using PISetValue = System.Reflection.PropertyInfo;



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

        public ExEmEl Xml { get; set; }

        public ViewSettings TextBlockSettings { get; set; }

        public Information Info { get; set; }

        public GenerateActions GenAct { get; set; }

        public ChoiceActions ChoicActMain { get; set; }

        public string FilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();            
        }

        public void Generate_Click(object sender, RoutedEventArgs e)
        {
            GenAct = GenAct ?? new GenerateActions()
            {
                GenDoc = Xml.XDoc
                
            };
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Loaded_Window(object sender, RoutedEventArgs e)
        {
            Xml = new ExEmEl(@"C:\Users\jogo\Documents\git_Test\HWLogCriteria.xml", ExEmEl.NewDocument.No);
            Info = new Information();
            TextBoxAppearance TextBoxInfo = new TextBoxAppearance();
            ChoicActMain = new ChoiceActions()
            {
                TextBoxInfo = TextBoxInfo
            };
            TextBlockSettings = new ViewSettings();
            this.DataContext = TextBlockSettings;
        }

    }
}
