using JForms.Data.Local;
using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Entity
{
    public class FormFieldValidation
    {
        //pk
        public int FormFieldValidationId { get; set; }

        //type of validation
        public ValidationType Type { get; set; }

        //custom js validation script
        public string Script { get; set; }

        //built in rules for validation
        public ICollection<FormValidationRule> Rules { get; set; }

    }
}
