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

    public interface IFormService {


        Task<Response> Create(CreateFormDto form);

        Task<Form> Get(int formId);

        Task<SearchFormResponseDto> Search(SearchFormDto search);

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
            var response = new CreateFormResponseDto();

            var formEntity = FormHelper.MapForm(form);//_mapper.Map<Form>(form);

            await _dbContext.Forms.AddAsync(formEntity);

            var affectedRows = await _dbContext.SaveChangesAsync();

            response.Success = true;
            response.FormId = formEntity.FormId;

            return response;
        }

        public async Task<Form> Get(int formId)
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

        public Task<SearchFormResponseDto> Search(SearchFormDto search)
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
    }
}
