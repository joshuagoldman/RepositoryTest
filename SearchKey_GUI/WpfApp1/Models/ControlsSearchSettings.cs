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
        AppearanceSettings exclude_test_types = new AppearanceSettings();
        AppearanceSettings serial_number = new AppearanceSettings();
        AppearanceSettings product = new AppearanceSettings();
        AppearanceSettings expression_button = new AppearanceSettings();        
        AppearanceSettings expression = new AppearanceSettings();
        AppearanceSettings variable = new AppearanceSettings();
        AppearanceSettings search_files_filter = new AppearanceSettings();
        AppearanceSettings search_path_option = new AppearanceSettings();
        AppearanceSettings regex_options = new AppearanceSettings();
        AppearanceSettings include_files = new AppearanceSettings();
        AppearanceSettings exclude_files = new AppearanceSettings();
        AppearanceSettings screening_allowed = new AppearanceSettings();
        AppearanceSettings inclded_in_test = new AppearanceSettings();

        public AppearanceSettings ExcludeTestTypes
        {
            get
            {
                exclude_test_types.ItemsSource = new string[] { "", "RcExtPrtt", "RcExtLat", "RCPrtt" };
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
                EmptyFieldToRed(product);
                return product;
            }
            set
            {
                product = value;
            }
        }

        public AppearanceSettings ExpressionButton
        {
            get
            {
                EmptyButtonWindowToRed(expression_button, new AppearanceSettings[] { Expression, Variable });
                return expression_button;
            }
            set
            {
                expression_button = value;
            }
        }

        public AppearanceSettings Expression
        {
            get
            {
                EmptyFieldToRed(expression);
                return expression;
            }
            set
            {
                expression = value;
            }
        }

        public AppearanceSettings Variable
        {
            get
            {
                EmptyFieldToRed(variable);
                return variable;
            }
            set
            {
                variable = value;
            }
        }

        public AppearanceSettings SearchFilesFilter
        {
            get
            {
                return search_files_filter;
            }
            set
            {
                search_files_filter = value;
            }
        }

        public AppearanceSettings SearchPathOption
        {
            get
            {
                search_path_option.ItemsSource = new string[] { "", "AllDirectories", "ALLDirectories" };
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
                regex_options.ItemsSource = new string[] { "YES","None" };
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
                screening_allowed.ItemsSource = new string[] { "YES", "NO" };
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
                inclded_in_test.ItemsSource = new string[] { "YES", "NO" };
                return inclded_in_test;
            }
            set
            {
                inclded_in_test = value;
            }
        }
    }
}

