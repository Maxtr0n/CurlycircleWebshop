using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Common
{
  public interface IIdentityHelper
  {
    int GetAuthenticatedUserId();

    IEnumerable<string> GetAuthenticatedUserRoles();
  }
}
