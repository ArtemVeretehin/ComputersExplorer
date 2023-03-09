using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ComputersExplorer;
using ComputersExplorer.Models;

namespace ComputersExplorer.CustomAuthenticationSchemes.GUID
{
    public interface IGUIDAuthenticationManager
    {
        string Authenticate(string UserRoleName, string username);

        IDictionary<string, Credential> Tokens { get; }
    }
}
