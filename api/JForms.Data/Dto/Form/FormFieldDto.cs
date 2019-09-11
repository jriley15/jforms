using JForms.Data.Local;
using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Dto.Form
{
    public class FormFieldDto
    {

        public int FormFieldId { get; set; }

        public string Name { get; set; }

        public FieldType Type { get; set; }

        public FormFieldValidationDto Validation { get; set; }

        public ICollection<FormFieldOptionDto> Options { get; set; }

    }
}
