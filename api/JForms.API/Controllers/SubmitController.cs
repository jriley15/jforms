using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubmitController : ControllerBase
    {


        //Form submit action
        [HttpPost]
        public async Task<IActionResult> SubmitForm()
        {

            return Ok();

        }


    }
}
