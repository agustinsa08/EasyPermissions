using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyPermissions.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeNoticesController : GenericController<TypeNotice>
    {
        private readonly ITypeNoticesUnitOfWork _typeNoticesUnitOfWork;

        public TypeNoticesController(IGenericUnitOfWork<TypeNotice> unitOfWork, ITypeNoticesUnitOfWork typeNoticesUnitOfWork) : base(unitOfWork)
        {
            _typeNoticesUnitOfWork = typeNoticesUnitOfWork;
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _typeNoticesUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _typeNoticesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _typeNoticesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _typeNoticesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }
    }
}
