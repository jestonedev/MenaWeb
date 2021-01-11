using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.SecurityServices
{
    public class CookieUserWithConnectionStringRequirement: IAuthorizationRequirement
    {
    }
}
