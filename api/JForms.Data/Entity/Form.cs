using JForms.Data.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Data.Entity
{
    public class Form
    {
        //primary key
        public int FormId { get; set; }

        //user id
        public User User { get; set; }

        //name
        public int Name { get; set; }

        //form type (post form, json body form)
        public FormType Type { get; set; }

        //form fields
        public ICollection<FormField> Fields { get; set; }

        //form submissions
        public ICollection<FormSubmission> Submissions { get; set; }

        //dates?

    }
}
