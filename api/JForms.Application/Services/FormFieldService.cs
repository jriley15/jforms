using AutoMapper;
using JForms.Data;
using JForms.Data.Dto;
using JForms.Data.Dto.Form;
using JForms.Data.Entity;
using JForms.Data.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JForms.Application.Services
{

    public interface IFormFieldService
    {


        Task<Response> GetTypes();

        Task<Response> GetValidationTypes(FieldType fieldType);

    }

    public class FormFieldService : IFormFieldService
    {

        private readonly Data.DatabaseContext _dbContext;

        private readonly IMapper _mapper;

        public FormFieldService(Data.DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response> GetTypes()
        {
            return new DataResponse<IEnumerable<FormFieldType>>()
            {
                Data = await _dbContext.FormFieldTypes.ToListAsync(),
                Success = true
            };
        }

        public async Task<Response> GetValidationTypes(FieldType fieldType)
        {
            var field = (int)fieldType;
            return new DataResponse<IEnumerable<FormFieldValidationRuleType>>()
            {
                Data = await _dbContext.FormFieldValidationRuleTypes
                .Include(x => x.FormFieldTypeRuleType)
                .ThenInclude(x => x.FormFieldType)
                .Where(x => x.FormFieldTypeRuleType.Any(y => y.FormFieldTypeId == field)).ToListAsync(),
                Success = true
            };

        }
    }
}
