using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL iUserBL;
        public UserController(IUserBL iUserBL)
        {
            this.iUserBL = iUserBL;
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
                    return this.Ok(new { success = true, message = "Mail Successfully Sent", data = result });
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

        public IActionResult PasswordReset(string newPassword, string confirmPassword) // email taken frm token
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iUserBL.ResetPassword(email, newPassword, confirmPassword);
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
    } 
}
