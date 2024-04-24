using EasyPermissions.Backend.UnitsOfWork.Implementations;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyPermissions.Backend.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    public class TypePermissionsController : GenericController<TypePermission>
    {
        private readonly ITypePermissionsUnitOfWork _typePermissionsUnitOfWork;

        public TypePermissionsController(IGenericUnitOfWork<TypePermission> unitOfWork, ITypePermissionsUnitOfWork typePermissionsUnitOfWork) : base(unitOfWork)
        {
            _typePermissionsUnitOfWork = typePermissionsUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _typePermissionsUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _typePermissionsUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
