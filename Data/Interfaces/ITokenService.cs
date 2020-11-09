using Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
