using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JForms.API.Extensions;
using JForms.Data.Dto.Form;
using JForms.Application.Services;
using JForms.Data.Entity;

namespace JForms.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FormController : ControllerBase
    {

        private readonly IFormService _formService;

        private readonly IFormSnippetservice _formSnippetservice;

        public FormController(IFormService formService, IFormSnippetservice formSnippetservice)
        {
            _formService = formService;
            _formSnippetservice = formSnippetservice;
        }


        //create form here from UI for owners
        [HttpPost]
        public async Task<IActionResult> Create(CreateFormDto form)
        {
            return this.GenerateResponse(await _formService.Create(form));
        }

        //form data fetch from UI here for user submissions / owner editing
        [HttpGet]
        public async Task<Form> Get(int formId)
        {
            return await _formService.Get(formId);
        }


        //form data search here for owners to display in list on UI
        [HttpPost]
        public async Task<IActionResult> Search(SearchFormDto search)
        {
            return this.GenerateResponse(await _formService.Search(search));
        }


        //form data update here for owners
        [HttpPost]
        public async Task<IActionResult> Update(CreateFormDto form)
        {

            return this.GenerateResponse(await _formService.Update(form));

        }

        //form data delete here for owners
        [HttpPost]
        public async Task<IActionResult> Delete(int formId)
        {

            return Ok();

        }

        [HttpGet]
        public async Task<IEnumerable<FormSnippetDto>> GetSnippets(int formId)
        {
            return await _formSnippetservice.GetSnippets(formId);
        }

    }
}
