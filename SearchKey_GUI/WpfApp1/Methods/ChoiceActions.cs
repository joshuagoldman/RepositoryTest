using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using SearchKey_GUI.Models;
using XmlFeatures.XmlDoc;
using System.Xml.XPath;
using System.Xml.Linq;

namespace SearchKey_GUI.Methods
{
    public class ChoiceActions
    {
        public Controls ControlInfo { get; set; }

        public CreateTreeDict Dict { get; set; }

        public ExEmEl Xml { get; set; }

        public AllWindows AllWind { get; set; }

        public enum TurnEmptyToRed { Yes, No }

        public TurnEmptyToRed NotifyMandatoryFields { get; set; }

        public enum SaveXmlFile { Yes, No }

        public SaveXmlFile SaveFile { get; set; }

        private enum CriteriaSearchKeyExistance { CritExists, KeyExists, NoneExist }

        public enum XtraChoices { None,ClearTextBoxes, RemoveNewLineAtEnd }

        private XElement SearchKeyNode { get; set; }

        public ChoiceActions()
        {
        }

        public void Write32App()
        {
            var TreeDict = Dict.GetTreeDict();
            Xml.XmlSearchInfo(tree_dict: TreeDict,
                              parent_above_child_no_attr: "SearchKeys",
                              instantiate_choice: ExEmEl.Instantiate.Both);
            Xml.TreeCreation.GetTree();
            ControlInfo.TextBlockObject.Text = $"Searchgroup: {ControlInfo.SearchGroup.Text} \n\n\n " +
                $"{Xml.TreeCreation.NewTree.XPathSelectElements("child::*").FirstOrDefault()?.ToString()}" ?? "";
        }
        public void WindowText2ControlInfoClass()
        {
            var CurrWindows = AllWind.GetType().GetProperties().
                Select(window => window.GetValue(AllWind));

            var AppearanceSettingsInstances = ControlInfo.GetType().
                GetProperties().
                Where(prop => prop.PropertyType == typeof(AppearanceSettings)).
                Select(prop => (AppearanceSettings)prop.GetValue(ControlInfo)).
                Where(prop => prop != null).ToList();

                AppearanceSettingsInstances.ForEach(obj => obj.Text = ChooseTextChangeToRed(CurrWindows.ToList().
                    Select(window => GetWindowTextOrChangeToRed(obj, (Window)window)).ToArray()));
        }

        public string GetWindowTextOrChangeToRed(AppearanceSettings obj, Window Win)
        {
            var Results = Win.FindName(obj.NameProp)?.GetType().GetProperties().
            Where(prop => prop.Name.Equals("Text"));
            return Results == null || Results.Count() == 0 ? "" :
                   obj.ReqField == AppearanceSettings.RequiredField.Yes &&
                   NotifyMandatoryFields == TurnEmptyToRed.Yes &&
                   Results.FirstOrDefault().GetValue(Win.FindName(obj.NameProp)).ToString().Count() == 0 ? "ChangeToRed" :
                   Results.FirstOrDefault().GetValue(Win.FindName(obj.NameProp)).ToString();
        }

        public string ChooseTextChangeToRed(string[] Options)
        {
            return Options.All(option => string.IsNullOrEmpty(option)) ?
                   Options.FirstOrDefault() :
                   Options.All(option => option != "ChangeToRed") ?
                   Options.Where(option => !string.IsNullOrEmpty(option)).FirstOrDefault() :
                   Options.Where(option => option == "ChangeToRed").FirstOrDefault();
        }

        public void RedNotificationPopUpMessage()
        {

            var RedFields = ControlInfo.GetType().GetProperties().ToList().
                Where(prop => prop.PropertyType == typeof(AppearanceSettings)).
                Where(prop => (AppearanceSettings)prop.GetValue(ControlInfo) != null).
                Select(prop => (AppearanceSettings)prop.GetValue(ControlInfo)).Where(prop => prop.Text.Equals("ChangeToRed")).
                Select(prop => prop.NameProp).ToList();

            if (PopupWindowTest())
            {
                MessageBoxResult result = MessageBox.Show($"Fields:\r\n\r\n {String.Join(",\r\n\r\n", RedFields).Replace("TextBoxObject", "").Replace("LabelObject", "") } ,\r\n\r\nare empty. Fill required fields and retry.",
                                          "Error",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);

                SaveFile = SaveXmlFile.No;
            }
        }

        public void SaveFileActions()
        {
            if (Xml?.Find == null)
            {
                Xml.XmlSearchInfo(parent_above_child_no_attr: "SearchKeys",
                  instantiate_choice: ExEmEl.Instantiate.Both);
            }
            NotifyMandatoryFields = TurnEmptyToRed.Yes;
            WindowText2ControlInfoClass();
            RedNotificationPopUpMessage();

            var SearchGroupElement =
                Xml.XDoc.XPathSelectElement($"*//SearchGroup[@Name = '{ControlInfo.SearchGroup.Text}']") ?? null;
            if (SearchGroupElement == null && SaveFile == SaveXmlFile.Yes)
            {
                MessageBoxResult result = MessageBox.Show("Invalid search group!",
                      "Error",
                      MessageBoxButton.OK,
                      MessageBoxImage.Error);

                SaveFile = SaveXmlFile.No;
            }
            else
            {

                SearchKeyNode =
                    Xml.XDoc.XPathSelectElement($"*//SearchKey[@Name = '{ControlInfo.SearchKey.Text}']") ?? null;
                var OverWrite = false;

                if (SearchKeyNode != null && SaveFile == SaveXmlFile.Yes)
                {
                    MessageBoxResult QuestionResult = MessageBox.Show("The search key already exists, and is about to be replicated and saved. Would you like to replace the current one?",
                      "Warning",
                      MessageBoxButton.YesNoCancel,
                      MessageBoxImage.Question);
                    OverWrite = QuestionResult == MessageBoxResult.Yes ?
                                                     true :
                                                     false;
                    SaveFile = QuestionResult == MessageBoxResult.Cancel ?
                                   SaveXmlFile.No :
                                   SaveXmlFile.Yes;
                }

                if (SaveFile == SaveXmlFile.Yes )
                {
                    Xml.Find.FindByElement(new List<string> { "SearchGroup", "Name", ControlInfo.SearchGroup.Text });
                    if(OverWrite)
                    {
                        SearchKeyNode?.Remove();
                    }

                    try
                    {
                        var StartIndex = ControlInfo.TextBlockObject.Text.IndexOf("<SearchKey");
                        var TreeInStringForm = ControlInfo.TextBlockObject.Text.Substring(StartIndex);
                        Xml.Find.ChildParentElement.Add(XElement.Parse(TreeInStringForm));
                        Xml.XDoc.Save(Xml.FilePath);
                        MessageBoxResult result = MessageBox.Show($"The generated searchkey was saved on: \r\n\r\n {Xml.FilePath}",
                          "Information",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
                    }
                    catch(Exception e)
                    {
                        MessageBoxResult QuestionResult = MessageBox.Show($"Couldn't save the search key due to the following error:\n\n{e.Message}",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
        }

        public void CheckTreeExistence()
        {
            var CriteriaWRevisionXPath = Xml.XDoc.
                XPathSelectElement($"(//{ControlInfo.CriteriaReferenceWithRevision.NameProp}[@Value = '{ControlInfo.CriteriaReferenceWithRevision.Text ?? "kkk"}']/../..)[last()]");
            var SearchKeyXPath = Xml.XDoc.XPathSelectElement($"//{ControlInfo.SearchKey.NameProp}[@Name = '{ControlInfo.SearchKey.Text ?? "kkkk"}']");

            var Test = SearchKeyXPath != null ? CriteriaSearchKeyExistance.KeyExists :
                       CriteriaWRevisionXPath != null ? CriteriaSearchKeyExistance.CritExists : CriteriaSearchKeyExistance.NoneExist;

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

                ExprNProd.Where(node => node != null).ToList().ForEach(node => GetXmlSearchKeyTextToWindow(CurrWindows.ToList().Select(window => FindAppearanceSettingsTextToWindow(node, (Window)window)).ToArray(),
                                                                                        node,
                                                                                        XtraChoices.RemoveNewLineAtEnd));

            EmptyWindow();

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

        public void EmptyWindow()
        {
            ControlInfo.GetType().GetProperties().
                Where(prop => prop.PropertyType == typeof(AppearanceSettings)).
                Select(prop => (AppearanceSettings)prop.GetValue(ControlInfo)).ToList().
                ForEach(prop => {
                    prop.Background = Brushes.White;
                    prop.Foreground = Brushes.Black;
                    prop.Text = "";
                });
        }

        public bool PopupWindowTest()
        {
            return ControlInfo.GetType().GetProperties().ToList().
                Where(prop => prop.PropertyType == typeof(AppearanceSettings)).
                Where(prop => prop.GetValue(ControlInfo) != null).
                Any(prop => prop.GetValue(ControlInfo).GetType().GetProperties().ToList().
                Any(subprop => subprop.GetValue(prop.GetValue(ControlInfo)).Equals("ChangeToRed")));
        }
    }
}
