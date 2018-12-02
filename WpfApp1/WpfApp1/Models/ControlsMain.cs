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

namespace WpfApp1.Models
{


    public partial class Controls
    {

        AppearanceSettings textblock_object = new AppearanceSettings();
        AppearanceSettings search_key_textbox_object = new AppearanceSettings();
        AppearanceSettings searchgroup_Label_object = new AppearanceSettings();
        AppearanceSettings standard_label_appearance_1 = new AppearanceSettings();
        AppearanceSettings standard_textbox_appearance_1 = new AppearanceSettings();

        public AppearanceSettings TextBlockObject
        {
            get
            {
                textblock_object.FontSize = double.Parse("25");
                textblock_object.Background = Brushes.LightPink;
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
                EmptyFieldToRed(search_key_textbox_object);
                search_key_textbox_object.FontSize = double.Parse("15");
                search_key_textbox_object.Background = Brushes.Yellow;
                return search_key_textbox_object;
            }
            set
            {
                search_key_textbox_object = value;
            }
        }
        
        public AppearanceSettings SearchGroup
        {
            get
            {
                EmptyFieldToRed(searchgroup_Label_object);
                return searchgroup_Label_object;
            }
            set
            {
                searchgroup_Label_object = value;
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

        public Controls()
        {
            GetType().GetProperties().ToList().Where(prop => (AppearanceSettings)prop.GetValue(this) != null).ToList().
                ForEach(prop => prop.GetValue(this).GetType().GetProperties().
                Where(subprop => subprop.GetValue(prop.GetValue(this)) != null).
                Where(subprop => subprop.Name.Equals("NameProp")).ToList().
                ForEach(subprop => subprop.SetValue(prop.GetValue(this), prop.Name)));


            /* var slagerak = GetType().GetProperties().ToList().Where(prop => (AppearanceSettings)prop.GetValue(this) != null).ToList().
                 Select(prop => prop.GetValue(this).GetType().GetProperties().
                 Where(subprop => subprop.GetValue(prop.GetValue(this)) != null).
                 Where(subprop => subprop.Name.Equals("Background")).ToList().
                 Select(subprop => subprop.GetValue(prop.GetValue(this))));*/

            /*Obj.GetType().GetProperties().ToList().Select(prop => prop.GetValue(Obj));*/
        }

        private AppearanceSettings EmptyFieldToRed(AppearanceSettings Obj)
        {
            Obj.Background = Obj.Text == "ChangeToRed" ? Brushes.Red : Brushes.White;
            Obj.Foreground = Obj.Text == "ChangeToRed" ? Brushes.White : Brushes.Black;
            Obj.ReqField = AppearanceSettings.RequiredField.Yes;
            return Obj;
        }

        private AppearanceSettings SetLabelAppearance(AppearanceSettings Obj)
        {
            Obj.Background = Brushes.YellowGreen;
            Obj.FontSize = double.Parse("15");
            return Obj;
        }

        private AppearanceSettings SetStandardLabelAppearance1(AppearanceSettings Obj)
        {
            Obj.TextWrapping = TextWrapping.Wrap;
            Obj.Background = Brushes.Aqua;
            Obj.FontSize = double.Parse("15");

            return Obj;
        }

        private AppearanceSettings SetStandardTextBoxAppearance1(AppearanceSettings Obj)
        {
            Obj.TextWrapping = TextWrapping.Wrap;
            Obj.FontSize = double.Parse("10");
            return Obj;
        }

        private AppearanceSettings SetStandardComboBoxBoxAppearance1(AppearanceSettings Obj)
        {
            Obj.Background = Brushes.DimGray;
            Obj.FontSize = double.Parse("10");
            return Obj;
        }
    }
}

