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


        Task<IEnumerable<FormFieldType>> GetTypes();

        Task<IEnumerable<FormValidationRuleType>> GetValidationTypes(FieldType fieldType);

    }

    public class FormFieldService : IFormFieldService
    {

        private readonly Data.DbContext _dbContext;

        private readonly IMapper _mapper;

        public FormFieldService(Data.DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FormFieldType>> GetTypes()
        {
            return await _dbContext.FormFieldTypes.ToListAsync();
        }

        public async Task<IEnumerable<FormValidationRuleType>> GetValidationTypes(FieldType fieldType)
        {
            var field = (int)fieldType;
            return await _dbContext.FormValidationRuleTypes
                .Include(x => x.FormFieldTypeRuleType)
                .ThenInclude(x => x.FormFieldType)
                .Where(x => x.FormFieldTypeRuleType.Any(y => y.FormFieldTypeId == field)).ToListAsync();

        }
    }
}
