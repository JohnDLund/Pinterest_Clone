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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;
        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }

        // GETS ALL THE RELATIONSHIPS BETWEEN VAULTS AND KEEPS
        [HttpGet]
        public ActionResult<IEnumerable<VaultKeep>> Get()
        {
            try
            {
                return Ok(_vks.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        // GETS THE RELATIONSHIP BETWEEN VAULTS AND THEIR KEEPS
        [HttpGet("{vaultKeepId}")]
        public ActionResult<VaultKeep> GetById(int vaultKeepId)
        {
            try
            {
                return Ok(_vks.GetById(vaultKeepId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        // ADDS A SINGLE KEEP TO A SINGLE VAULT
        [HttpPost]
        [Authorize]
        public ActionResult<VaultKeep> Post([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                newVaultKeep.UserId = userId.Value;
                return Ok(_vks.Create(newVaultKeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // REMOVES A SINGLE KEEP FROM A SINGLE VAULT BY DELETING RELATIONSHIP
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<VaultKeep> Delete(int id)
        {
            try
            {
                Claim User = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = User.Value;
                return Ok(_vks.Delete(id, userId));

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}