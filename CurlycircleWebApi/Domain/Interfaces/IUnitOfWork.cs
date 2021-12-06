using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Interfaces
{
  public interface IUnitOfWork
  {
    Task SaveChangesAsync();

    Task<IDbContextTransaction> BeginTransactionAsync();
  }
}
