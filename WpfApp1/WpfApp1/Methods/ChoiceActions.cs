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

        public ChoiceActions()
        {
        }
        public void ChangeToRedNotification()
        {
            var CurrApp = Application.Current.MainWindow;

            var Props = TextBoxInfo.GetType().GetProperties().
                            Where(prop => prop.PropertyType.Name.Equals("String")).ToList();
            Props.ForEach(prop => prop.SetValue(TextBoxInfo, LogicalTreeHelper.FindLogicalNode(CurrApp, prop.Name).Equals("ChangeToRed") ?
                                                "ChangeToRed" : TextSearch.GetText(LogicalTreeHelper.FindLogicalNode(CurrApp, prop.Name))));

        }

        public void RedNotificationPopUpMessage()
        {
            var PopupWindowTest = TextBoxInfo.GetType().GetProperties().ToList().Any(vals => vals.GetValue(TextBoxInfo).Equals("ChangeToRed"));

            var RedFields = TextBoxInfo.GetType().GetProperties().ToList().Where(vals => vals.GetValue(TextBoxInfo).Equals("ChangeToRed"));

            if (PopupWindowTest)
            {
                MessageBoxResult result = MessageBox.Show($"Fields {RedFields.ToList().Select(field => field.ToString())} are empty. Fill required fields and retry.",
                                          "Confirmation",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
            }
            else
            {

            }
        }

    }
}
