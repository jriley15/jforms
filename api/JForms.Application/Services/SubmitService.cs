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
using Microsoft.EntityFrameworkCore;

namespace JForms.Application.Services
{


    public interface ISubmitService {


        Task<Response> SubmitForm(int formId, Dictionary<string, string> submission);

    
    }



    public class SubmitService : ISubmitService
    {

        private readonly Data.DatabaseContext _dbContext;

        private readonly IMapper _mapper;

        public SubmitService(Data.DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response> SubmitForm(int formId, Dictionary<string, string> submission)
        {

            //get form we're creating a submission for
            var formEntity = await _dbContext.Forms.Include(f => f.Fields).SingleOrDefaultAsync(f => f.FormId == formId);


            //TODO: check origin from http headers with allowed origins from form

            //TODO: validate all fields here




            //create a new submission entity
            var submissionEntity = new FormSubmission();

            //add submission values to entity and map the fields
            formEntity.Fields.ToList().ForEach(field =>
            {

                submissionEntity.Values.Add(new FormSubmissionValue()
                {
                    Field = field,
                    Value = submission[field.Name]
                });
            });

            formEntity.Submissions.Add(submissionEntity);

            await _dbContext.SaveChangesAsync();



            var response = new Response()
            {
                Success = true
            };

            return response;

        }


    }
}
