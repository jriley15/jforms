using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Dto.Form
{
    public class SearchFormResponseDto : Response
    {

        public ICollection<FormDto> Data { get; set; }

    }
}
