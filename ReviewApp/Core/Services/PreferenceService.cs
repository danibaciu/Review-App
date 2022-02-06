using System;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Database.Context;
using Models.Request.Preference;
using Models.Response.Generic;

namespace Core.Services
{
    public class PreferenceService : IPreferenceService
    {
        private DatabaseContext _context;
        private IConfiguration _config;
        private ICoreResponseModel _response;
        private ICommonService _common;

        public PreferenceService(DatabaseContext _context, IConfiguration _config, ICoreResponseModel _response, ICommonService _common)
        {
            this._context = _context;
            this._config = _config;
            this._response = _response;
            this._common = _common;
        }

        public async Task<CoreResponseModel> updatePreference(PreferenceRequest request)
        {
            try
            {
                var profile = await _context.preferences.FirstOrDefaultAsync(x => x.userId == request.userId).ConfigureAwait(false);
                if (profile == null) return _response.getFailResponse("No preference found", null);

                profile.darkMode = request.darkMode;
                profile.gdpr_acceptance = request.gdpr_acceptance;

                await _context.SaveChangesAsync().ConfigureAwait(false);
                return _response.getSuccessResponse("Preferences Updated", profile);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }
    }

}
