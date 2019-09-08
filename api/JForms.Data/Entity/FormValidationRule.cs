using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Entity
{
    public class FormValidationRule
    {
        //pk
        public int FormValidationRuleId { get; set; }

        //rule type
        public FormValidationRuleType Type { get; set; }

        //rule constraint value
        public string Constraint { get; set; }

    }
}
