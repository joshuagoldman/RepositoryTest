using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Methods
{
    public class GenerateActions
    {    
        public ChoiceActions ChoicAct { get; set; }

        public GenerateActions()
        {

        }

        public void PerformActions()
        {
            ChoicAct.ChangeToRedNotification();
            ChoicAct.RedNotificationPopUpMessage();

        }
    }
}
