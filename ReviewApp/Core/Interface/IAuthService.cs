using System;
using System.Threading.Tasks;
using Models.Request.Auth;
using Models.Response.Generic;

namespace Core.Interface
{
    public interface IAuthService
    {
        Task<CoreResponseModel> register(RegisterRequest register);
        Task<CoreResponseModel> login(LoginRequest login);
        Task<CoreResponseModel> updateUser(UpdateUserRequest update);
    }
}
