using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using CommonLayer.ModelClass;
using RepoLayer.Entity;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL iCollabBL;


        public CollabController(ICollabBL iCollabBL)
        {
            this.iCollabBL = iCollabBL;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateCollaboration")]

        public IActionResult CreateCollab(long NoteId, CollabEmail email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iCollabBL.CreateCollab(NoteId, email);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Collaboration Creation Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Collaboration Creation UnSuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }        
    }
}
