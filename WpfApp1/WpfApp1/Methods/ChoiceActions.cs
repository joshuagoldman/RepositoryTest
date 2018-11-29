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

namespace WpfApp1.Methods
{
    public class ChoiceActions
    {
        public TextBoxAppearance TextBoxInfo { get; set; }

        public Window CurrApp { get; set; }

        public CreateTreeDict Dict { get; set; }

        public MainWindow Main { get; set; }

        public ExEmEl Xml { get; set; }

        public ChoiceActions()
        {
        }
        public void ChangeToRedNotification()
        {
            CurrApp = Application.Current.MainWindow;

            var V = TextBoxInfo.GetType().
               GetProperties(BindingFlags.Public | BindingFlags.Instance).
               Where(prop => prop.PropertyType == typeof(ViewSettings) && (ViewSettings)prop.GetValue(TextBoxInfo) != null);

            var ViewSettingsInstances = TextBoxInfo.GetType().
                GetProperties().Select(prop => (ViewSettings)prop.GetValue(TextBoxInfo)).
                Where(prop => prop != null && !prop.NameProp.Contains("LabelObject")).ToList();

            ViewSettingsInstances.ForEach(obj => obj.Text = LogicalTreeHelper.FindLogicalNode(CurrApp, obj.NameProp) == null ? "ChangeToRed" :
                                                            string.IsNullOrEmpty(GetMainWindowText(obj)) ? 
                                                            "ChangeToRed" : GetMainWindowText(obj));
        }

        public string GetMainWindowText(ViewSettings obj)
        {
            return Main.FindName(obj.NameProp).GetType().GetProperties().
            Where(prop => prop.Name.Equals("Text")).FirstOrDefault().
            GetValue(LogicalTreeHelper.FindLogicalNode(CurrApp, obj.NameProp)).ToString();
        }

        public void RedNotificationPopUpMessage()
        {
            var PopupWindowTest = TextBoxInfo.GetType().GetProperties().ToList().Where(prop => prop.GetValue(TextBoxInfo) != null).
                Any(prop => prop.GetValue(TextBoxInfo).GetType().GetProperties().ToList().Any(subprop => subprop.GetValue(prop.GetValue(TextBoxInfo)).Equals("ChangeToRed")));


            var RedFields = TextBoxInfo.GetType().GetProperties().ToList().Where(prop => (ViewSettings)prop.GetValue(TextBoxInfo) != null).
                Select(prop => (ViewSettings)prop.GetValue(TextBoxInfo)).Where(prop => prop.Text.Equals("ChangeToRed")).
                Select(prop => prop.NameProp).ToList();

            if (PopupWindowTest)
            {
                MessageBoxResult result = MessageBox.Show($"Fields:\r\n\r\n {String.Join(",\r\n\r\n", RedFields).Replace("TextBoxObject","").Replace("LabelObject", "") } ,\r\n\r\nare empty. Fill required fields and retry.",
                                          "Error",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
            }
            else
            {
                var TreeDict = Dict.GetTreeDict();
                Xml.XmlSearchInfo(tree_dict: TreeDict,
                                  tree_root: $"SearchGroup,Name,{TextBoxInfo.SearchGroup.Text}",
                                  parent_above_child_no_attr: "SearchKeys",
                                  instantiate_choice: ExEmEl.Instantiate.Both);

                Xml.TreeCreation.GetTree();

                TextBoxInfo.TextBlockObject.Text = Xml.TreeCreation.NewTree.ToString();

                Xml.XDoc.Save(Xml.FilePath);
            }
        }

    }
}
