﻿using System;
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

        AppearanceSettings reason_report = new AppearanceSettings();
        AppearanceSettings info_text_screening = new AppearanceSettings();
        AppearanceSettings info_text = new AppearanceSettings();
        AppearanceSettings info_text_extended = new AppearanceSettings(); 

        public AppearanceSettings ReasonReport
        {
            get
            {
                return reason_report;
            } 
            set
            {
                reason_report = value;
            }
        }

        public AppearanceSettings InfotextScreening
        {
            get
            {
                EmptyFieldToRed(info_text_screening);
                return info_text_screening;
            }
            set
            {
                info_text_screening = value;
            }
        }
        
        public AppearanceSettings Infotext
        {
            get
            {
                EmptyFieldToRed(info_text);
                return info_text;
            }
            set
            {
                info_text = value;
            }
        }

        public AppearanceSettings InfoTextExtended
        {
            get
            {
                EmptyFieldToRed(info_text_extended);
                return info_text_extended;
            } 
            set
            {
                info_text_extended = value;
            }
        }
    }
}
