using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPermissions.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionHelper _permissionHelper;
        private readonly IPermissionsUnitOfWork _permissionsUnitOfWork;

        public PermissionsController(IPermissionsUnitOfWork permissionsUnitOfWork, IPermissionHelper permissionHelper)
        {
            _permissionHelper = permissionHelper;
            _permissionsUnitOfWork = permissionsUnitOfWork;
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(PermissionDTO permissionDTO)
        {
            var response = await _permissionsUnitOfWork.UpdateFullAsync(User.Identity!.Name!, permissionDTO);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("full")]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _permissionsUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]  PaginationDTO pagination)
        {
            var response = await _permissionsUnitOfWork.GetAsync(User.Identity!.Name!, pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _permissionsUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("totalPages")]
        public async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _permissionsUnitOfWork.GetTotalPagesAsync(User.Identity!.Name!, pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(PermissionDTO permissionDTO)
        {
            var response = await _permissionHelper.ProcessPermissionAsync(User.Identity!.Name!, permissionDTO.CategoryPermissionId, permissionDTO.Description);
            if (response.WasSuccess)
            {
                return NoContent();
            }
            return BadRequest(response.Message);
        }
    }
}