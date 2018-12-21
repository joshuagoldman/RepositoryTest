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
using SearchKey_GUI.Models;
using SearchKey_GUI.Methods;
using XmlFeatures.XmlDoc;
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
            Xml = new ExEmEl(@"C:\Users\jogo\Gitrepos\Nodetest\Nodetest\Datapackets\HWLogCriteria\documents\HWLogCriteria.xml",
                                              ExEmEl.NewDocument.No);

            //C:\Users\DELL\Documents\GitRepoJosh

            Controls ControlInfo = new Controls();
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

            Main.DataContext = ControlInfo;            
        }
    }
}
