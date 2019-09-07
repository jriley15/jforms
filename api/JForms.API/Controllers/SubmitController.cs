using JForms.API.Attributes;
using JForms.Application.Services;
using JForms.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JForms.API.Extensions;
using System.Collections.Generic;

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
            return this.GenerateResponse(await _submitService.SubmitForm(formId, form));
        }

        [HttpPost]
        [Route("{formId}")]
        public async Task<IActionResult> SubmitFromBody([FromRoute] int formId, [FromBody] Dictionary<string, string> body)
        {
            return this.GenerateResponse(await _submitService.SubmitForm(formId, body));
        }
    }
}
