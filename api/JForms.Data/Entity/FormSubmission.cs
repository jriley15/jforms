using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Data.Entity
{
    public class FormSubmission
    {
        //primary key
        public int FormSubmissionId { get; set; }

        //foreign key to parent form
        public Form Form { get; set; }

        //boolean for validation pass
        public bool Success { get; set; }

        //errors?

        public DateTime CreatedOn { get; set; }

        //submission values
        public ICollection<FormSubmissionValue> Values { get; set; }

        public FormSubmission()
        {
            Values = new List<FormSubmissionValue>();
        }

    }
}
