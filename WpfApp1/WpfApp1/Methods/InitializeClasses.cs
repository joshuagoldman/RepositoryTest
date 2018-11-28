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

namespace WpfApp1.Methods
{
    public class InitializeClasses
    {
        public MainWindow Main { get; set; }

        public ExEmEl Xml { get; set; }

        public ViewSettings TextBlockSettings { get; set; }

        public GenerateActions GenAct { get; set; }

        public ChoiceActions ChoicActMain { get; set; }

        public string FilePath { get; set; }

        public InitializeClasses()
        {
            Xml = new ExEmEl(@"C:\Users\jogo\Documents\git_Test\HWLogCriteria.xml", ExEmEl.NewDocument.No);
            TextBoxAppearance TextBoxInfo = new TextBoxAppearance();
            CreateTreeDict NewDict = new CreateTreeDict()
            {
                Information = TextBoxInfo
            };

            ChoicActMain = new ChoiceActions()
            {
                TextBoxInfo = TextBoxInfo,
                Dict = NewDict
            };

            TextBlockSettings = new ViewSettings();

            Main.DataContext = TextBoxInfo;

            GenAct = GenAct ?? new GenerateActions()
            {
                ChoicAct = ChoicActMain
            };

        }

    }
}
