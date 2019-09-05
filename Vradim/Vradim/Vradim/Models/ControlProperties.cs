using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Vradim.Models
{
    public class ControlProperties : ContentPage
    {
        private ImageSource source;

        public ImageSource Source 
        {
            get { return source; }
            set
            {
                source = value;
                OnPropertyChanged("Source");
            }
        }

        private bool is_visible;
        public bool Visibillity
        {
            get { return is_visible; }
            set
            {
                is_visible = value;
                OnPropertyChanged("Visibillity");
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("text");
            }
        }

    }
}
