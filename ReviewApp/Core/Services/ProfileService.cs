using System;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Database.Context;
using Models.Request.Profile;
using Models.Response.Generic;

namespace Core.Services
{
    public class ProfileService : IProfileService
    {
        private DatabaseContext _context;
        private IConfiguration _config;
        private ICoreResponseModel _response;
        private ICommonService _common;

        public ProfileService(DatabaseContext _context, IConfiguration _config, ICoreResponseModel _response, ICommonService _common)
        {
            this._context = _context;
            this._config = _config;
            this._response = _response;
            this._common = _common;
        }

        public async Task<CoreResponseModel> updateProfile(ProfileRequest request)
        {
            try
            {
                var profile = await _context.profiles.FirstOrDefaultAsync(x => x.userId == request.userId).ConfigureAwait(false);
                if(profile == null) return _response.getFailResponse("No profile found", null);

                profile.displayName = request.displayName;
                profile.age = request.age;

                await _context.SaveChangesAsync().ConfigureAwait(false);
                return _response.getSuccessResponse("Profile Updated", profile);
            }
            catch(Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }
    }
}
