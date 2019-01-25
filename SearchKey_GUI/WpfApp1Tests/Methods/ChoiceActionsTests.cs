using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.Methods;
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
using XmlFeatures.XmlDoc;
using Application = System.Windows.Application;
using WpfApp1.HostWindowUtilities;
using System.Reflection;
using PISetValue = System.Reflection.PropertyInfo;

namespace WpfApp1.Methods.Tests
{
    [TestClass()]
    public class ChoiceActionsTests
    {
        public GenerateActions GenAct { get; set; }

        [TestMethod()]
        public void ChangeToRedNotificationTest()
        {
            //arrange
            GenAct = GenAct ?? new GenerateActions()
            {
                GenDoc = Xml.XDoc

            };
            //act
            ChangeToRedNotification()
        }

        public void TestInitiation()
        {
            Xml = new ExEmEl(@"C:\Users\jogo\Documents\git_Test\HWLogCriteria.xml", ExEmEl.NewDocument.No);
            Info = new Information();
            TextBoxAppearance TextBoxInfo = new TextBoxAppearance();
            ChoicActMain = new ChoiceActions()
            {
                TextBoxInfo = TextBoxInfo
            };
            TextBlockSettings = new ViewSettings();
        }
    }
}