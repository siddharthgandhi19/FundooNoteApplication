using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entity;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL iUserBL;
        FundooContext fundooContext;
        IDistributedCache distributedCache;
        public UserController(IUserBL iUserBL, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iUserBL = iUserBL;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }
        [HttpPost]

        [Route("UserRegistration")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = iUserBL.Registration(userRegistration);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("UserLogin")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var result = iUserBL.Login(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successfull", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Login UnSuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]

        public IActionResult PasswordForgot(string email)
        {
            try
            {
                var result = iUserBL.ForgotPassword(email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Mail Successfully Sent" }); //,data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Mail Not Successfully Sent" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize] //for token only code will read from here
        [HttpPut]
        [Route("ResetPassword")]

        public IActionResult PasswordReset(ResetPassword resetPassword) // email taken frm token
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iUserBL.ResetPassword(email,resetPassword);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Password Reset Successfully", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Password Not Reset Try Again" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllUsersUsingRedisCache()
        {
            var cacheKey = "userList";
            string serializedUserList;
            var userList = new List<UserEntity>();
            var redisUserList = await distributedCache.GetAsync(cacheKey);
            if (redisUserList != null)
            {
                serializedUserList = Encoding.UTF8.GetString(redisUserList);
                userList = JsonConvert.DeserializeObject<List<UserEntity>>(serializedUserList);
            }
            else
            {
                userList = await fundooContext.UserTable.ToListAsync();
                serializedUserList = JsonConvert.SerializeObject(userList);
                redisUserList = Encoding.UTF8.GetBytes(serializedUserList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisUserList, options);
            }
            return Ok(userList);
        }
    } 
}
