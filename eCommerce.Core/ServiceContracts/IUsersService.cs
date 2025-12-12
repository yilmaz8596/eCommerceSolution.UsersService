using eCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.ServiceContracts
{
    public interface IUsersService
    {
        Task<AuthenticationResponse?> LoginUser(LoginRequest loginRequest);
        Task<AuthenticationResponse?> RegisterUser(RegisterRequest registerRequest);
    }
}
