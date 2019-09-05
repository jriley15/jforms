using JForms.API.Attributes;
using JForms.Application.Services;
using JForms.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JForms.API.Extensions;
using System.Collections.Generic;

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


    [ApiController]
    [Route("[controller]")]
    public class SubmitController : ControllerBase
    {


        private readonly ISubmitService _submitService;

        public SubmitController(ISubmitService submitService)
        {
            _submitService = submitService;
        }

        [HttpPost]
        [FormContentType]
        [Route("{formId}")]
        public async Task<IActionResult> SubmitFromForm([FromRoute] int formId, [FromForm] Dictionary<string, string> form)
        {


            //check if submission is json body or form

            //call service method

            return this.GenerateResponse(await _submitService.SubmitForm(formId, form));

        }

        [HttpPost]
        [Route("{formId}")]
        public async Task<IActionResult> SubmitFromBody([FromRoute] int formId, [FromBody] Dictionary<string, string> body)
        {


            //check if submission is json body or form

            //call service method


            return this.GenerateResponse(await _submitService.SubmitForm(formId, body));

        }
    }
}
