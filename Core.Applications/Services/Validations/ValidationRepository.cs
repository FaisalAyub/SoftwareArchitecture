using Core.Data;
using Core.Data.Entities.Administration;
using Core.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Application.Services.Validations
{
   public class ValidationRepository : RepositoryBase<Validation>, IValidationRepository
    {
        public ValidationRepository(RepositoryContext repositoryContext)
                                              : base(repositoryContext)
        {

        }

        public IEnumerable<Validation> GetValidations()
        {
            return GetAll().Include(x=>x.ValidationParameters).Where(t => t.Status).ToList();
        }

       
    }
}
