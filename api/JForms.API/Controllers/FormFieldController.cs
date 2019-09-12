using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JForms.API.Extensions;
using JForms.Data.Dto.Form;
using JForms.Application.Services;
using JForms.Data.Entity;
using JForms.Data.Local;

namespace JForms.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FormFieldController : ControllerBase
    {

        private readonly IFormFieldService _formFieldService;

        public FormFieldController(IFormFieldService formFieldService)
        {
            _formFieldService = formFieldService;
        }



        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            return this.GenerateResponse(await _formFieldService.GetTypes());
        }

        [HttpGet]
        public async Task<IActionResult> GetValidationTypes(FieldType fieldType)
        {
            return  this.GenerateResponse(await _formFieldService.GetValidationTypes(fieldType));
        }

    }
}
