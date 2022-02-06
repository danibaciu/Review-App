using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Database.Context;
using System.Threading.Tasks;
using Models.Request.Auth;
using Models.Response.Generic;
using Models.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Response.Auth;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private DatabaseContext _context;
        private IConfiguration _config;
        private ICoreResponseModel _response;
        private ICommonService _common;

        public AuthService(DatabaseContext _context,IConfiguration _config, ICoreResponseModel _response,ICommonService _common)
        {
            this._context = _context;
            this._config = _config;
            this._response = _response;
            this._common = _common;
        }

        public async Task<CoreResponseModel> register(RegisterRequest register)
        {
            try
            {
                var checkMailUsernameResult = await _common.checkEmailExists(register.email);
                if (!checkMailUsernameResult.success) return checkMailUsernameResult;

                var userToCreate = new User()
                {
                    email = register.email,
                    password = _common.passwordHasher(register.password)
                };

                await _context.users.AddAsync(userToCreate).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                var profile = new Profile()
                {
                    displayName = register.displayName,
                    age = register.age,
                    userId = userToCreate.userId
                };

                var preference = new Preference()
                {
                    darkMode = register.darkMode,
                    gdpr_acceptance = register.gdpr_acceptance,
                    userId = userToCreate.userId
                };

                await _context.userRoles.AddAsync(new UserRole()
                {
                    roleId = register.roleId,
                    userId = userToCreate.userId
                }).ConfigureAwait(false);

                await _context.profiles.AddAsync(profile).ConfigureAwait(false);
                await _context.preferences.AddAsync(preference).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);


                var roles = await _context.roles.FirstOrDefaultAsync(x => x.roleId == register.roleId).ConfigureAwait(false);

                var loginResponse = new LoginResponseModel()
                {
                    userId = userToCreate.userId,
                    email = userToCreate.email,
                    profile = new Profile()
                    {
                        displayName = profile.displayName,
                        age = profile.age,
                        id=profile.id
                    },
                    preference=  new Preference()
                    {
                        id=preference.id,
                        darkMode = preference.darkMode,
                        gdpr_acceptance = preference.gdpr_acceptance,
                    },
                   
                    role = new Role()
                    {
                        roleId = roles.roleId,
                        roleName = roles.roleName
                    },
                    token = generateJWT(userToCreate.userId, roles.roleName),
                };

                return _response.getSuccessResponse("Registered successfully", loginResponse);

            }
            catch (Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }

        public async Task<CoreResponseModel> login(LoginRequest login)
        {
            try
            {
                var user = await (
                        from users in _context.users

                        join profile in _context.profiles
                        on users.userId equals profile.userId

                        join preference in _context.preferences
                        on users.userId equals preference.userId

                        join userRole in _context.userRoles
                        on users.userId equals userRole.userId

                        join roles in _context.roles
                        on userRole.roleId equals roles.roleId

                        where(users.email == login.email && users.password == _common.passwordHasher(login.password))
                        select new LoginResponseModel()
                        {
                            userId = users.userId,
                            email = users.email,
                            profile = new Profile()
                            {
                                age = profile.age,
                                displayName = profile.displayName,
                                id = profile.id
                            },
                            preference = new Preference()
                            {
                                darkMode = preference.darkMode,
                                gdpr_acceptance = preference.gdpr_acceptance,
                                id= preference.id
                            },
                            role = new Role()
                            {
                                roleId = roles.roleId,
                                roleName = roles.roleName
                            },
                            token = ""
                        })
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);

                if (user == null) return _response.getFailResponse("Incorrect credentials", null);

                user.token = generateJWT(user.userId, user.role.roleName);

                return _response.getSuccessResponse("Login successfully", user);

            }
            catch (Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }

        public async Task<CoreResponseModel> updateUser(UpdateUserRequest update)
        {
            try
            {
                var user = await _context.users.FirstOrDefaultAsync(x => x.userId == update.userId).ConfigureAwait(false);
                if (user == null) return _response.getFailResponse("No user exists", null);

                //var emailResponse = await _common.checkEmailExists(update.email).ConfigureAwait(false);
                //if (!emailResponse.success) return emailResponse;

                user.email = update.email;
                user.password = _common.passwordHasher(update.password);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                user.password = "";
                return _response.getSuccessResponse("User Updated", user);
            }
            catch(Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }


        private string generateJWT(long userId, string roleName)
        {
            try
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid,userId+""),
                    new Claim(ClaimTypes.Role,roleName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:secret"]));

                var token = new JwtSecurityToken(
                    issuer: _config["jwt:validIssuer"],
                    audience: _config["jwt:validAudience"],
                    expires: DateTime.Now.AddDays(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}
