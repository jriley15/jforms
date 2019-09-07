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
        public string Name { get; set; }

        //form type (post form, json body form)
        public FormType Type { get; set; }

        //form fields
        public ICollection<FormField> Fields { get; set; }

        //form submissions
        public ICollection<FormSubmission> Submissions { get; set; }

        //allowed origins
        public ICollection<FormOrigin> Origins { get; set; }

        //timestamps?


        public Form()
        {
            Submissions = new List<FormSubmission>();
            Fields = new List<FormField>();
            Origins = new List<FormOrigin>();
        }

    }
}
