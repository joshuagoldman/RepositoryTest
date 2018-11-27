using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;
using System.ComponentModel;

namespace WpfApp1.Models
{    
    public class Information : INotifyPropertyChanged 
    {
        TextBoxAppearance InfoViewSettings { get; set; }

        string[] input_date_with_index = { "InputDateWithIndex", "Value", "ChangeToRed" };
        string[] criteria_reference_with_revision = { "CriteriaReferenceWithRevision", "Value", "ChangeToRed" };
        string[] responsible_value = { "CriteriaReferenceWithRevision", "Value", "ChangeToRed" };
        string[] reason = { "CriteriaReferenceWithRevision", "Value", "ChangeToRed" };

        public string[] InputDateWithIndexValue
        {
            get => input_date_with_index;
            set
            {
                input_date_with_index = value;
                OnPropertyChanged("InputDateWithIndexValue");
            }
        }

        public string[] CriteriaReferenceWithRevisionValue
        {
            get => criteria_reference_with_revision;
            set
            {

                criteria_reference_with_revision = value;
                OnPropertyChanged("CriteriaReferenceWithRevisionValue");
            }
        }

        public string[] ResponsibleValue
        {
            get => responsible_value;
            set
            {
                responsible_value = value;
                OnPropertyChanged("ResponsibleValue");
            }
        }

        public string[] ReasonValue
        {
            get => reason;
            set
            {
                reason = value;
                OnPropertyChanged("ReasonValue");
            }
        }

        public Information()
        {

        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
