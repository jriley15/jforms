using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Attributes
{
    public class FormContentTypeAttribute : Attribute, IActionConstraint
    {
        public int Order => 0;

        public bool Accept(ActionConstraintContext ctx) =>
            ctx.RouteContext.HttpContext.Request.HasFormContentType;
    }
}
