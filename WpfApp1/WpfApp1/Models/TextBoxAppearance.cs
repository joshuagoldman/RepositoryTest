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

        ViewSettings search_key_viewsettings_object = new ViewSettings();
        ViewSettings input_date_with_index_viewsettings_object = new ViewSettings(); 
        ViewSettings criteria_reference_with_revision_viewsettings_object = new ViewSettings();
        ViewSettings responsible_viewsettings_object = new ViewSettings();
        ViewSettings reason_viewsettings_object = new ViewSettings();

        public ViewSettings SearchKey
        {
            get => search_key_viewsettings_object;
            set
            {
                SetAppearance(search_key_viewsettings_object);
                search_key_viewsettings_object = value;
                OnPropertyChanged("SearchKey");
            }
        }

        public ViewSettings InputDateWithIndexObject
        {
            get => input_date_with_index_viewsettings_object;
            set
            {
                SetAppearance(input_date_with_index_viewsettings_object);
                input_date_with_index_viewsettings_object = value;
                OnPropertyChanged("InputDateWithIndex");
            }
        }

        public ViewSettings CriteriaReferenceWithRevisionViewsettingsObject
        {
            get => criteria_reference_with_revision_viewsettings_object;
            set
            {
                SetAppearance(criteria_reference_with_revision_viewsettings_object);
                input_date_with_index_viewsettings_object = value;
                OnPropertyChanged("CriteriaReferenceWithRevisionViewsettingsObject");
            }
        }

        public ViewSettings ResponsibleViewsettingsObject
        {
            get => responsible_viewsettings_object;
            set
            {
                SetAppearance(responsible_viewsettings_object);
                responsible_viewsettings_object = value;
                OnPropertyChanged("ResponsibleViewsettingsObject");
            }
        }

        public ViewSettings ReasonViewsettingsObject
        {
            get => reason_viewsettings_object;
            set
            {
                SetAppearance(reason_viewsettings_object);
                reason_viewsettings_object = value;
                OnPropertyChanged("ReasonViewsettingsObject");
            }
        }

        public TextBoxAppearance()
        {
            GetType().GetProperties().ToList().ForEach(prop => prop.GetType().GetProperties().
            Select(subprop => subprop.Name.Equals("NameProp") ? 
            (string)subprop.GetValue((ViewSettings)prop.GetValue(this)) : null)
            .First().Equals(prop.Name));
        }

        private void SetAppearance(ViewSettings Obj)
        {
            var Props = Obj.GetType().GetProperties().ToList();
            if (Props.Any(prop => prop.Name.Equals("Text") && !string.IsNullOrEmpty((string)prop.GetValue(Obj))))
            {
                Props.ForEach(prop =>
                {
                    if (prop.Name.Equals("Background")) prop.SetValue(Obj, Brushes.White);
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

