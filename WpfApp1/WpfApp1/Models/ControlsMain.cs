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
                SetAppearance(search_key_textbox_object);
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
                SetAppearance(searchgroup_Label_object);
                return searchgroup_Label_object;
            }
            set
            {
                searchgroup_Label_object = value;
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

        private AppearanceSettings SetAppearance(AppearanceSettings Obj)
        {
            Obj.Background = Obj.Text == "ChangeToRed" ? Brushes.Red : Brushes.White;
            Obj.Foreground = Obj.Text == "ChangeToRed" ? Brushes.White : Brushes.Black;
            return Obj;
        }

        private AppearanceSettings SetLabelAppearance(AppearanceSettings Obj)
        {
            Obj.Background = Brushes.YellowGreen;
            Obj.FontSize = double.Parse("15");
            return Obj;
        }
    }
}

