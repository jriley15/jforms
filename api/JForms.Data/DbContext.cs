using JForms.Data.Entity;
using JForms.Data.Local;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public DbSet<Form> Forms { get; set; }

        public DbSet<User> Users { get; set; }


        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                modelBuilder.Entity<FormFieldType>().HasData(new FormFieldType { FormFieldTypeId = (int)fieldType, Value = fieldType.ToString() });
            }

            foreach (ValidationType fieldType in (ValidationType[])Enum.GetValues(typeof(ValidationType)))
            {
                modelBuilder.Entity<FormValidationRuleType>().HasData(new FormFieldType { FormFieldTypeId = (int)fieldType, Value = fieldType.ToString() });
            }


        }

    }
}
