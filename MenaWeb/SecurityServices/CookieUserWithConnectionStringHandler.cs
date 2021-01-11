using Microsoft.AspNetCore.Authorization;
using MySql.Data.MySqlClient;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MenaWeb.SecurityServices
{
    public class CookieUserWithConnectionStringHandler : AuthorizationHandler<CookieUserWithConnectionStringRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CookieUserWithConnectionStringRequirement requirement)
        {
            if (!(context.User.Identity is WindowsIdentity))
            {
                var connString = context.User.FindFirstValue("connString");
                try
                {
                    var connPersonal = new MySqlConnection(connString);
                    connPersonal.Open();
                    connPersonal.Close();
                    context.Succeed(requirement);
                }
                catch (Exception)
                {

                }
            }
            return Task.CompletedTask;
        }
    }
}
