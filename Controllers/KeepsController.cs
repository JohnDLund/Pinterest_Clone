using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;


        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        // GETS ALL PUBLIC KEEPS
        [HttpGet]
        public ActionResult<IEnumerable<Keep>> Get()
        {
            try
            {
                return Ok(_ks.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // GETS SINGLE KEEP BY ID
        [HttpGet("{keepId}")]
        public ActionResult<Keep> GetById(int keepId)
        {
            try
            {
                Keep foundKeep = _ks.GetById(keepId);

                Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (foundKeep.IsPrivate == true && userId.Value != foundKeep.UserId)
                {
                    return BadRequest("That keep is private");
                }
                return Ok(foundKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GETS ALL KEEPS CREATED BY AUTHENTICATED USER
        [HttpGet("user")]
        [Authorize]
        public ActionResult<IEnumerable<Keep>> GetByUserId()
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = user.Value;

                return Ok(_ks.GetByUserId(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // CREATES NEW KEEP
        [HttpPost]
        [Authorize]
        public ActionResult<Keep> Post([FromBody] Keep newKeep)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                newKeep.UserId = user.Value;
                return Ok(_ks.Create(newKeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETES SINGLE KEEP BY ID
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Keep> Delete(int id)
        {
            try
            {
                Claim User = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = User.Value;
                return Ok(_ks.Delete(id, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // EDITS SINGLES KEEP BY ID
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Keep> Edit([FromBody] Keep newKeep, int id)
        {
            try
            {
                Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (newKeep.UserId != userId.Value)
                {
                    throw new Exception("You can only edit your keeps");
                }
                newKeep.Id = id;
                return Ok(_ks.Edit(newKeep, userId.Value));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GETS USER ID OF AUTHENTICATED USER
        [HttpGet("userId")]
        public ActionResult<string> GetUserId()
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = user.Value;
                return userId;
            }
            catch
            {
                return BadRequest("you are not logged in");
            }
        }

    }
}