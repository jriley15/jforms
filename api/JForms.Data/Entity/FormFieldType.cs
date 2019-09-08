using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JForms.Data.Entity
{
    public class FormFieldType
    {

        //primary key
        public int FormFieldTypeId { get; set; }

        //name of type
        public string Name { get; set; }

        public bool MultipleOptions { get; set; }

        //validation rules that this type can have
        [JsonIgnore]
        public ICollection<FormFieldTypeRuleType> FormFieldTypeRuleType { get; set; }


    }
}

