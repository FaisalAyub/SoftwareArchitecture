using Core.Data;
using Core.Data.Entities.Administration;
using Core.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Services.RecordTypes
{
  public  class RecordTypeRepository : RepositoryBase<RecordType>, IRecordTypeRepository
    {
        public RecordTypeRepository(RepositoryContext repositoryContext)
                                              : base(repositoryContext)
        {

        }
    }
}
