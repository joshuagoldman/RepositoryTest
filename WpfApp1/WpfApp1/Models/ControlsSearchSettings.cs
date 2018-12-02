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

        AppearanceSettings exclude_test_types = new AppearanceSettings();
        AppearanceSettings serial_number = new AppearanceSettings();
        AppearanceSettings product = new AppearanceSettings();
        AppearanceSettings expression = new AppearanceSettings(); 
        AppearanceSettings search_path_option = new AppearanceSettings();
        AppearanceSettings regex_options = new AppearanceSettings();
        AppearanceSettings include_files = new AppearanceSettings();
        AppearanceSettings exclude_files = new AppearanceSettings();
        AppearanceSettings screening_allowed = new AppearanceSettings();
        AppearanceSettings included_in_test = new AppearanceSettings();

        public AppearanceSettings ExcludeTestTypes
        {
            get
            {
                SetStandardComboBoxBoxAppearance1(exclude_test_types);
                return exclude_test_types;
            } 
            set
            {
                exclude_test_types = value;
            }
        }

        public AppearanceSettings SerialNumber
        {
            get
            {
                SetStandardTextBoxAppearance1(serial_number);
                return serial_number;
            }
            set
            {
                serial_number = value;
            }
        }
        
        public AppearanceSettings Product
        {
            get
            {
                SetStandardTextBoxAppearance1(Product);
                EmptyFieldToRed(product);
                return Product;
            }
            set
            {
                product = value;
            }
        }

        public AppearanceSettings Expression
        {
            get
            {
                SetStandardTextBoxAppearance1(expression);
                EmptyFieldToRed(expression);
                return expression;
            } 
            set
            {
                expression = value;
            }
        }

        public AppearanceSettings SearchPathOption
        {
            get
            {
                SetStandardComboBoxBoxAppearance1(search_path_option);
                return search_path_option;
            } 
            set
            {
                search_path_option = value;
            }
        }

        public AppearanceSettings RegexOptions
        {
            get
            {
                SetStandardComboBoxBoxAppearance1(regex_options);
                EmptyFieldToRed(regex_options);
                return regex_options;
            }
            set
            {
                regex_options = value;
            }
        }

        public AppearanceSettings IncludeFiles
        {
            get
            {
                SetStandardComboBoxBoxAppearance1(include_files);
                EmptyFieldToRed(include_files);
                return include_files;
            } 
            set
            {
                include_files = value;
            }
        }


        public AppearanceSettings ExcludeFiles
        {
            get
            {
                SetStandardComboBoxBoxAppearance1(exclude_files);
                EmptyFieldToRed(exclude_files);                
                return exclude_files;
            } 
            set
            {
                exclude_files = value;
            }
        }

        public AppearanceSettings ScreeningAllowed
        {
            get
            {
                SetStandardComboBoxBoxAppearance1(screening_allowed);
                EmptyFieldToRed(screening_allowed);
                return screening_allowed;
            } 
            set
            {
                screening_allowed = value;
            }
        }

        public AppearanceSettings IncludedInTest
        {
            get
            {
                SetStandardComboBoxBoxAppearance1(included_in_test);
                return included_in_test;
            }
            set
            {
                included_in_test = value;
            }
        }
    }
}

