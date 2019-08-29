using JForms.API.Data.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Data.Entity
{
    public class FormField
    {

        //primary key
        public int FormFieldId { get; set; }

        //foreign key to parent form
        public Form Form { get; set; }

        //display name
        public string Name { get; set; }

        //field type
        public FieldType Type { get; set; }



    }
}
