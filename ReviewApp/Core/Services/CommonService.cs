using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Database.Context;
using Models.Response.Generic;

namespace Core.Services
{
    public class CommonService : ICommonService
    {
        private DatabaseContext _context;
        private ICoreResponseModel _response;


        public CommonService(DatabaseContext _context, IConfiguration _config, ICoreResponseModel _response)
        {
            this._context = _context;
            this._response = _response;

        }
        public string passwordHasher(string password)
        {
            try
            {
                string hashedPassword = "";
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }

                return hashedPassword;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<CoreResponseModel> checkEmailExists(string email)
        {
            try
            {
                var user = await _context.users.FirstOrDefaultAsync(x => x.email == email).ConfigureAwait(false);               

                return user == null ? _response.getSuccessResponse("success", null) : _response.getFailResponse("Email already exists", null);

            }
            catch (Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }

        
    }
}
