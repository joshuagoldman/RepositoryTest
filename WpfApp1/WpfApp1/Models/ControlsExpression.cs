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
        public enum IsVariable { Yes, No }

        AppearanceSettings variable_list = new AppearanceSettings();
        AppearanceSettings variable_one = new AppearanceSettings();
        AppearanceSettings variable_two = new AppearanceSettings();
        AppearanceSettings variable_three = new AppearanceSettings();
        AppearanceSettings variable_four = new AppearanceSettings();
        AppearanceSettings variable_five = new AppearanceSettings();
        AppearanceSettings variable_six = new AppearanceSettings();
        AppearanceSettings variable_seven = new AppearanceSettings();
        AppearanceSettings variable_eight = new AppearanceSettings();
        AppearanceSettings variable_nine = new AppearanceSettings();
        AppearanceSettings variable_ten = new AppearanceSettings();

        public AppearanceSettings VariableList
        {
            get
            {
                variable_list.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
                return variable_list;
            }
            set
            {
                variable_list = value;
            }
        }

        public AppearanceSettings VariableOne
        {
            get
            {
                EmptyFieldToRed(variable_one);
                variable_one.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_one;
            }
            set
            {
                variable_one = value;
            }
        }

        public AppearanceSettings VariableTwo
        {
            get
            {
                EmptyFieldToRed(variable_two);
                variable_two.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_two;
            }
            set
            {
                variable_two = value;
            }
        }

        public AppearanceSettings VariableThree
        {
            get
            {
                EmptyFieldToRed(variable_three);
                variable_three.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_three;
            }
            set
            {
                variable_three = value;
            }
        }

        public AppearanceSettings VariableFour
        {
            get
            {
                EmptyFieldToRed(variable_four);
                variable_four.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_four;
            }
            set
            {
                variable_four = value;
            }
        }

        public AppearanceSettings VariableFive
        {
            get
            {
                EmptyFieldToRed(variable_five);
                variable_five.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_five;
            }
            set
            {
                variable_five= value;
            }
        }

        public AppearanceSettings VariableSex
        {
            get
            {
                EmptyFieldToRed(variable_six);
                variable_six.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_six;
            }
            set
            {
                variable_six = value;
            }
        }

        public AppearanceSettings VariableSeven
        {
            get
            {
                EmptyFieldToRed(variable_seven);
                variable_seven.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_seven;
            }
            set
            {
                variable_seven = value;
            }
        }

        public AppearanceSettings VariableEight
        {
            get
            {
                EmptyFieldToRed(variable_eight);
                variable_eight.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_eight;
            }
            set
            {
                variable_eight = value;
            }
        }

        public AppearanceSettings VariableNine
        {
            get
            {
                EmptyFieldToRed(variable_nine);
                variable_nine.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_nine;
            }
            set
            {
                variable_nine = value;
            }
        }

        public AppearanceSettings VariableTen
        {
            get
            {
                EmptyFieldToRed(variable_ten);
                variable_ten.IsVar = AppearanceSettings.IsVariable.Yes;
                return variable_ten;
            }
            set
            {
                variable_ten = value;
            }
        }
    }
}
