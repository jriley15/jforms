using AutoMapper;
using JForms.Application.Helpers;
using JForms.Data;
using JForms.Data.Dto;
using JForms.Data.Dto.Form;
using JForms.Data.Entity;
using JForms.Data.Local;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JForms.Application.Services
{

    public interface IFormService
    {


        Task<Response> Create(CreateFormDto form);

        Task<Response> GetForm(int formId);

        Task<Response> GetForm(string formId);

        Task<Response> GetFormsForCurrentUser();

        Task<Form> GetFormComplete(int formId);

        Task<Response> GetFormWithSubmissions(int formId);

        Task<Response> Search(SearchFormDto search);

        Task<Response> Update(CreateFormDto form);

        Task Delete(int formId);


    }

    public class FormService : IFormService
    {

        private readonly Data.DatabaseContext _dbContext;

        private readonly IMapper _mapper;

        private readonly IAuthService _authService;

        public FormService(Data.DatabaseContext dbContext, IMapper mapper, IAuthService authService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<Response> Create(CreateFormDto form)
        {
            var response = new Response();

            var formEntity = FormHelper.MapForm(form);//_mapper.Map<Form>(form);

            if (String.IsNullOrWhiteSpace(formEntity.Name))
            {
                response.AddError("*", "Form name cannot be empty.");
            }
            if (formEntity.Fields.Count == 0)
            {
                response.AddError("*", "Forms need at least 1 field.");
            }

            //validate form fields
            foreach (var field in formEntity.Fields)
            {
                if (String.IsNullOrWhiteSpace(field.Name))
                {
                    response.AddError("*", "Field " + field.Name + " must have a name.");
                }
                if (!Enum.IsDefined(typeof(FieldType), field.FormFieldTypeId))
                {
                    response.AddError("*", "Field " + field.Name + " has an invalid field type.");
                }
                //validate options
                foreach (var option in field.Options)
                {
                    if (String.IsNullOrWhiteSpace(option.Value))
                    {
                        response.AddError("*", "Field " + field.Name + " has an empty option value.");
                    }
                }
                //validate basic rules
                if (field.Validation.Type == ValidationType.Rules)
                {
                    foreach (var rule in field.Validation.Rules)
                    {
                        if (!Enum.IsDefined(typeof(RuleType), rule.FormFieldValidationRuleTypeId))
                        {
                            response.AddError("*", "Field " + field.Name + " has an invalid rule type.");
                        }
                        if (String.IsNullOrWhiteSpace(rule.Constraint) && rule.FormFieldValidationRuleTypeId != (int)RuleType.Required)
                        {
                            response.AddError("*", "Field " + field.Name + " has an empty rule constraint value.");
                        }
                    }
                }
                else if (field.Validation.Type == ValidationType.CustomScript)
                {
                    //custom js validation 
                }
                else
                {
                    response.AddError("*", "Field " + field.Name + " has an invalid validation type.");
                }
            }

            if (response.Errors.Count == 0)
            {
                var user = await _authService.GetCurrentUser();

                formEntity.User = user;

                await _dbContext.Forms.AddAsync(formEntity);

                var affectedRows = await _dbContext.SaveChangesAsync();

                return new DataResponse<int>() { Data = formEntity.FormId, Success = true };
            }
            return response;
        }

        public async Task<Response> GetForm(int formId)
        {
            var form = await GetFormComplete(formId);

            form.EncodedFormId = CustomEncoder.encode(formId);

            return new DataResponse<Form>() { Data = form, Success = true };
        }

        public async Task<Response> GetForm(string formId)
        {
            return new DataResponse<Form>() { Data = await GetFormComplete(CustomEncoder.decode(formId)), Success = true };
        }

        public async Task<Response> GetFormsForCurrentUser()
        {

            var formEntities = await _dbContext.Forms
                .Where(f => f.User.Id == _authService.GetCurrentUserId())
                .Include(f => f.Submissions).ToListAsync();

            var formDtos = new List<FormDto>();

            foreach (Form form in formEntities)
            {
                formDtos.Add(new FormDto()
                {
                    Name = form.Name,
                    FormId = form.FormId,
                    SubmissionCount = form.Submissions.Count,
                    CreatedOn = form.CreatedOn
                });
            }

            return new DataResponse<IEnumerable<FormDto>>()
            {
                Data = formDtos,
                Success = true
            };
        }
        public async Task<Response> GetFormWithSubmissions(int formId)
        {
            return new DataResponse<Form>()
            {
                Data = (await _dbContext.Forms.Include(f => f.Submissions)
                .ThenInclude(f => f.Values)
                .Include(f => f.Fields)
                .SingleOrDefaultAsync(f => f.FormId == formId)),
                Success = true
            };
        }

        public Task<Response> Search(SearchFormDto search)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(CreateFormDto form)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int formId)
        {
            throw new NotImplementedException();
        }

        //keep this here until repositories added
        public async Task<Form> GetFormComplete(int formId)
        {
            return await _dbContext.Forms.Include(f => f.Fields).
                ThenInclude(f => f.Validation).
                ThenInclude(v => v.Rules)
            .Include(f => f.Fields)
                .ThenInclude(f => f.Options)
            .Include(f => f.Fields)
                .ThenInclude(f => f.FormFieldType)
            .SingleOrDefaultAsync(f => f.FormId == formId);
        }

    }
}
