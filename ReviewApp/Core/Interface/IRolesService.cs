using System;
using System.Threading.Tasks;
using Models.Response.Generic;

namespace Core.Interface
{
    public interface IRolesService
    {
        Task<CoreResponseModel> getRoles();
    }
}
