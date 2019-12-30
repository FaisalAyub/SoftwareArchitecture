using Core.Data.Entities;
using Core.Data.Entities.Administration;
using Core.Data.Entities.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
   public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions options)
         : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<RecordType> RecordTypes { get; set;}
        public DbSet<Validation> Validations { get; set; }
        public DbSet<ValidationParameter> ValidationParameters { get; set; }
        public DbSet<ValidationError> validationErrors { get; set; }
    }
}
