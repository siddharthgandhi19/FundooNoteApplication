﻿using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

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

        public IActionResult CreateCollab(long notesId, string email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iCollabBL.CreateCollab(notesId, email);

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

        public IActionResult RetrieveCollab(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);

                var result = iCollabBL.RetrieveCollab(noteId, userId);

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
        public IActionResult DeleteCollab(long collabId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);

                var result = iCollabBL.DeleteCollab(collabId, userId);

                if (result != null)
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
    }
}
