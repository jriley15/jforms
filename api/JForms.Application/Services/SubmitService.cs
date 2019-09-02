using System;
using System.Collections.Generic;
using System.Text;
using JForms.Data.Dto;

namespace JForms.Application.Services
{


    public interface ISubmitService {


        Response SubmitForm(SubmitDto form);

    
    }



    public class SubmitService : ISubmitService
    {
        public Response SubmitForm(SubmitDto form)
        {
            var response = new Response()
            {
                Success = false
            };


            response.AddError("test", "invalid field");
            response.AddError("test 2", "invalid field");
            response.AddError("test 2", "invalid field");

            return response;

        }
    }
}
