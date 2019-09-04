using JForms.API.Attributes;
using JForms.Application.Services;
using JForms.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JForms.API.Extensions;

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
        public async Task<IActionResult> SubmitFromForm([FromRoute] int formId, [FromForm] SubmitDto testForm)
        {


            //check if submission is json body or form

            //call service method

            return this.GenerateResponse(_submitService.SubmitForm(null));

        }

        [HttpPost]
        [Route("{formId}")]
        public async Task<IActionResult> SubmitFromBody([FromRoute] int formId, [FromBody] SubmitDto testBody)
        {


            //check if submission is json body or form

            //call service method


            return this.GenerateResponse(_submitService.SubmitForm(null));

        }
    }
}
