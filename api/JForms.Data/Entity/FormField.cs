using JForms.Data.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Data.Entity
{
    public class FormField
    {

        //primary key
        public int FormFieldId { get; set; }

        //display name
        public string Name { get; set; }

        //field type

        public int FormFieldTypeId { get; set; }
        public FormFieldType FormFieldType { get; set; }

        //options for radio buttons, multi-selects, and drop-downs
        public ICollection<FormFieldOption> Options { get; set; }

        //foreign key to this fields validation 
        public FormFieldValidation Validation { get; set; }


    }
}
