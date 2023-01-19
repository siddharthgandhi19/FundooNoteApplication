﻿using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepoLayer.Interface;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelRL iLabelBL;
        public LabelController(ILabelRL iLabelBL)
        {
            this.iLabelBL = iLabelBL;
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
    }
}