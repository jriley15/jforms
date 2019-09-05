using AutoMapper;
using JForms.Data;
using JForms.Data.Dto;
using JForms.Data.Dto.Form;
using JForms.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JForms.Application.Services
{

    public interface IFormService {


        Task<Response> Create(CreateFormDto form);

        Task<GetFormResponseDto> Get(int formId);

        Task<SearchFormResponseDto> Search(SearchFormDto search);

        Task<Response> Update(CreateFormDto form);

        Task Delete(int formId);

    
    }

    public class FormService : IFormService
    {

        private readonly DbContext _dbContext;

        private readonly IMapper _mapper;

        public FormService(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<Response> Create(CreateFormDto form)
        {
            var response = new Response();

            var formEntity = _mapper.Map<Form>(form);

            await _dbContext.Forms.AddAsync(formEntity);

            var affectedRows = await _dbContext.SaveChangesAsync();

            response.Success = true;

            return response;
        }

        public Task<GetFormResponseDto> Get(int formId)
        {
            throw new NotImplementedException();
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
