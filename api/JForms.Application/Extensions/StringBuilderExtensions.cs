using System;
using System.Collections.Generic;
using System.Text;

namespace JForms.Application.Extensions
{
    public static class StringBuilderExtensions
    {


        public static void AppendTab(this StringBuilder stringBuilder)
        {
            stringBuilder.Append("    ");
        }

    }
}
