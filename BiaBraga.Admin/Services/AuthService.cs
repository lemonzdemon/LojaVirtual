using BiaBraga.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BiaBraga.Admin.Services
{
    public class AuthService : AuthorizeAttribute
    {
        public AuthService(params Role[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
