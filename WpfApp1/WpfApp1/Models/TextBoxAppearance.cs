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


    public class TextBoxAppearance
    {

        public Dictionary<string, ViewSettings> AppearanceDict { get; set; }

        public ViewSettings Obj { get; set; }

        ViewSettings textblock_object = new ViewSettings();
        ViewSettings search_key_textbox_object = new ViewSettings();
        ViewSettings searchgroup_Label_object = new ViewSettings();
        ViewSettings input_date_with_index_textbox_object = new ViewSettings(); 
        ViewSettings criteria_reference_with_revision_textbox_object = new ViewSettings();
        ViewSettings responsible_textbox_object = new ViewSettings();
        ViewSettings reason_textbox_object = new ViewSettings();
        ViewSettings input_date_with_index_Label_object = new ViewSettings();
        ViewSettings criteria_reference_with_revision_Label_object = new ViewSettings();
        ViewSettings responsible_Label_object = new ViewSettings();
        ViewSettings reason_Label_object = new ViewSettings();

        public ViewSettings TextBlockObject
        {
            get
            {
                textblock_object.FontSize = double.Parse("30");
                return textblock_object;
            } 
            set
            {
                textblock_object = value;
            }
        }

        public ViewSettings SearchKey
        {
            get
            {
                SetAppearance(search_key_textbox_object);
                search_key_textbox_object.FontSize = double.Parse("15");
                return search_key_textbox_object;
            }
            set
            {
                search_key_textbox_object = value;
            }
        }
        
        public ViewSettings SearchGroup
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

        public ViewSettings InputDateWithIndexTextBoxObject
        {
            get
            {
                SetAppearance(input_date_with_index_textbox_object);
                return input_date_with_index_textbox_object;
            } 
            set
            {
                input_date_with_index_textbox_object = value;
            }
        }

        public ViewSettings CriteriaReferenceWithRevisionTextBoxObject
        {
            get
            {
                SetAppearance(criteria_reference_with_revision_textbox_object);
                return criteria_reference_with_revision_textbox_object;
            } 
            set
            {
                criteria_reference_with_revision_textbox_object = value;
            }
        }

        public ViewSettings ResponsibleTextBoxObject
        {
            get
            {
                SetAppearance(responsible_textbox_object);
                return responsible_textbox_object;
            }
            set
            {
                responsible_textbox_object = value;
            }
        }

        public ViewSettings ReasonTextBoxObject
        {
            get
            {
                SetAppearance(reason_textbox_object);
                return reason_textbox_object;
            } 
            set
            {
                reason_textbox_object = value;
            }
        }


        public ViewSettings InputDateWithIndexLabelObject
        {
            get
            {
                SetLabelAppearance(input_date_with_index_Label_object);                
                return input_date_with_index_Label_object;
            } 
            set
            {
                input_date_with_index_Label_object = value;
            }
        }

        public ViewSettings CriteriaReferenceWithRevisionLabelObject
        {
            get
            {
                SetLabelAppearance(criteria_reference_with_revision_Label_object);
                return criteria_reference_with_revision_Label_object;
            } 
            set
            {
                criteria_reference_with_revision_Label_object = value;
            }
        }

        public ViewSettings ResponsibleLabelObject
        {
            get
            {
                SetLabelAppearance(responsible_Label_object);
                return responsible_Label_object;
            }
            set
            {
                responsible_Label_object = value;
            }
        }

        public ViewSettings ReasonLabelObject
        {
            get
            {
                SetLabelAppearance(reason_Label_object);
                return reason_Label_object;
            }
            set
            {
                reason_Label_object = value;
            }
        }

        public TextBoxAppearance()
        {
            GetType().GetProperties().ToList().Where(prop => (ViewSettings)prop.GetValue(this) != null).ToList().
                ForEach(prop => prop.GetValue(this).GetType().GetProperties().
                Where(subprop => subprop.GetValue(prop.GetValue(this)) != null).
                Where(subprop => subprop.Name.Equals("NameProp")).ToList().
                ForEach(subprop => subprop.SetValue(prop.GetValue(this), prop.Name)));


            /* var slagerak = GetType().GetProperties().ToList().Where(prop => (ViewSettings)prop.GetValue(this) != null).ToList().
                 Select(prop => prop.GetValue(this).GetType().GetProperties().
                 Where(subprop => subprop.GetValue(prop.GetValue(this)) != null).
                 Where(subprop => subprop.Name.Equals("Background")).ToList().
                 Select(subprop => subprop.GetValue(prop.GetValue(this))));*/

            /*Obj.GetType().GetProperties().ToList().Select(prop => prop.GetValue(Obj));*/
        }

        private ViewSettings SetAppearance(ViewSettings Obj)
        {
            Obj.Background = Obj.Text == "ChangeToRed" ? Brushes.Red : Brushes.White;
            Obj.Foreground = Obj.Text == "ChangeToRed" ? Brushes.White : Brushes.Black;
            return Obj;
        }

        private ViewSettings SetLabelAppearance(ViewSettings Obj)
        {
            Obj.Background = Brushes.YellowGreen;
            Obj.FontSize = double.Parse("15");
            return Obj;
        }
    }
}

