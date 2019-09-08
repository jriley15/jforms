using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Entity
{
    public class FormFieldType
    {

        //primary key
        public int FormFieldTypeId { get; set; }

        //value type
        public string Value { get; set; }

        //validation rules that this type can have
        public ICollection<FormFieldTypeRuleType> FormFieldTypeRuleType { get; set; }


    }
}

