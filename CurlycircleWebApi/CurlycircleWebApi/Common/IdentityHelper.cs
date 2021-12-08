using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CurlycircleWebApi.Common
{
  public class IdentityHelper : IIdentityHelper
  {
    private readonly IHttpContextAccessor httpContextAccessor;

    public IdentityHelper(IHttpContextAccessor httpContextAccessor)
    {
      this.httpContextAccessor = httpContextAccessor;
    }

    public int GetAuthenticatedUserId()
    {
      return int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub));
    }

    public IEnumerable<string> GetAuthenticatedUserRoles()
    {
      return httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role).Select(x => x.Value);
    }
  }
}
