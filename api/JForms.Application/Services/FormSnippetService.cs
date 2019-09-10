using JForms.Data.Dto.Form;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using JForms.Data.Entity;
using JForms.Data.Local;

namespace JForms.Application.Services
{

    public interface IFormSnippetservice
    {
        Task<IEnumerable<FormSnippetDto>> GetSnippets(int formId);
    }

    public class FormSnippetService : IFormSnippetservice
    {

        private readonly Data.DbContext _dbContext;

        public FormSnippetService(Data.DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FormSnippetDto>> GetSnippets(int formId)
        {

            //generate code snippets

            var form = await _dbContext.Forms.Include(f => f.Fields).ThenInclude(f => f.Validation).ThenInclude(v => v.Rules).SingleOrDefaultAsync(f => f.FormId == formId);


            //HTML form

            StringBuilder HTML = new StringBuilder();

            HTML.Append("<form>");

            foreach (FormField field in form.Fields)
            {
                switch (field.FormFieldType.FormFieldTypeId) {

                    case (int)FieldType.String:


                        break;

                }



            }

            HTML.Append("</form>");



            throw new NotImplementedException();
        }
    }
}
