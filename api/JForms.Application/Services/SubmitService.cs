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
            formEntity.Fields.ToList().ForEach(field =>
            {
                //basic rules
                if (field.Validation.Type == ValidationType.Rules)
                {

                    var submittedValue = submission.GetValueOrDefault(field.Name);
                    var required = field.Validation.Rules.FirstOrDefault(r => r.FormFieldValidationRuleTypeId == (int)RuleType.Required) != null;

                    //universal rule
                    if (submittedValue == null && required)
                    {
                        response.AddError(field.Name, "Required");
                    }

                    if (submittedValue != null)
                    {
                        //specific rules based on field type
                        switch (field.FormFieldTypeId)
                        {

                            case (int)FieldType.Number:

                                bool isNumber = int.TryParse(submittedValue, out int parsedValue);
                                var minRule = field.Validation.Rules.FirstOrDefault(r => r.FormFieldValidationRuleTypeId == (int)RuleType.Minimum_Value);
                                var maxRule = field.Validation.Rules.FirstOrDefault(r => r.FormFieldValidationRuleTypeId == (int)RuleType.Maxmimum__Value);

                                if (!isNumber)
                                {
                                    response.AddError(field.Name, "Value must be a number");
                                }
                                else
                                {
                                    if (minRule != null && parsedValue < int.Parse(minRule.Constraint))
                                    {
                                        response.AddError(field.Name, "Value must be greater than or equal to " + minRule.Constraint);
                                    }
                                    else if (maxRule != null && parsedValue > int.Parse(maxRule.Constraint))
                                    {
                                        response.AddError(field.Name, "Value must be less than or equal to " + maxRule.Constraint);
                                    }
                                }

                                break;

                            case (int)FieldType.String:

                                var minLengthRule = field.Validation.Rules.FirstOrDefault(r => r.FormFieldValidationRuleTypeId == (int)RuleType.Minimum_Length);
                                var maxLengthRule = field.Validation.Rules.FirstOrDefault(r => r.FormFieldValidationRuleTypeId == (int)RuleType.Maxmimum_Length);


                                if (minLengthRule != null && submittedValue.Length < int.Parse(minLengthRule.Constraint))
                                {
                                    response.AddError(field.Name, "Value must be at least " + minLengthRule.Constraint + " characters long");
                                }
                                else if (maxLengthRule != null && submittedValue.Length > int.Parse(maxLengthRule.Constraint))
                                {
                                    response.AddError(field.Name, "Value must be less than or equal to " + maxLengthRule.Constraint + " characters long");
                                }


                                break;
                        }
                    }


                }
                //custom javascript validation
                else if (field.Validation.Type == ValidationType.CustomScript)
                {

                }

            });

            if (response.Errors.Count == 0)
            {
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
            }

            return response;

        }


    }
}
