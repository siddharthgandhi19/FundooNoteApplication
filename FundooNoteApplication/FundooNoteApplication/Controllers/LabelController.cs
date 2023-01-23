using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepoLayer.Interface;
using Microsoft.Extensions.Caching.Distributed;
using RepoLayer.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RepoLayer.Entity;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ILabelRL iLabelBL;
        FundooContext fundooContext;
        IDistributedCache distributedCache;
        public LabelController(ILabelRL iLabelBL, FundooContext fundooContext, IDistributedCache distributedCache)
        {
            this.iLabelBL = iLabelBL;
            this.fundooContext = fundooContext;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(long NoteID, string labelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iLabelBL.AddLabel(NoteID, userId, labelName);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Created label successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Created label unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveThroughLabelID")]
        public IActionResult RetrieveLabel(long LabelID)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);

                var result = iLabelBL.RetrieveLabel(LabelID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Retrieving Label Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false,essage = "Retrieving Label Unsuccessfully"});
                }
            }
            catch (System.Exception) { throw; }
        }

        [Authorize]
        [HttpGet]
        [Route("RetrieveThroughNoteID")]
        public IActionResult RetrieveLabelThroughNoteID(long NoteID)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);

                var result = iLabelBL.RetrieveLabelThroughNoteID(NoteID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Retrieving Label Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, essage = "Retrieving Label Unsuccessfully" });
                }
            }
            catch (System.Exception) { throw; }
        }

        [Authorize]
        [HttpGet]
        [Route("DeleteLabel")]

        public IActionResult DeleteLabel(long LabelID)
        {
            try
            {
                var result = iLabelBL.DeleteLabel(LabelID);

                if (result == true)
                {
                    return Ok(new { success = true, message = "Label Deleted Succesfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Deleted Unsuccessful" });
                }
            }
            catch (System.Exception) { throw; }
        }

        [Authorize]
        [HttpPut]
        [Route("EditLabel")]

        public IActionResult EditLabel(long NoteID, long LabelID, string labelName)
        {
            try
            {
                var result = iLabelBL.EditLabel(NoteID, LabelID, labelName);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Updated Successful", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Label Updated Unsuccessful" });
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
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedLabelList;
            var labelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                labelList = await fundooContext.LabelTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }
    }
}