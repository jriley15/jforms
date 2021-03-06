﻿using JForms.Data.Entity;
using JForms.Data.Local;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Form> Forms { get; set; }

        public DbSet<FormFieldType> FormFieldTypes { get; set; }

        public DbSet<FormFieldValidationRuleType> FormFieldValidationRuleTypes { get; set; }

        public DbSet<FormSubmission> FormSubmissions { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FormFieldTypeRuleType>()
                .HasKey(pt => new { pt.FormFieldTypeId, pt.FormValidationRuleTypeId });

            modelBuilder.Entity<FormFieldTypeRuleType>()
                .HasOne(pt => pt.FormFieldType)
                .WithMany(p => p.FormFieldTypeRuleType)
                .HasForeignKey(pt => pt.FormFieldTypeId);

            modelBuilder.Entity<FormFieldTypeRuleType>()
                .HasOne(pt => pt.FormValidationRuleType)
                .WithMany(t => t.FormFieldTypeRuleType)
                .HasForeignKey(pt => pt.FormValidationRuleTypeId);


            foreach (FieldType fieldType in (FieldType[])Enum.GetValues(typeof(FieldType)))
            {
                var Type = new FormFieldType { FormFieldTypeId = (int)fieldType, Name = fieldType.ToString() };
            
                //field types that have multiple options
                if (fieldType == FieldType.DropDown ||
                    fieldType == FieldType.RadioButton ||
                    fieldType == FieldType.CheckBox)
                {
                    Type.MultipleOptions = true;
                }

                modelBuilder.Entity<FormFieldType>().HasData(Type);

            }

            foreach (RuleType ruleType in (RuleType[])Enum.GetValues(typeof(RuleType)))
            {
                modelBuilder.Entity<FormFieldValidationRuleType>().HasData(new FormFieldValidationRuleType { FormFieldValidationRuleTypeId = (int)ruleType, Name = ruleType.ToString() });
            }

            //String validation types
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.String, FormValidationRuleTypeId = (int)RuleType.Required });
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.String, FormValidationRuleTypeId = (int)RuleType.Minimum_Length });
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.String, FormValidationRuleTypeId = (int)RuleType.Maxmimum_Length });

            //Number validation types
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.Number, FormValidationRuleTypeId = (int)RuleType.Required });
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.Number, FormValidationRuleTypeId = (int)RuleType.Minimum_Value });
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.Number, FormValidationRuleTypeId = (int)RuleType.Maxmimum__Value });

            //Date validation types
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.Date, FormValidationRuleTypeId = (int)RuleType.Required });
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.Date, FormValidationRuleTypeId = (int)RuleType.Minimum_Date });
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.Date, FormValidationRuleTypeId = (int)RuleType.Maxmimum_Date });

            //Radio button validation types
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.RadioButton, FormValidationRuleTypeId = (int)RuleType.Required });

            //Drop down validation types
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.DropDown, FormValidationRuleTypeId = (int)RuleType.Required });

            //Checkbox validation types
            modelBuilder.Entity<FormFieldTypeRuleType>().HasData(new FormFieldTypeRuleType { FormFieldTypeId = (int)FieldType.CheckBox, FormValidationRuleTypeId = (int)RuleType.Required });

        }

    }
}
