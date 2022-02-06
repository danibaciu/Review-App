using System;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Database.Context;
using Models.Response.Generic;

namespace Core.Services
{
    public class RolesService : IRolesService
    {
        private DatabaseContext _context;
        private IConfiguration _config;
        private ICoreResponseModel _response;
        private ICommonService _common;

        public RolesService(DatabaseContext _context, IConfiguration _config, ICoreResponseModel _response, ICommonService _common)
        {
            this._context = _context;
            this._config = _config;
            this._response = _response;
            this._common = _common;
        }

        public async Task<CoreResponseModel> getRoles()
        {
            try
            {
                var roles = await _context.roles.ToListAsync().ConfigureAwait(false);
                return _response.getSuccessResponse("Success", roles);
            }
            catch(Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

    }
}
