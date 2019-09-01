using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Controllers
{
    public class FormController : BaseController
    {


        //create form here from UI for owners
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return Ok();

        }

        //form data fetch from UI here for user submissions / owner editing
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            

            return Ok();

        }


        //form data search here for owners to display in list on UI
        [HttpGet]
        public async Task<IActionResult> Search()
        {

            return Ok();

        }


        //form data update here for owners
        [HttpGet]
        public async Task<IActionResult> Update()
        {

            return Ok();

        }

        //form data delete here for owners
        [HttpGet]
        public async Task<IActionResult> Delete()
        {

            return Ok();

        }






    }
}
