using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace SearchKey_GUI.Models
{


    public partial class Controls
    {

        AppearanceSettings textblock_object = new AppearanceSettings();
        AppearanceSettings search_key = new AppearanceSettings();
        AppearanceSettings search_group = new AppearanceSettings();
        AppearanceSettings standard_label_appearance_1 = new AppearanceSettings();
        AppearanceSettings standard_textbox_appearance_1 = new AppearanceSettings();
        AppearanceSettings standard_combobox_appearance_1 = new AppearanceSettings();
        AppearanceSettings longer_text_inputtemplate = new AppearanceSettings();        

        public AppearanceSettings TextBlockObject
        {
            get
            {
                textblock_object.FontSize = double.Parse("14");
                textblock_object.Background = Brushes.Transparent;
                textblock_object.Foreground = Brushes.Black;
                return textblock_object;
            } 
            set
            {
                textblock_object = value;
            }
        }

        public AppearanceSettings SearchKey
        {
            get
            {
                EmptyFieldToRed(search_key);
                return search_key;
            }
            set
            {
                search_key = value;
            }
        }
        
        public AppearanceSettings SearchGroup
        {
            get
            {
                search_group.ItemsSource = new string[] { "DUW_DUL_DUS", "Radio", "DUG", "Baseband", "TCU_SIU" };
                return search_group;
            }
            set
            {
                search_group = value;
            }
        }

        public AppearanceSettings StandardLabelAppearance1
        {
            get
            {
                SetStandardLabelAppearance1(standard_label_appearance_1);
                return standard_label_appearance_1;
            }
            set
            {
                standard_label_appearance_1 = value;
            }
        }

        public AppearanceSettings StandardTextBoxAppearance1
        {
            get
            {
                SetStandardTextBoxAppearance1(standard_textbox_appearance_1);
                return standard_textbox_appearance_1;
            }
            set
            {
                standard_textbox_appearance_1 = value;
            }
        }

        public AppearanceSettings StandardComboBoxAppearance1
        {
            get
            {
                SetStandardLabelAppearance1(standard_combobox_appearance_1);
                return standard_combobox_appearance_1;
            }
            set
            {
                standard_combobox_appearance_1 = value;
            }
        }

        public AppearanceSettings LongerTextInputTemplate
        {
            get
            {
                return longer_text_inputtemplate;
            }
            set
            {
                reason = value;
            }
        }

        public Controls()
        {
            GetType().GetProperties().ToList().Where(prop => (AppearanceSettings)prop.GetValue(this) != null).ToList().
                ForEach(prop => prop.GetValue(this).GetType().GetProperties().
                Where(subprop => subprop.GetValue(prop.GetValue(this)) != null).
                Where(subprop => subprop.Name.Equals("NameProp")).ToList().
                ForEach(subprop => subprop.SetValue(prop.GetValue(this), prop.Name)));
        }

        private void EmptyFieldToRed(AppearanceSettings Obj)
        {
            Obj.Background = Obj.Text == "ChangeToRed" ? Brushes.Red : Brushes.White;
            Obj.Foreground = Obj.Text == "ChangeToRed" ? Brushes.White : Brushes.Black;
            Obj.ReqField = AppearanceSettings.RequiredField.Yes;
        }

        private void EmptyButtonWindowToRed(AppearanceSettings Button, params AppearanceSettings[] Objects)
        {
            Button.Background = Objects.Any(obj => obj.Text == "ChangeToRed") ? Brushes.Red : Brushes.White;
        }

        private AppearanceSettings SetStandardLabelAppearance1(AppearanceSettings Obj)
        {
            Obj.TextWrapping = TextWrapping.Wrap;
            Obj.Background = Brushes.DimGray;
            Obj.FontSize = double.Parse("15");
            return Obj;
        }

        private AppearanceSettings SetStandardTextBoxAppearance1(AppearanceSettings Obj)
        {
            Obj.TextWrapping = TextWrapping.Wrap;
            Obj.TextAlignment = TextAlignment.Center;
            Obj.FontSize = double.Parse("15");
            return Obj;
        }

        private AppearanceSettings SetStandardComboBoxAppearance1(AppearanceSettings Obj)
        {
            Obj.Background = Brushes.DimGray;
            Obj.FontSize = double.Parse("15");
            return Obj;
        }

    }
}

