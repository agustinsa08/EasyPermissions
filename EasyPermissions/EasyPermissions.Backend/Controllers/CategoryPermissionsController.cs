using EasyPermissions.Backend.UnitsOfWork.Implementations;
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
    public class CategoryPermissionsController : GenericController<CategoryPermission>
    {
        private readonly ICategoryPermissionsUnitOfWork _categoryPermissionsUnitOfWork;

        public CategoryPermissionsController(IGenericUnitOfWork<CategoryPermission> unitOfWork, ICategoryPermissionsUnitOfWork categoryPermissionsUnitOfWork) : base(unitOfWork)
        {
            _categoryPermissionsUnitOfWork = categoryPermissionsUnitOfWork;
        }

        [HttpGet("combo/{typePermissionId:int}")]
        public async Task<IActionResult> GetComboAsync(int typePermissionId)
        {
            return Ok(await _categoryPermissionsUnitOfWork.GetComboAsync(typePermissionId));
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _categoryPermissionsUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _categoryPermissionsUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _categoryPermissionsUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _categoryPermissionsUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
    }
}