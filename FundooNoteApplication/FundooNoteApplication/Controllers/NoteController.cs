﻿using BusinessLayer.Interface;
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
        public IActionResult RemoveNotes(NoteRemove noteRemove, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.RemoveNotes(noteRemove, noteId);
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
        [HttpPut] //Entring the data in the database
        [Route("NoteTrash")]
        public IActionResult MoveToTrash(NoteTrashed noteTrashed)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.MoveToTrash(noteTrashed, userId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Note Trash Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Trash Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut] 
        [Route("NoteArchive")]
        public IActionResult MoveToArchive(NoteTrashed noteTrashed)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.MoveToArchive(noteTrashed, userId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Note Archive Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Archive Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}