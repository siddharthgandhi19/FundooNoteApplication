using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBL iNoteBL;
        public NoteController(INoteBL iNoteBL)
        {
            this.iNoteBL = iNoteBL;
        }
        [Authorize]
        [HttpPost] //Entring the data in the database
        [Route("NoteRegistration")]
        public IActionResult CreateNote(NoteRegistration noteRegistration)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.CreateNote(noteRegistration, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Create Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Create Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("RemoveNote")]
        public IActionResult RemoveNotes(NoteIDModel noteIdModel, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.RemoveNotes(noteIdModel, noteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Deleted Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Delete Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}