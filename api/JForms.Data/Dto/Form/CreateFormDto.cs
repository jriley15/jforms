using JForms.Data.Local;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Data.Dto.Form
{
    public class CreateFormDto
    {

        public int FormId { get; set; }

        [Required]
        public string Name { get; set; }

        public FormType Type { get; set; }

        public ICollection<FormFieldDto> Fields { get; set; }




    }
}
