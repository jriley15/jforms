﻿// <auto-generated />
using System;
using JForms.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JForms.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0-preview9.19423.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("JForms.Data.Entity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("JForms.Data.Entity.Form", b =>
                {
                    b.Property<int>("FormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("FormId");

                    b.HasIndex("UserId");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormField", b =>
                {
                    b.Property<int>("FormFieldId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FormFieldTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("FormId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("ValidationFormFieldValidationId")
                        .HasColumnType("integer");

                    b.HasKey("FormFieldId");

                    b.HasIndex("FormFieldTypeId");

                    b.HasIndex("FormId");

                    b.HasIndex("ValidationFormFieldValidationId");

                    b.ToTable("FormField");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldOption", b =>
                {
                    b.Property<int>("FormFieldOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("FormFieldId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("FormFieldOptionId");

                    b.HasIndex("FormFieldId");

                    b.ToTable("FormFieldOption");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldType", b =>
                {
                    b.Property<int>("FormFieldTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("MultipleOptions")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("FormFieldTypeId");

                    b.ToTable("FormFieldTypes");

                    b.HasData(
                        new
                        {
                            FormFieldTypeId = 1,
                            MultipleOptions = false,
                            Name = "String"
                        },
                        new
                        {
                            FormFieldTypeId = 2,
                            MultipleOptions = false,
                            Name = "Number"
                        },
                        new
                        {
                            FormFieldTypeId = 3,
                            MultipleOptions = false,
                            Name = "Date"
                        },
                        new
                        {
                            FormFieldTypeId = 4,
                            MultipleOptions = true,
                            Name = "RadioButton"
                        },
                        new
                        {
                            FormFieldTypeId = 5,
                            MultipleOptions = true,
                            Name = "DropDown"
                        },
                        new
                        {
                            FormFieldTypeId = 6,
                            MultipleOptions = true,
                            Name = "CheckBox"
                        });
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldTypeRuleType", b =>
                {
                    b.Property<int>("FormFieldTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("FormValidationRuleTypeId")
                        .HasColumnType("integer");

                    b.HasKey("FormFieldTypeId", "FormValidationRuleTypeId");

                    b.HasIndex("FormValidationRuleTypeId");

                    b.ToTable("FormFieldTypeRuleType");

                    b.HasData(
                        new
                        {
                            FormFieldTypeId = 1,
                            FormValidationRuleTypeId = 1
                        },
                        new
                        {
                            FormFieldTypeId = 1,
                            FormValidationRuleTypeId = 4
                        },
                        new
                        {
                            FormFieldTypeId = 1,
                            FormValidationRuleTypeId = 5
                        },
                        new
                        {
                            FormFieldTypeId = 2,
                            FormValidationRuleTypeId = 1
                        },
                        new
                        {
                            FormFieldTypeId = 2,
                            FormValidationRuleTypeId = 2
                        },
                        new
                        {
                            FormFieldTypeId = 2,
                            FormValidationRuleTypeId = 3
                        });
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldValidation", b =>
                {
                    b.Property<int>("FormFieldValidationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Script")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("FormFieldValidationId");

                    b.ToTable("FormFieldValidation");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldValidationRule", b =>
                {
                    b.Property<int>("FormFieldValidationRuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Constraint")
                        .HasColumnType("text");

                    b.Property<int?>("FormFieldValidationId")
                        .HasColumnType("integer");

                    b.Property<int>("FormFieldValidationRuleTypeId")
                        .HasColumnType("integer");

                    b.HasKey("FormFieldValidationRuleId");

                    b.HasIndex("FormFieldValidationId");

                    b.HasIndex("FormFieldValidationRuleTypeId");

                    b.ToTable("FormFieldValidationRule");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldValidationRuleType", b =>
                {
                    b.Property<int>("FormFieldValidationRuleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("FormFieldValidationRuleTypeId");

                    b.ToTable("FormFieldValidationRuleTypes");

                    b.HasData(
                        new
                        {
                            FormFieldValidationRuleTypeId = 1,
                            Name = "Required"
                        },
                        new
                        {
                            FormFieldValidationRuleTypeId = 2,
                            Name = "Minimum_Value"
                        },
                        new
                        {
                            FormFieldValidationRuleTypeId = 3,
                            Name = "Maxmimum__Value"
                        },
                        new
                        {
                            FormFieldValidationRuleTypeId = 4,
                            Name = "Minimum_Length"
                        },
                        new
                        {
                            FormFieldValidationRuleTypeId = 5,
                            Name = "Maxmimum_Length"
                        },
                        new
                        {
                            FormFieldValidationRuleTypeId = 6,
                            Name = "Minimum_Date"
                        },
                        new
                        {
                            FormFieldValidationRuleTypeId = 7,
                            Name = "Maxmimum_Date"
                        },
                        new
                        {
                            FormFieldValidationRuleTypeId = 8,
                            Name = "Regex"
                        });
                });

            modelBuilder.Entity("JForms.Data.Entity.FormOrigin", b =>
                {
                    b.Property<int>("FormOriginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("FormId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("FormOriginId");

                    b.HasIndex("FormId");

                    b.ToTable("FormOrigin");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormSubmission", b =>
                {
                    b.Property<int>("FormSubmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("FormId")
                        .HasColumnType("integer");

                    b.Property<bool>("Success")
                        .HasColumnType("boolean");

                    b.HasKey("FormSubmissionId");

                    b.HasIndex("FormId");

                    b.ToTable("FormSubmission");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormSubmissionValue", b =>
                {
                    b.Property<int>("FormSubmissionValueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("FieldFormFieldId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubmissionFormSubmissionId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("FormSubmissionValueId");

                    b.HasIndex("FieldFormFieldId");

                    b.HasIndex("SubmissionFormSubmissionId");

                    b.ToTable("FormSubmissionValue");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("JForms.Data.Entity.Form", b =>
                {
                    b.HasOne("JForms.Data.Entity.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormField", b =>
                {
                    b.HasOne("JForms.Data.Entity.FormFieldType", "FormFieldType")
                        .WithMany()
                        .HasForeignKey("FormFieldTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JForms.Data.Entity.Form", null)
                        .WithMany("Fields")
                        .HasForeignKey("FormId");

                    b.HasOne("JForms.Data.Entity.FormFieldValidation", "Validation")
                        .WithMany()
                        .HasForeignKey("ValidationFormFieldValidationId");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldOption", b =>
                {
                    b.HasOne("JForms.Data.Entity.FormField", null)
                        .WithMany("Options")
                        .HasForeignKey("FormFieldId");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldTypeRuleType", b =>
                {
                    b.HasOne("JForms.Data.Entity.FormFieldType", "FormFieldType")
                        .WithMany("FormFieldTypeRuleType")
                        .HasForeignKey("FormFieldTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JForms.Data.Entity.FormFieldValidationRuleType", "FormValidationRuleType")
                        .WithMany("FormFieldTypeRuleType")
                        .HasForeignKey("FormValidationRuleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JForms.Data.Entity.FormFieldValidationRule", b =>
                {
                    b.HasOne("JForms.Data.Entity.FormFieldValidation", null)
                        .WithMany("Rules")
                        .HasForeignKey("FormFieldValidationId");

                    b.HasOne("JForms.Data.Entity.FormFieldValidationRuleType", "FormFieldValidationRuleType")
                        .WithMany()
                        .HasForeignKey("FormFieldValidationRuleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JForms.Data.Entity.FormOrigin", b =>
                {
                    b.HasOne("JForms.Data.Entity.Form", null)
                        .WithMany("Origins")
                        .HasForeignKey("FormId");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormSubmission", b =>
                {
                    b.HasOne("JForms.Data.Entity.Form", "Form")
                        .WithMany("Submissions")
                        .HasForeignKey("FormId");
                });

            modelBuilder.Entity("JForms.Data.Entity.FormSubmissionValue", b =>
                {
                    b.HasOne("JForms.Data.Entity.FormField", "Field")
                        .WithMany()
                        .HasForeignKey("FieldFormFieldId");

                    b.HasOne("JForms.Data.Entity.FormSubmission", "Submission")
                        .WithMany("Values")
                        .HasForeignKey("SubmissionFormSubmissionId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("JForms.Data.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("JForms.Data.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JForms.Data.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("JForms.Data.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
