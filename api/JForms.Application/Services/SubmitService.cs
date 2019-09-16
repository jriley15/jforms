using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JForms.Data;
using JForms.Data.Dto;
using JForms.Data.Entity;
using JForms.Data.Local;
using Microsoft.EntityFrameworkCore;

namespace JForms.Application.Services
{


    public interface ISubmitService
    {


        Task<Response> SubmitForm(int formId, Dictionary<string, string> submission);


    }



    public class SubmitService : ISubmitService
    {

        private readonly Data.DatabaseContext _dbContext;

        private readonly IMapper _mapper;

        private readonly IFormService _formService;

        public SubmitService(Data.DatabaseContext dbContext, IMapper mapper, IFormService formService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _formService = formService;
        }

        public async Task<Response> SubmitForm(int formId, Dictionary<string, string> submission)
        {
            var response = new Response();


            //get form we're creating a submission for
            var formEntity = await _formService.GetFormComplete(formId);


            //TODO: check origin from http headers with allowed origins from form

            //TODO: validate all fields here



            //create a new submission entity
            var submissionEntity = new FormSubmission()
            {
                CreatedOn = DateTime.UtcNow
            };

            //add submission values to entity and map the fields
            formEntity.Fields.ToList().ForEach(field =>
            {
                //check boxes can have multiple submission values for one field
                if (field.FormFieldType.FormFieldTypeId == (int)FieldType.CheckBox)
                {
                    foreach (FormFieldOption option in field.Options)
                    {
                        if (submission.ContainsKey(field.Name + "-" + option.Value))
                        {
                            submissionEntity.Values.Add(new FormSubmissionValue()
                            {
                                Field = field,
                                Value = option.Value
                            });
                        }
                    }
                }
                else if (submission.ContainsKey(field.Name))
                {
                    submissionEntity.Values.Add(new FormSubmissionValue()
                    {
                        Field = field,
                        Value = submission[field.Name]
                    });
                }
            });

            formEntity.Submissions.Add(submissionEntity);

            await _dbContext.SaveChangesAsync();

            response.Success = true;

            return response;

        }


    }
}
