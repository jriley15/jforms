using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Controllers
{
    public class FormController : BaseController
    {


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            

            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {

            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {

            return Ok();

        }






    }
}
