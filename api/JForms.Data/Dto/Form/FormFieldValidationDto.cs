using JForms.Data.Local;
using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Dto.Form
{
    public class FormFieldValidationDto
    {

        public ValidationType Type { get; set; }

        public string Script { get; set; }

        public ICollection<FormFieldValidationRuleDto> Rules { get; set; }


    }
}
