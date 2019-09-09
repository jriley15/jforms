using JForms.Data.Dto.Form;
using JForms.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JForms.Application.Helpers
{
    public class FormHelper
    {

        public static Form MapForm(CreateFormDto formDto)
        {
            Form MappedForm = new Form()
            {
                Name = formDto.Name,
                Type = formDto.Type,
                Fields = formDto.Fields.Select(field => new FormField() {
                    Name = field.Name,
                    FormFieldTypeId = (int)field.Type,
                     Validation = new FormFieldValidation()
                     {
                          Script = field.Validation.Script,
                          Type = field.Validation.Type,
                           Rules = field.Validation.Rules.Select(rule => new FormFieldValidationRule() { 
                                Constraint = rule.Value,
                                FormFieldValidationRuleTypeId = (int)rule.Type 
                           }).ToList()

                     }
                }).ToList() 
            };



            return MappedForm;
        }


    }
}
