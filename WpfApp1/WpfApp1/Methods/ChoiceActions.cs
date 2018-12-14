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
using System.Xml.Linq;

namespace WpfApp1.Methods
{
    public class ChoiceActions
    {
        public Controls ControlInfo { get; set; }

        public CreateTreeDict Dict { get; set; }

        public ExEmEl Xml { get; set; }

        public AllWindows AllWind { get; set; }

        private enum SaveXmlFile { Yes, No }

        private enum CriteriaSearchKeyExistance { CritExists, KeyExists, NoneExist }

        public enum XtraChoices { None,ClearTextBoxes, RemoveNewLineAtEnd }

        private XElement SearchKeyNode { get; set; }

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

            AppearanceSettingsInstances.ForEach(obj => obj.Text = ChooseTextChangeToRed(CurrWindows.ToList().Select(window => GetWindowTextOrChangeToRed(obj, (Window)window)).ToArray()));

        }

        public string GetWindowTextOrChangeToRed(AppearanceSettings obj, Window Win)
        {
            var Results = Win.FindName(obj.NameProp)?.GetType().GetProperties().
            Where(prop => prop.Name.Equals("Text"));
            return Results == null || Results.Count() == 0 ? "" :
                   obj.ReqField == AppearanceSettings.RequiredField.Yes &&
                   Results.FirstOrDefault().GetValue(Win.FindName(obj.NameProp)).ToString().Count() == 0 ? "ChangeToRed" :
                   Results.FirstOrDefault().GetValue(Win.FindName(obj.NameProp)).ToString();
        }

        public string ChooseTextChangeToRed(string[] Options)
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
                MessageBoxResult result = MessageBox.Show($"Fields:\r\n\r\n {String.Join(",\r\n\r\n", RedFields).Replace("TextBoxObject", "").Replace("LabelObject", "") } ,\r\n\r\nare empty. Fill required fields and retry.",
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
            var ReplaceSearchKey = SaveXmlFile.Yes;
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
                    MessageBoxResult QuestionResult = MessageBox.Show("The search key already exists, and is about to be replicated and saved. Would you like to replace the current one?",
                      "Warning",
                      MessageBoxButton.YesNoCancel,
                      MessageBoxImage.Question);
                    ReplaceSearchKey = QuestionResult == MessageBoxResult.Yes ?
                                                     SaveXmlFile.Yes :
                                                     SaveXmlFile.No;
                }
                if (ReplaceSearchKey == SaveXmlFile.No || ReplaceSearchKey == SaveXmlFile.Yes)
                {
                    Xml.Find.FindByElement(new List<string> { "SearchGroup", "Name", ControlInfo.SearchGroup.Text });
                    if(ReplaceSearchKey == SaveXmlFile.Yes)
                    {
                        SearchKeyNode.Remove();
                    }
                    Xml.Find.ChildParentElement.Add(Xml.TreeCreation.NewTree.Nodes());
                    Xml.XDoc.Save(Xml.FilePath);
                    MessageBoxResult result = MessageBox.Show($"The generated searchkey was saved on: \r\n\r\n {Xml.FilePath}",
                      "Information",
                      MessageBoxButton.OK,
                      MessageBoxImage.Information);
                }
            }
        }

        public void CheckTreeExistence()
        {
            var CriteriaWRevisionXPath = Xml.XDoc.
                XPathSelectElement($"(//{ControlInfo.CriteriaReferenceWithRevision.NameProp}[@Value = '{ControlInfo.CriteriaReferenceWithRevision.Text ?? "kkk"}']/../..)[last()]");
            var SearchKeyXPath = Xml.XDoc.XPathSelectElement($"//{ControlInfo.SearchKey.NameProp}[@Name = '{ControlInfo.SearchKey.Text ?? "kkk"}']");

            var Test = CriteriaWRevisionXPath != null ? CriteriaSearchKeyExistance.CritExists :
                       SearchKeyXPath != null ? CriteriaSearchKeyExistance.KeyExists : CriteriaSearchKeyExistance.NoneExist;

            switch (Test)
            {
                case CriteriaSearchKeyExistance.CritExists:
                    SearchKeyNode = CriteriaWRevisionXPath;
                    WriteTreeTagValuesToApp();
                    break;
                case CriteriaSearchKeyExistance.KeyExists:
                    SearchKeyNode = SearchKeyXPath;
                    WriteTreeTagValuesToApp();
                    break;
                case CriteriaSearchKeyExistance.NoneExist:
                    MessageBoxResult result = MessageBox.Show($"The sought for search key could not be located!",
                      "Information",
                      MessageBoxButton.OK,
                      MessageBoxImage.Error);
                    break;
            }
        }

        public void WriteTreeTagValuesToApp()
        {
            var XmlNodes = SearchKeyNode.XPathSelectElements("*//*").ToList().
                Where(tag => tag.HasAttributes == true).ToList();

            XmlNodes.Add(SearchKeyNode.XPathSelectElements(".").First());

            XmlNodes.Add(SearchKeyNode.XPathSelectElements("(../..)[last()]").First());

            var CurrWindows = AllWind.GetType().GetProperties().
                Select(window => window.GetValue(AllWind));

            XmlNodes.ForEach(node => GetXmlSearchKeyTextToWindow(CurrWindows.ToList().Select(window => FindAppearanceSettingsTextToWindow(node, (Window)window)).ToArray(),
                                                                      node));

            XmlNodes.ToList().Where(node => node.Name.ToString() == "Product" ||
                                            node.Name.ToString() == "Variable" ||
                                            node.Name.ToString() == "SearchFilesFilter").ToList().
                ForEach(node => GetXmlSearchKeyTextToWindow(CurrWindows.ToList().Select(window => FindAppearanceSettingsTextToWindow(node, (Window)window)).ToArray(),
                                                                                        node,
                                                                                        XtraChoices.ClearTextBoxes));

            XmlNodes.ToList().Where(node => node.Name.ToString() == "Product" ||
                                            node.Name.ToString() == "Variable" ||
                                            node.Name.ToString() == "SearchFilesFilter").ToList().
                ForEach(node => GetXmlSearchKeyTextToWindowExpNProd(CurrWindows.ToList().Select(window => FindAppearanceSettingsTextToWindow(node, (Window)window)).ToArray(),
                                                                      node));

            var ExprNProd = new List<XElement>()
            {
                XmlNodes.ToList().Where(node => node.Name.ToString() == "Product").FirstOrDefault(),
                XmlNodes.ToList().Where(node => node.Name.ToString() == "Variable").FirstOrDefault(),
                XmlNodes.ToList().Where(node => node.Name.ToString() == "SearchFilesFilter").FirstOrDefault()
            };

                ExprNProd.ForEach(node => GetXmlSearchKeyTextToWindow(CurrWindows.ToList().Select(window => FindAppearanceSettingsTextToWindow(node, (Window)window)).ToArray(),
                                                                                        node,
                                                                                        XtraChoices.RemoveNewLineAtEnd));

            ControlInfo.GetType().GetProperties().
                Select(prop => (AppearanceSettings)prop.GetValue(ControlInfo)).ToList().
                ForEach(prop => { prop.Background = Brushes.White;
                                  prop.Foreground = Brushes.Black;
                                  prop.Text = "";
                });

            var SearchGroupNameXmlFIleToText = SearchKeyNode.XPathSelectElements("(../..)[last()]").First().FirstAttribute.Value.ToString();
            ControlInfo.TextBlockObject.Text = $"Searchgroup: {SearchGroupNameXmlFIleToText} \n\n\n " + 
                                               SearchKeyNode.ToString();
        }

        public object FindAppearanceSettingsTextToWindow(XElement node, Window Win)
        {
            var Results = Win.FindName(node.Name.ToString())?.GetType().GetProperties().
            Where(prop => prop.Name.Equals("Text"));
            return Results == null || Results.Count() == 0 ? null :
                    Win;
        }

        public void GetXmlSearchKeyTextToWindow(object[] Options, XElement obj, XtraChoices Choice = XtraChoices.None)
        {
            if (Options.Any(option => option != null))
            {
                var WinNoCast = Options.FirstOrDefault(option => option != null);
                var Win = (Window)WinNoCast;
                var WinTextProp = Win.FindName(obj.Name.ToString())?.GetType().GetProperties().
                    Where(prop => prop.Name.Equals("Text")).FirstOrDefault();
                WinTextProp.SetValue(Win.FindName(obj.Name.ToString()), 
                                  Choice == XtraChoices.ClearTextBoxes ? "" :
                                  Choice == XtraChoices.RemoveNewLineAtEnd ? 
                                  WinTextProp.GetValue(Win.FindName(obj.Name.ToString())).ToString().
                                  Substring(0, WinTextProp.GetValue(Win.FindName(obj.Name.ToString())).ToString().Length - "\n".Length) :
                                  obj.FirstAttribute.Value.ToString());
            }
        }

        public void GetXmlSearchKeyTextToWindowExpNProd(object[] Options, XElement obj)
        {            
            if (Options.Any(option => option != null))
            {
                var WinNoCast = Options.FirstOrDefault(option => option != null);
                var Win = (Window)WinNoCast;
                var ExistingValue = Win.FindName(obj.Name.ToString())?.GetType().GetProperties().
                    Where(prop => prop.Name.Equals("Text")).FirstOrDefault().
                    GetValue(Win.FindName(obj.Name.ToString()));
                var ModifiedString = (string)ExistingValue + string.Join(",", obj.Attributes().
                    Select(attr => attr.Name.ToString() + "NEXT" + attr.Value.ToString() + "NEXT").ToArray()).
                    Replace(",", "");

                ModifiedString = ModifiedString.Substring(0, ModifiedString.Length - "NEXT".Length) + "\n";

                Win.FindName(obj.Name.ToString())?.GetType().GetProperties().
                    Where(prop => prop.Name.Equals("Text")).FirstOrDefault().
                    SetValue(Win.FindName(obj.Name.ToString()), ModifiedString);
            }
        }
    }
}
