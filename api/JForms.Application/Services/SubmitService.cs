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

                    var required = field.Validation.Rules.FirstOrDefault(r => r.FormFieldValidationRuleTypeId == (int)RuleType.Required) != null;

                    if (field.FormFieldTypeId != (int)FieldType.CheckBox)
                    {

                        var submittedValue = submission.GetValueOrDefault(field.Name);

                        //universal rule
                        if ((submittedValue == null || String.IsNullOrWhiteSpace(submittedValue)) && required)
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

                                case (int)FieldType.Date:
                                    bool isDate = DateTime.TryParse(submittedValue, out DateTime parsedDate);
                                    var minDateRule = field.Validation.Rules.FirstOrDefault(r => r.FormFieldValidationRuleTypeId == (int)RuleType.Minimum_Date);
                                    var maxDateRule = field.Validation.Rules.FirstOrDefault(r => r.FormFieldValidationRuleTypeId == (int)RuleType.Maxmimum_Date);
                                    if (!isDate)
                                    {
                                        response.AddError(field.Name, "Value must be a date");
                                    }
                                    else
                                    {
                                        if (minDateRule != null && parsedDate < DateTime.Parse(minDateRule.Constraint))
                                        {
                                            response.AddError(field.Name, "Date must be after " + minDateRule.Constraint);
                                        }
                                        else if (maxDateRule != null && parsedDate > DateTime.Parse(maxDateRule.Constraint))
                                        {
                                            response.AddError(field.Name, "Date must be before " + maxDateRule.Constraint);
                                        }
                                    }
                                    break;

                                case (int)FieldType.RadioButton:
                                case (int)FieldType.DropDown:

                                    if (submission.ContainsKey(field.Name))
                                    {
                                        //make sure submitted value is one of the possible options provided
                                        var selectedValue = field.Options.FirstOrDefault(o => o.Value == submission[field.Name]);
                                        if (selectedValue == null)
                                        {
                                            response.AddError(field.Name, "Invalid value selected");
                                        }

                                    }

                                    break;
                            }
                        }
                    }
                    else
                    {
                        int selectedOptions = 0;

                        //check boxes have special naming conventions for values
                        foreach (FormFieldOption option in field.Options)
                        {
                            if (submission.ContainsKey(field.Name + "-" + option.Value))
                            {
                                selectedOptions++;
                            }
                        }

                        if (selectedOptions == 0 && required)
                        {
                            response.AddError(field.Name, "Required");
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
