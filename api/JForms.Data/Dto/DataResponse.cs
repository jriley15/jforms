using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Dto
{
    public class DataResponse <T> : Response
    {

        public T Data { get; set; }

    }
}
