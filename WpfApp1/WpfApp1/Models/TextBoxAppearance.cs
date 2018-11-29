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


    public class TextBoxAppearance : INotifyPropertyChanged
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
            get => textblock_object;
            set
            {
                criteria_reference_with_revision_Label_object.FontSize = double.Parse("30");
                textblock_object = value;
                OnPropertyChanged("TextBlockObject");
            }
        }

        public ViewSettings SearchKey
        {
            get => search_key_textbox_object;
            set
            {
                SetAppearance(search_key_textbox_object);
                search_key_textbox_object = value;
                OnPropertyChanged("SearchKey");
            }
        }
        
        public ViewSettings SearchGroup
        {
            get => searchgroup_Label_object;
            set
            {
                SetAppearance(searchgroup_Label_object);
                searchgroup_Label_object = value;
                OnPropertyChanged("SearchGroup");
            }
        }

        public ViewSettings InputDateWithIndexTextBoxObject
        {
            get => input_date_with_index_textbox_object;
            set
            {
                SetAppearance(input_date_with_index_textbox_object);
                input_date_with_index_textbox_object = value;
                OnPropertyChanged("InputDateWithIndexTextBoxObject");
            }
        }

        public ViewSettings CriteriaReferenceWithRevisionTextBoxObject
        {
            get => criteria_reference_with_revision_textbox_object;
            set
            {
                SetAppearance(criteria_reference_with_revision_textbox_object);
                input_date_with_index_textbox_object = value;
                OnPropertyChanged("CriteriaReferenceWithRevisionTextBoxObject");
            }
        }

        public ViewSettings ResponsibleTextBoxsObject
        {
            get => responsible_textbox_object;
            set
            {
                SetAppearance(responsible_textbox_object);
                responsible_textbox_object = value;
                OnPropertyChanged("ResponsibleTextBoxsObject");
            }
        }

        public ViewSettings ReasonTextBoxObject
        {
            get => reason_textbox_object;
            set
            {
                SetAppearance(reason_textbox_object);
                reason_textbox_object = value;
                OnPropertyChanged("ReasonTextBoxObject");
            }
        }


        public ViewSettings InputDateWithIndexLabelObject
        {
            get => input_date_with_index_Label_object;
            set
            {
                criteria_reference_with_revision_Label_object.Background = Brushes.Beige;
                criteria_reference_with_revision_Label_object.FontSize = double.Parse("30");
                input_date_with_index_Label_object = value;
                OnPropertyChanged("InputDateWithIndexLabelObject");
            }
        }

        public ViewSettings CriteriaReferenceWithRevisionLabelObject
        {
            get => criteria_reference_with_revision_Label_object;
            set
            {
                criteria_reference_with_revision_Label_object.Background = Brushes.Blue;
                criteria_reference_with_revision_Label_object = value;
                OnPropertyChanged("CriteriaReferenceWithRevisionLabelObject");
            }
        }

        public ViewSettings ResponsibleLabelObject
        {
            get => responsible_Label_object;
            set
            {
                criteria_reference_with_revision_Label_object.Background = Brushes.BurlyWood;
                search_key_textbox_object = value;
                OnPropertyChanged("ResponsibleLabelObject");
            }
        }

        public ViewSettings ReasonLabelObject
        {
            get => reason_Label_object;
            set
            {
                criteria_reference_with_revision_Label_object.Background = Brushes.YellowGreen;
                reason_Label_object = value;
                OnPropertyChanged("ReasonLabelObject");
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
        }

        private void SetAppearance(ViewSettings Obj)
        {
            var Props = Obj.GetType().GetProperties().ToList();
            if (Props.Any(prop => prop.Name.Equals("Text") && (string)prop.GetValue(Obj) != "ChangeToRed"))
            {
                Props.ForEach(prop =>
                {
                    if (prop.Name.Equals("Background")) prop.SetValue(Obj, Brushes.Black);
                    if (prop.Name.Equals("TextColor")) prop.SetValue(Obj, Brushes.Black);
                });
            }
            else
            {
                Props.ForEach(prop =>
                {
                    if (prop.Name.Equals("Background")) prop.SetValue(Obj, Brushes.Red);
                    if (prop.Name.Equals("TextColor")) prop.SetValue(Obj, Brushes.White);
                });
            }
        } 

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

