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

        AppearanceSettings input_date_with_index = new AppearanceSettings(); 
        AppearanceSettings criteria_reference_with_revision = new AppearanceSettings();
        AppearanceSettings responsible = new AppearanceSettings();
        AppearanceSettings reason = new AppearanceSettings();        

        public AppearanceSettings InputDateWithIndex
        {
            get
            {
                EmptyFieldToRed(input_date_with_index);                
                return input_date_with_index;
            } 
            set
            {
                input_date_with_index = value;
            }
        }

        public AppearanceSettings CriteriaReferenceWithRevision
        {
            get
            {
                criteria_reference_with_revision.ItemsSource =  
                    GetHWLogCriteriaElementVals("CriteriaReferenceWithRevision").Distinct().ToArray();
                EmptyFieldToRed(criteria_reference_with_revision);
                return criteria_reference_with_revision;
            } 
            set
            {
                criteria_reference_with_revision = value;
            }
        }

        public AppearanceSettings Responsible
        {
            get
            {
                EmptyFieldToRed(responsible);
                return responsible;
            }
            set
            {
                responsible = value;
            }
        }

        public AppearanceSettings Reason
        {
            get
            {
                return reason;
            } 
            set
            {
                reason = value;
            }
        }
    }
}

