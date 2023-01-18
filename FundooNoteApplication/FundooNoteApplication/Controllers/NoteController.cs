using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepoLayer.Context;

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

        [Authorize]
        [HttpPut]
        [Route("NoteUpdate")]
        public IActionResult UpdateNotes(NoteRegistration noteRegistration, long NoteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.UpdateNotes(noteRegistration, userId, NoteID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Update Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveNote")]
        public IActionResult RetrieveNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.RetrieveNotes(userId, noteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Retrieve Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Retrieve Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveAllNote")]
        public IActionResult RetrieveAllNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.RetrieveAllNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Retrieve Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Retrieve Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ArchieveNote")]
        public IActionResult ArchieveNotes(NoteIDModel noteIDModel)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.ArchieveNotes(noteIDModel, UserId);
                string message = (result == 1) ? "Note Archieve" : "Note UnArchieve";
                if (result > 0)
                {
                    return this.Ok(new { success = true, message = message, data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Archieve Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("PinNote")]
        public IActionResult PinnedNotes(NoteIDModel noteIDModel)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.PinnedNotes(noteIDModel, UserId);
                string message = (result == 1) ? "Note Pin" : "Note UnPin";
                if (result > 0)
                {
                    return this.Ok(new { success = true, message = message, data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Archieve Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("TrashNote")]
        public IActionResult TrashedNotes(NoteIDModel noteIDModel)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.TrashedNotes(noteIDModel, UserId);
                string message = (result == 1) ? "Note Trash" : "Note UnTrash";
                if (result > 0)
                {
                    return this.Ok(new { success = true, message = message, data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Archieve Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("DeleteAllTrashNote")]
        public IActionResult TrashedAllNotes(long UserId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.TrashedAllNotes(UserId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "All Trash Note Delete Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Delete Trash Note Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("BackgroundColor")]
        public IActionResult BgColor(long NoteID, string backgroundColor, NoteColor noteColor)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.Color(userId, NoteID, backgroundColor, noteColor);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Background Color Done", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Background Color Not Done" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ImageUpload")]

        public IActionResult UploadImage(IFormFile image, long NoteID)
        {
            try
            {           
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                var result = iNoteBL.UploadImage(image, NoteID, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Image Uploaded Succesfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Image Uploaded Unsuccessfully" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}