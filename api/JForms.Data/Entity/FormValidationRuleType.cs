﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JForms.Data.Entity
{
    public class FormValidationRuleType
    {

        //primary key
        public int FormValidationRuleTypeId { get; set; }

        //Name of rule type
        public string Name { get; set; }

        [JsonIgnore]
        //field types that this rule can be used for
        public ICollection<FormFieldTypeRuleType> FormFieldTypeRuleType { get; set; }


    }
}
