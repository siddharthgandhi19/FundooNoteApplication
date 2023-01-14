using CommonLayer.ModelClass;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRL : IUserRL
    {
        FundooContext fundooContext;
        private readonly string _secret;
        private readonly string _expDate;
        public UserRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;

        }

        public UserEntity Registration(UserRegistration userRegistation)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistation.FirstName;
                userEntity.LastName = userRegistation.LastName;
                userEntity.Email = userRegistation.Email;
                userEntity.Password = userRegistation.Password;
                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Login(UserLogin userLogin)
        {
            try
            {
                var result = fundooContext.UserTable.Where(x => x.Email == userLogin.Email && x.Password == userLogin.Password).FirstOrDefault();
                if (result != null)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateSecurityToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string ForgotPassword(string email)
        {
            try
            {
                var result = fundooContext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                if (result != null)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public bool ResetPassword (string email, ResetPassword resetPassword)
        {
            try
            {
                if (resetPassword.NewPassword == resetPassword.ConfirmPassword)
                {
                    var result = fundooContext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                    result.Password= resetPassword.NewPassword;
                    fundooContext.SaveChanges(); // saving in database
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
