using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Data.Dto.Auth
{
    public class RegisterDto
    {
        public string Email { get; set; }

        //public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

    }
}
