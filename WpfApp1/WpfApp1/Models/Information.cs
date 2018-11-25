using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;

namespace WpfApp1.Models
{    
    class Information
    {
        public string InputDateWithIndexValue { get; set; }

        public string CriteriaReferenceWithRevisionValue { get; set; }

        public string ResponsibleValue { get; set; }

        public string ReasonValue { get; set; }

        public Information(string inputdateWithindexValue = "ThrowMandatoryValueException",
                           string CriteriaReferenceWithRevisionValue = "ThrowMandatoryValueException",
                           string ResponsibleValue = "ThrowMandatoryValueException",
                           string reason = "ThrowMandatoryValueException")
        {
            string[] InputDateWithIndex = { "InputDateWithIndex", "Value", inputdateWithindexValue };
            string[] CriteriaReferenceWithRevision = { "CriteriaReferenceWithRevision", "Value", CriteriaReferenceWithRevisionValue };
            string[] Responsible = { "CriteriaReferenceWithRevision", "Value", ResponsibleValue };
            string[] Reason = { "CriteriaReferenceWithRevision", "Value", ReasonValue };
        }
    }
}
