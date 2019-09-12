using AutoMapper;
using JForms.Application.Helpers;
using JForms.Data;
using JForms.Data.Dto;
using JForms.Data.Dto.Form;
using JForms.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JForms.Application.Services
{

    public interface IFormService
    {


        Task<Response> Create(CreateFormDto form);

        Task<Response> Get(int formId);

        Task<Form> GetFormComplete(int formId);

        Task<Response> Search(SearchFormDto search);

        Task<Response> Update(CreateFormDto form);

        Task Delete(int formId);


    }

    public class FormService : IFormService
    {

        private readonly Data.DatabaseContext _dbContext;

        private readonly IMapper _mapper;

        public FormService(Data.DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<Response> Create(CreateFormDto form)
        {

            var formEntity = FormHelper.MapForm(form);//_mapper.Map<Form>(form);

            await _dbContext.Forms.AddAsync(formEntity);

            var affectedRows = await _dbContext.SaveChangesAsync();

            return new DataResponse<int>() { Data = formEntity.FormId, Success = true };
        }

        public async Task<Response> Get(int formId)
        {
            return new DataResponse<Form>() { Data = await GetFormComplete(formId), Success = true };
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
