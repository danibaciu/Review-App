using System;
using System.Threading.Tasks;
using Models.Response.Generic;

namespace Core.Interface
{
    public interface ICommonService
    {
        string passwordHasher(string password);
        Task<CoreResponseModel> checkEmailExists(string email);
    }
}
