using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using CommonLayer.ModelClass;
using RepoLayer.Entity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Context;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ICollabBL iCollabBL;
        FundooContext fundooContext;
        IDistributedCache distributedCache;


        public CollabController(ICollabBL iCollabBL, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iCollabBL = iCollabBL;
            this.fundooContext= fundooContext;
            this.distributedCache = distributedCache;
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

        [Authorize]
        [HttpGet]
        [Route("RetrieveCollaboration")]

        public IActionResult RetrieveCollab(long CollabId)
        {
            try
            {
                var result = iCollabBL.RetrieveCollab(CollabId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Data Retrieve Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Data Retrieve UnSuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveCollaborationThroughNote")]

        public IActionResult RetrieveCollabThroughNotes(long NoteID)
        {
            try
            {
                var result = iCollabBL.RetrieveCollabThroughNotes(NoteID);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Data Retrieve Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Data Retrieve UnSuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveCollaborationThroughUser")]

        public IActionResult RetrieveCollabThroughUsers(long UserId)
        {
            try
            {
                var result = iCollabBL.RetrieveCollabThroughUsers(UserId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Data Retrieve Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Data Retrieve UnSuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteCollaboration")]
        public IActionResult DeleteCollab(CollabIDModel collabIDModel)
        {

            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);

                var result = iCollabBL.DeleteCollab(collabIDModel, userId);

                if (result == true)
                {
                    return Ok(new { success = true, message = "Data Delete Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Data Delete UnSuccessful" });
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
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "collabList";
            string serializedCollabList;
            var collabList = new List<CollaborationEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<CollaborationEntity>>(serializedCollabList);
            }
            else
            {
                collabList = await fundooContext.CollabTable.ToListAsync();
                serializedCollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }
    }
}
