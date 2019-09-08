using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Entity
{
    public class FormFieldTypeRuleType
    {
        public int FormValidationRuleTypeId { get; set; }

        public FormValidationRuleType FormValidationRuleType { get; set; }

        public int FormFieldTypeId { get; set; }

        public FormFieldType FormFieldType { get; set; }

    }
}
