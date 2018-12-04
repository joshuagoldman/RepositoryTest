using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Models
{
    public class AppearanceSettings : INotifyPropertyChanged
    {

        public enum RequiredField { Yes, No }

        Brush background = Brushes.White;

        TextAlignment textalignment = System.Windows.TextAlignment.Center;

        double fontsize = double.Parse("15");

        TextWrapping textwrappping = System.Windows.TextWrapping.Wrap;

        string text = "";

        string name = "";

        Brush foreground = Brushes.Blue;

        RequiredField req_field = RequiredField.No;

        string[] items = {"", "", ""};

        Visibility visibility = Visibility.Visible;

        public Brush Background
        {
            get => background;
            set  
            {
                background = value;
                OnPropertyChanged("Background");                
            }
        }

        public TextAlignment TextAlignment
        {
            get => textalignment;
            set
            {
                textalignment = value;
                OnPropertyChanged("TextAlignment");                
            }
        }

        public double FontSize
        {
            get => fontsize;
            set
            {
                fontsize = value;
                OnPropertyChanged("FontSize");
            }
        }

        public TextWrapping TextWrapping
        {
            get => textwrappping;
            set
            {
                textwrappping = value;
                OnPropertyChanged("TextWrapping");
            }
        }

        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public Brush Foreground
        {
            get => foreground;
            set
            {
                foreground = value;
                OnPropertyChanged("Foreground");
            }
        }

       public string NameProp
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("NameProp");
            }
        }

        public RequiredField ReqField
        {
            get => req_field;
            set
            {
                req_field = value;
                OnPropertyChanged("ReqField");
            }
        }

        public string[] ItemsSource
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public AppearanceSettings()
        {
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }
}
