using JForms.API.Attributes;
using JForms.Application.Services;
using JForms.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JForms.API.Extensions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JForms.API.Controllers
{

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

            //return to previous url with success / error??
            //maybe show a generic view with a success / return button

            var response = await _submitService.SubmitForm(formId, form);



            return Redirect("http://localhost:3000/form/submit/" + formId + "?response=" + JsonConvert.SerializeObject(new DataResponse<string>() { Success = response.Success, Errors = response.Errors, Data = Request.Headers["Referer"].ToString() }));
            //this.GenerateResponse(await _submitService.SubmitForm(formId, form));
        }

        [HttpPost]
        [Route("{formId}")]
        public async Task<IActionResult> SubmitFromBody([FromRoute] int formId, [FromBody] Dictionary<string, string> body)
        {
            return this.GenerateResponse(await _submitService.SubmitForm(formId, body));
        }

    }
}
