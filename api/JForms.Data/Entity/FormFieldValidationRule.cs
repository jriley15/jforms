using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Entity
{
    public class FormFieldValidationRule
    {
        //pk
        public int FormFieldValidationRuleId { get; set; }

        //rule type
        public int FormFieldValidationRuleTypeId { get; set; }
        public FormFieldValidationRuleType FormFieldValidationRuleType { get; set; }

        //rule constraint value
        public string Constraint { get; set; }

    }
}
