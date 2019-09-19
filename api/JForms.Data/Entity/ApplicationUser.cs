using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JForms.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public string AvatarUrl { get; set; }
    }
}
