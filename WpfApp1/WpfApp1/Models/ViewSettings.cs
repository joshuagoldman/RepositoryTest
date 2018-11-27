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
    public class ViewSettings : INotifyPropertyChanged
    {
        Brush background = Brushes.Blue;

        TextAlignment textalignment = System.Windows.TextAlignment.Center;

        double fontsize = double.Parse("20");

        TextWrapping textwrappping = System.Windows.TextWrapping.Wrap;

        Grid textblockgrid = new Grid();

        string text = "";

        Brush foreground = Brushes.Black;


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

        public ViewSettings()
        {
        }

        public Grid TextBlockGrid
        {
            get => textblockgrid;
            set
            {
                textblockgrid = value;
                OnPropertyChanged("TextBlockGrid");
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

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }
}
