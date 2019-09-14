using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Dto.Form
{
    public class FormDto
    {

        public int FormId { get; set; }

        public string Name { get; set; }

        public int SubmissionCount { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
