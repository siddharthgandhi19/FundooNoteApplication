using BusinessLayer.Interface;
using CommonLayer.ModelClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepoLayer.Context;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepoLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        FundooContext fundooContext;
        IDistributedCache distributedCache;
        INoteBL iNoteBL;

        public NoteController(INoteBL iNoteBL, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iNoteBL = iNoteBL;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.ArchieveNotes(noteIDModel, userId);
                if (result != null)
                {
                    if (result == true)
                    {
                        return this.Ok(new { success = true, message = "Note Archived" });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Note Unarchived", data = result });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }

            }
            catch (Exception)
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.PinnedNotes(noteIDModel, userId);
                if (result != null)
                {
                    if (result == true)
                    {
                        return this.Ok(new { success = true, message = "Note Pinned", data = result });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Note Unpinned" });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.TrashedNotes(noteIDModel, userId);
                if (result != null)
                {
                    if (result == true)
                    {
                        return this.Ok(new { success = true, message = "Note Trashed", data = result });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Note Restore" });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
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
        public IActionResult BgColor(NoteColor noteColor)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.NoteColor(noteColor, UserId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Background Color Done", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Background Color Not Done" });
                }
            }
            catch (Exception)
            {
                throw;
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

        [Authorize]
        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllNoteUsingRedisCache()
        {
            var cacheKey = "noteList";
            string serializedNoteList;
            var noteList = new List<NoteEntity>();
            var redisNoteList = await distributedCache.GetAsync(cacheKey);
            if (redisNoteList != null)
            {
                serializedNoteList = Encoding.UTF8.GetString(redisNoteList);
                noteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);
            }
            else
            {
                noteList = await fundooContext.NoteTable.ToListAsync();
                serializedNoteList = JsonConvert.SerializeObject(noteList);
                redisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNoteList, options);
            }
            return Ok(noteList);
        }
    }
}