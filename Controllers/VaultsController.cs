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
    [Authorize]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vks;
        public VaultsController(VaultsService vs, VaultKeepsService vks)
        {
            _vs = vs;
            _vks = vks;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Vault>> Get()
        {
            try
            {
                return Ok(_vs.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }


        [HttpGet("{vaultId}")]
        [Authorize]
        public ActionResult<Vault> GetById(int vaultId)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                Vault foundVault = _vs.GetById(vaultId);
                if (foundVault.UserId != user.Value)
                {
                    throw new Exception("Vault does not belong to you");
                }
                return Ok(foundVault);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }



        [HttpGet("user")]
        public ActionResult<IEnumerable<Vault>> GetByUserId()
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user.Value == null)
                {
                    throw new Exception("You must be logged in to get vaults");
                }
                return Ok(_vs.GetByUserId(user.Value));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpGet("{vaultId}" + "/keeps")]
        [Authorize]
        public ActionResult<IEnumerable<VaultKeepViewModel>> GetKeepsByVaultId(int vaultId)
        {
            try
            {
                Claim User = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = User.Value;
                return Ok(_vks.GetKeepsByVaultId(vaultId, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult<Vault> Post([FromBody] Vault newVault)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                newVault.UserId = user.Value;
                return Ok(_vs.Create(newVault));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Vault> Delete(int id)
        {
            try
            {
                Claim User = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = User.Value;
                return Ok(_vs.Delete(id, userId));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}