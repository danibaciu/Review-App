using System;
using System.Threading.Tasks;
using Models.Request.Profile;
using Models.Response.Generic;

namespace Core.Interface
{
    public interface IProfileService
    {
        Task<CoreResponseModel> updateProfile(ProfileRequest request);
    }
}
