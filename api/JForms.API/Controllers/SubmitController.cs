using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Controllers
{



    /*
     * 
     * 
     * Either configure routing to let form id's act as controller actions
     * 
     * or
     * 
     * have two seperate actions here - one for json body posts and one for form posts (send form id in model)
     * 
     * 
     * 
     */


    [Controller]
    [Route("[controller]")]
    public class SubmitController : Controller
    {

        [Route("{formId}")]
        public async Task<IActionResult> Submit([FromRoute] int formId, [FromBody] object body, [FromForm] object form)
        {
    


            return Ok();

        }


    }
}
