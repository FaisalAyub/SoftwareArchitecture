﻿// <auto-generated />
using System;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Data.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Data.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Core.Data.Entities.Administration.RecordType", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentifierExpression")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PKColumn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RepeatableInd")
                        .HasColumnType("bit");

                    b.Property<string>("SectionID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("XPathRoot")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("RecordTypes");
                });

            modelBuilder.Entity("Core.Data.Entities.Administration.Validation", b =>
                {
                    b.Property<decimal>("ID")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AssemblyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssociatedTabID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComparisonFieldXPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ControlType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FocusControlID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FocusableInd")
                        .HasColumnType("bit");

                    b.Property<string>("FunctionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HighlightControlID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LookupID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("RangeFromValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RangeToValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RecordTypeID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegularExpression")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("SequenceNo")
                        .HasColumnType("tinyint");

                    b.Property<string>("Severity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpectrumEditID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<string>("XPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("XPathExpression")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Validations");
                });

            modelBuilder.Entity("Core.Data.Entities.Administration.ValidationParameter", b =>
                {
                    b.Property<decimal>("ValidationID")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("SequenceNo")
                        .HasColumnType("tinyint");

                    b.Property<decimal?>("ValidationID1")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("XPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ValidationID");

                    b.HasIndex("ValidationID1");

                    b.ToTable("ValidationParameters");
                });

            modelBuilder.Entity("Core.Data.Entities.Application.ValidationError", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationID")
                        .HasColumnType("int");

                    b.Property<string>("AssociatedTabID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ControlType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FocusControlID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FocusableInd")
                        .HasColumnType("bit");

                    b.Property<string>("GroupID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HighlightControlID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PKValue")
                        .HasColumnType("int");

                    b.Property<string>("RecordTypeID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RepeatableInd")
                        .HasColumnType("bit");

                    b.Property<string>("SectionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Severity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValidationID")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("validationErrors");
                });

            modelBuilder.Entity("Core.Data.Entities.Owner", b =>
                {
                    b.Property<int>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("OwnerId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Core.Data.Entities.Account", b =>
                {
                    b.HasOne("Core.Data.Entities.Owner", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Data.Entities.Administration.ValidationParameter", b =>
                {
                    b.HasOne("Core.Data.Entities.Administration.Validation", null)
                        .WithMany("ValidationParameters")
                        .HasForeignKey("ValidationID1");
                });
#pragma warning restore 612, 618
        }
    }
}
