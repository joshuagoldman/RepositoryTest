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
    public class GridSettings: INotifyPropertyChanged
    {       
        public GridSettings()
        {
            var GridRowDict = new Dictionary<string, string>();
            var AllGridTypes = new string[] { "GenereteGrid", "DeleteGrid", "SearchKeyGrid", "InformationGrid", "SearchSettingsGrid", "ReportGrid", "TextBlockGrid", "TextBoxGrid" };
            for (int i = 0; i < AllGridTypes.Count(); i++)
            {
                GridRowDict.Add(AllGridTypes[i], (i + 1).ToString());
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
