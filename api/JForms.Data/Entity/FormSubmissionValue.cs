using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Data.Entity
{
    public class FormSubmissionValue
    {
        //primary key
        public int FormSubmissionValueId { get; set; }


        //foreign key to parent form submission
        public FormSubmission Submission { get; set; }


        //foreign key to relating field that this value belongs to
        public FormField Field { get; set; }


    }
}
