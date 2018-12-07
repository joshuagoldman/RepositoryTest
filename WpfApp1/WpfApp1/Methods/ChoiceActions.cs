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
using System.Reflection;
using System.Xml.XPath;

namespace WpfApp1.Methods
{
    public class ChoiceActions
    {
        public Controls ControlInfo { get; set; }

        public CreateTreeDict Dict { get; set; }

        public ExEmEl Xml { get; set; }

        public AllWindows AllWind { get; set; }

        private enum SaveXmlFile { Yes, No }

        public ChoiceActions()
        {
        }
        public void ChangeToRedNotification()
        {
            var CurrWindows = AllWind.GetType().GetProperties().
                Select(window => window.GetValue(AllWind));

            var AppearanceSettingsInstances = ControlInfo.GetType().
                GetProperties().Select(prop => (AppearanceSettings)prop.GetValue(ControlInfo)).
                Where(prop => prop != null).ToList();

            AppearanceSettingsInstances.ForEach(obj => obj.Text = ChooseText(CurrWindows.ToList().Select(window => GetWindowText(obj, (Window)window)).ToArray()));

        }

        public string GetWindowText(AppearanceSettings obj, Window Win)
        {
            var Results = Win.FindName(obj.NameProp)?.GetType().GetProperties().
            Where(prop => prop.Name.Equals("Text"));
            return Results == null || Results.Count() == 0 ? "" : 
                   obj.ReqField == AppearanceSettings.RequiredField.Yes &&
                   Results.FirstOrDefault().GetValue(Win.FindName(obj.NameProp)).ToString().Count() == 0 ? "ChangeToRed" :
                   Results.FirstOrDefault().GetValue(Win.FindName(obj.NameProp)).ToString();
        }

        public string ChooseText(string[] Options)
        {
            return Options.All(option => string.IsNullOrEmpty(option)) ?
                   Options.FirstOrDefault() :
                   Options.All(option => option != "ChangeToRed") ?
                   Options.Where(option => option != "ChangeToRed" && !string.IsNullOrEmpty(option)).FirstOrDefault() :
                   Options.Where(option => option == "ChangeToRed").FirstOrDefault();


        }

        public void RedNotificationPopUpMessage()
        {
            var PopupWindowTest = ControlInfo.GetType().GetProperties().ToList().Where(prop => prop.GetValue(ControlInfo) != null).
                Any(prop => prop.GetValue(ControlInfo).GetType().GetProperties().ToList().Any(subprop => subprop.GetValue(prop.GetValue(ControlInfo)).Equals("ChangeToRed")));


            var RedFields = ControlInfo.GetType().GetProperties().ToList().Where(prop => (AppearanceSettings)prop.GetValue(ControlInfo) != null).
                Select(prop => (AppearanceSettings)prop.GetValue(ControlInfo)).Where(prop => prop.Text.Equals("ChangeToRed")).
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
                                  parent_above_child_no_attr: "SearchKeys",
                                  instantiate_choice: ExEmEl.Instantiate.Both);
                Xml.TreeCreation.GetTree();
                ControlInfo.TextBlockObject.Text = $"Searchgroup: {ControlInfo.SearchGroup.Text} \n\n\n " +
                    $"{Xml.TreeCreation.NewTree.XPathSelectElements("child::*").FirstOrDefault().ToString()}";
            }
        }

        public void SaveFileOrNot()
        {
            var SaveThisFile = SaveXmlFile.Yes;
            var SearchGroupElement =
                Xml.XDoc.XPathSelectElement($"*//SearchGroup[@Name = '{ControlInfo.SearchGroup.Text}']") ?? null;
            if (SearchGroupElement == null)
            {
                MessageBoxResult result = MessageBox.Show("Invalid search group!",
                      "Error",
                      MessageBoxButton.OK,
                      MessageBoxImage.Error);
            }
            else
            {
                var SearchKeyElement =
    Xml.XDoc.XPathSelectElement($"*//SearchKey[@Name = '{ControlInfo.SearchKey.Text}']") ?? null;
                if (SearchKeyElement != null)
                {
                    MessageBoxResult QuestionResult = MessageBox.Show("The search key already exists. Are you sure you want to continue and save?",
                      "Warning",
                      MessageBoxButton.YesNo,
                      MessageBoxImage.Question);
                    SaveThisFile = QuestionResult == MessageBoxResult.Yes ?
                                                     SaveXmlFile.Yes :
                                                     SaveXmlFile.No;
                }
                if (SaveThisFile == SaveXmlFile.Yes)
                {
                    Xml.Find.FindByElement(new List<string> { "SearchGroup", "Name", ControlInfo.SearchGroup.Text });
                    Xml.Find.ChildParentElement.Add(Xml.TreeCreation.NewTree.Nodes());
                    Xml.XDoc.Save(Xml.FilePath);
                    MessageBoxResult result = MessageBox.Show($"The generated searchkey was saved in: \r\n\r\n {Xml.FilePath}",
                      "Information",
                      MessageBoxButton.OK,
                      MessageBoxImage.Information);
                }
            }
        }

    }
}
