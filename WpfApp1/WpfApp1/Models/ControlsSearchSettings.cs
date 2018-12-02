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

        AppearanceSettings ExcludeTestTypes = new AppearanceSettings();
        AppearanceSettings SerialNumber = new AppearanceSettings();
        AppearanceSettings Product = new AppearanceSettings();
        AppearanceSettings input_date_with_index_textbox_object = new AppearanceSettings(); 
        AppearanceSettings criteria_reference_with_revision_textbox_object = new AppearanceSettings();
        AppearanceSettings responsible_textbox_object = new AppearanceSettings();
        AppearanceSettings reason_textbox_object = new AppearanceSettings();
        AppearanceSettings input_date_with_index_Label_object = new AppearanceSettings();
        AppearanceSettings criteria_reference_with_revision_Label_object = new AppearanceSettings();
        AppearanceSettings responsible_Label_object = new AppearanceSettings();
        AppearanceSettings reason_Label_object = new AppearanceSettings();

        public AppearanceSettings ExcludeTestTypes
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

        public AppearanceSettings SerialNumber
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
        
        public AppearanceSettings Product
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

        public AppearanceSettings Expression
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

        public AppearanceSettings SearchPathOption
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

        public AppearanceSettings RegexOptions
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

        public AppearanceSettings IncludeFiles
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


        public AppearanceSettings ExcludeFiles
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

        public AppearanceSettings ScreeningAllowed
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

        public AppearanceSettings IncludedInTest
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

        public AppearanceSettings ReasonLabelObject
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
    }
}

