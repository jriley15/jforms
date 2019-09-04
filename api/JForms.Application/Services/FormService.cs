using JForms.Data.Dto;
using JForms.Data.Dto.Form;
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

        public Task<Response> Create(CreateFormDto form)
        {
            throw new NotImplementedException();
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
