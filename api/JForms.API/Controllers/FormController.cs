using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JForms.API.Extensions;
using JForms.Data.Dto.Form;
using JForms.Application.Services;
using JForms.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using JForms.Application.Helpers;

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
        [Authorize]
        public async Task<IActionResult> Create(CreateFormDto form)
        {
            return this.GenerateResponse(await _formService.Create(form));
        }

        //form data fetch from UI here for user submissions / owner editing
        [HttpGet]
        public async Task<IActionResult> GetForSubmit(string formId)
        {
            return this.GenerateResponse(await _formService.GetForm(formId));
        }

        //form data fetch from UI here for user submissions / owner editing
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetForEdit(int formId)
        {
            return this.GenerateResponse(await _formService.GetForm(formId));
        }

        //gets shallow list of forms for current user to display on dashboard
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return this.GenerateResponse(await _formService.GetFormsForCurrentUser());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetWithSubmissions(int formId)
        {
            return this.GenerateResponse(await _formService.GetFormWithSubmissions(formId));
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
        public async Task<IActionResult> GetSnippets(int formId)
        {
            return this.GenerateResponse(await _formSnippetservice.GetSnippets(formId));
        }

    }
}
