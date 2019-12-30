using Core.Application.Services.Accounts;
using Core.Application.Services.Owners;
using Core.Application.Services.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        IValidationRepository Validation { get; }
        void Save();
    }
}
