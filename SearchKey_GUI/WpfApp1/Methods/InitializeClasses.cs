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
using System.Reflection;
using SearchKey_GUI.Models;
using Ericsson.AM.LogAnalyzer;
using SearchKey_GUI.Methods;
using XmlFeatures.XmlDoc;
using System.Xml.XPath;
using System.IO;

namespace SearchKey_GUI.Methods
{
    public class InitializeClasses
    {
        public MainWindow Main { get; set; }

        public ExEmEl Xml { get; set; }

        public ChoiceActions ChoicAct { get; set; }

        public AllWindows AllWind { get; set; }

        public ExpressionWindow ExWindow { get; set; }

        public ProductsWindow ProdWindow { get; set; }

        public InfoTextWindow InfoTextWin { get; set; }

        public string FilePath { get; set; }

        public void PerformInitiliazation()
        {

            var hWLogCritStream = Assembly.GetAssembly(typeof(LogAnalyzer)).GetManifestResourceStream("Ericsson.AM.LogAnalyzer.EmbeddedCriteria.RBS6000.Aftermarket.HWLogCriteria.xml");

            if (hWLogCritStream != null)
            {
                var filepath = @"C:\Users\jogo\Gitrepos\LogAnalyzerRepo\LogAnalyzer\Ericsson.AM.LogAnalyzer\EmbeddedCriteria\RBS6000\Aftermarket/HWLogCriteria.xml";

                Xml = new ExEmEl(filepath: filepath, stream: hWLogCritStream, yesorno: ExEmEl.NewDocument.No);

                var revision = Xml.XDoc.XPathSelectElement("*//Revision").Value;

                MessageBox.Show($"The criteria to be used has revision: {revision}",
                "Information",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }


            else
            {
                MessageBox.Show($"Please browse the HWLogCriteria.xml file directory in which changes are to be saved",
                   "Information",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);

                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.SelectedPath = @"C:\Users\jogo\Gitrepos\LogAnalyzerRepo\LogAnalyzer\Ericsson.AM.LogAnalyzer\EmbeddedCriteria\RBS6000\Aftermarket";
                dialog.ShowDialog();
                var filepath = dialog.SelectedPath + "\\HWLogCriteria.xml";

                Xml = new ExEmEl(filepath, yesorno: ExEmEl.NewDocument.No);

                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }

            var ControlInfo = new Controls(Xml);

            Main.DataContext = ControlInfo;

            CreateTreeDict Dict = new CreateTreeDict()
            {
                Information = ControlInfo
            };

            AllWind = new AllWindows
            {
                ExWindow = ExWindow,
                ProdWindow = ProdWindow,
                InfoTextWin = InfoTextWin,
                Main = Main
            };


            ChoicAct = new ChoiceActions()
            {
                ControlInfo = ControlInfo,
                Dict = Dict,
                AllWind = AllWind,
                Xml = Xml
            };

            Main.KeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    ControlInfo.SearchKey.Text = ControlInfo.SearchKey.ItemsSource.Any(item => item.Contains(ControlInfo.SearchKey.Text)) ?
                        ControlInfo.SearchKey.ItemsSource.FirstOrDefault(item => item.Contains(ControlInfo.SearchKey.Text)) : ControlInfo.SearchKey.Text;

                }
            };

        }

    }
}
