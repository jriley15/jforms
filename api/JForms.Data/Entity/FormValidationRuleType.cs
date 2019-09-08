using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Entity
{
    public class FormValidationRuleType
    {

        //primary key
        public int FormValidationRuleTypeId { get; set; }

        //field types that this rule can be used for
        public ICollection<FormFieldTypeRuleType> FormFieldTypeRuleType { get; set; }


    }
}
