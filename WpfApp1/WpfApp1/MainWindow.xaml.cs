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
using System.Windows.Forms;
using WpfApp1.Models;
using XmlFeatures.XmlDoc;
using WpfApp1.HostWindowUtilities;



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewSettings TextBlockSettings { get; set; }
        public string FilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var info = new Information(InputDateWithIndexValue.Name,
                                       CriteriaReferenceWithRevisionValue.Name,
                                       ResponsibleValue.Name,
                                       ReasonValue.Name);

            var xml = new ExEmEl(@"C:\Users\jogo\Documents\git_Test\HWLogCriteria.xml", ExEmEl.NewDocument.No);
            TextBlockSettings = new ViewSettings();
            this.DataContext = TextBlockSettings;
            var ViewHost = new SetNewProcessWindow();
            XmlDataProvider jj = new XmlDataProvider();
            jj.Document = xml.Doc;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
