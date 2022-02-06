using System;
using System.Threading.Tasks;
using Models.Request.Preference;
using Models.Response.Generic;

namespace Core.Interface
{
    public interface IPreferenceService
    {
        Task<CoreResponseModel> updatePreference(PreferenceRequest request);
    }
}
