using JForms.Data.Dto.Form;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using JForms.Data.Entity;
using JForms.Data.Local;
using JForms.Application.Extensions;
using JForms.Data.Dto;
using JForms.Data;

namespace JForms.Application.Services
{

    public interface IFormSnippetservice
    {
        Task<Response> GetSnippets(int formId);
    }

    public class FormSnippetService : IFormSnippetservice
    {

        private readonly DatabaseContext _dbContext;

        private readonly IFormService _formService;

        public FormSnippetService(DatabaseContext dbContext, IFormService formService)
        {
            _dbContext = dbContext;
            _formService = formService;
        }

        public async Task<Response> GetSnippets(int formId)
        {

            List<FormSnippetDto> Snippets = new List<FormSnippetDto>();

            //generate code snippets

            var form = await _formService.GetFormComplete(formId);

            //HTML form
            StringBuilder HTML = new StringBuilder();
            HTML.AppendLine("<form action='https://formsapi.jrdn.tech/Submit/" + form.FormId+"' method='post'>");
            foreach (FormField field in form.Fields)
            {
                HTML.AppendTab();
                HTML.AppendLine("<label for='"+field.Name+"'>"+char.ToUpper(field.Name[0])+field.Name.Substring(1, field.Name.Length - 1)+ "</label>");
                switch (field.FormFieldTypeId) {

                    case (int)FieldType.String:
                        HTML.AppendTab();
                        HTML.AppendLine("<input id='" + field.Name + "' type='text' name='" + field.Name+"' />");
                        break;

                    case (int)FieldType.Number:
                        HTML.AppendTab();
                        HTML.AppendLine("<input id='" + field.Name + "' type='number' name='" + field.Name + "' />");
                        break;

                    case (int)FieldType.Date:
                        HTML.AppendTab();
                        HTML.AppendLine("<input id='" + field.Name + "' type='date' name='" + field.Name + "' />");
                        break;

                    case (int)FieldType.RadioButton:
                        int radioIndex = 0;
                        foreach (FormFieldOption option in field.Options)
                        {
                            HTML.AppendTab();
                            HTML.AppendLine("<label for='" + field.Name + radioIndex + "'>"+ option.Value + "</label>");
                            HTML.AppendTab();
                            HTML.AppendLine("<input id='" + field.Name + radioIndex + "' type='radio' name='" + field.Name + "' value='"+ option.Value + "' />");
                            radioIndex++;
                        }
                        break;

                    case (int)FieldType.CheckBox:
                        int checkIndex = 0;
                        foreach (FormFieldOption option in field.Options)
                        {
                            HTML.AppendTab();
                            HTML.AppendLine("<label for='" + field.Name + checkIndex + "'>" + char.ToUpper(option.Value[0]) + option.Value.Substring(1, option.Value.Length - 1) + "</label>");
                            HTML.AppendTab();
                            HTML.AppendLine("<input id='" + field.Name + checkIndex + "' type='checkbox' name='" + field.Name + "-" + option.Value + "' value='" + option.Value + "' />");
                            checkIndex++;
                        }
                        break;
                    case (int)FieldType.DropDown:
                        HTML.AppendTab();
                        HTML.AppendLine("<select id='" + field.Name + "' name='" + field.Name+"'>");
                        int selectIndex = 0;
                        foreach (FormFieldOption option in field.Options)
                        {
                            HTML.AppendTab();
                            HTML.AppendTab();
                            HTML.AppendLine("<option value='"+ option.Value + "'>"+option.Value+"</option>");
                            selectIndex++;
                        }

                        HTML.AppendTab();
                        HTML.AppendLine("</select>");
                        break;
                }
            }
            HTML.AppendTab();
            HTML.AppendLine("<input type='submit' value='Submit' />");
            HTML.AppendLine("</form>");
            Snippets.Add(new FormSnippetDto() { Type = SnippetType.HTML, Code = HTML.ToString() });

            //Ajax
            StringBuilder Ajax = new StringBuilder();

            Ajax.AppendLine("var xhr = new XMLHttpRequest();");
            Ajax.AppendLine("xhr.onload = function () {");
            Ajax.AppendTab();
            Ajax.AppendLine("if (xhr.status >= 200 && xhr.status < 300) {");
            Ajax.AppendTab();
            Ajax.AppendTab();
            Ajax.AppendLine("// success");
            Ajax.AppendTab();
            Ajax.AppendLine("} else {");
            Ajax.AppendTab();
            Ajax.AppendTab();
            Ajax.AppendLine("// failed");
            Ajax.AppendTab();
            Ajax.AppendLine("}");
            Ajax.AppendLine("};");
            Ajax.AppendLine("xhr.open('POST', 'https://formsapi.jrdn.tech/Submit/" + form.FormId + "', true);");
            Ajax.AppendLine("xhr.setRequestHeader('Content-Type', 'application/json');");
            Ajax.AppendLine("xhr.send(JSON.stringify({");

            foreach (FormField field in form.Fields)
            {
                Ajax.AppendTab();
                Ajax.AppendLine(field.Name.Replace(" ", "") + ": ''," + (field.Options.Count > 0 ? " // options: (" + String.Join(", ", field.Options.Select(o => o.Value)) + ")" : " // Type: " + (FieldType)field.FormFieldTypeId));
            }

            Ajax.AppendLine("}));");

            Snippets.Add(new FormSnippetDto() { Type = SnippetType.Ajax, Code = Ajax.ToString() });


            //Fetch
            StringBuilder Fetch = new StringBuilder();

            Fetch.AppendLine("fetch('https://formsapi.jrdn.tech/Submit/"+form.FormId+"', {");
            Fetch.AppendTab();
            Fetch.AppendLine("method: 'POST',");
            Fetch.AppendTab();
            Fetch.AppendLine("body: JSON.stringify({");

            foreach (FormField field in form.Fields)
            {
                Fetch.AppendTab();
                Fetch.AppendTab();
                Fetch.AppendLine(field.Name.Replace(" ", "") + ": ''," + (field.Options.Count > 0 ? " // options: ("+String.Join(", ",field.Options.Select(o => o.Value))+")" : " // Type: " + (FieldType)field.FormFieldTypeId));
            }

            Fetch.AppendTab();
            Fetch.AppendLine("}),");
            Fetch.AppendTab();
            Fetch.AppendLine("headers:{");
            Fetch.AppendTab();
            Fetch.AppendTab();
            Fetch.AppendLine("'Content-Type': 'application/json'");
            Fetch.AppendTab();
            Fetch.AppendLine("}");
            Fetch.AppendLine("}).then(res => res.json())");
            Fetch.AppendLine(".then(response => console.log('Success:', JSON.stringify(response)))");
            Fetch.AppendLine(".catch(error => console.error('Error:', error));");

            Snippets.Add(new FormSnippetDto() { Type = SnippetType.Fetch, Code = Fetch.ToString() });


            return new DataResponse<IEnumerable<FormSnippetDto>>() { Data = Snippets, Success = true };
        }
    }
}
