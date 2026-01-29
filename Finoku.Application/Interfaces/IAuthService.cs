using Finoku.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Application.Interfaces
{
    public interface IAuthService
    {
        // JWT Token -> string dönüyor
        string Login(LoginRequestDto request);
    }
}
