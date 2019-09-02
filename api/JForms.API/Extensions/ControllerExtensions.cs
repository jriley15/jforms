using JForms.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Extensions
{
    public static class ControllerExtensions
    {

        public static IActionResult GenerateResponse(this ControllerBase controllerBase, Response response)
        {
            if (response.Success)
            {
                return controllerBase.Ok(response);
            }

            return controllerBase.BadRequest(response);
        }
    

    
    }
}
