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
    public class ImageNoticesController : GenericController<ImageNotice>
    {
        private readonly IImageNoticesUnitOfWork _imageNoticesUnitOfWork;

        public ImageNoticesController(IGenericUnitOfWork<ImageNotice> unitOfWork, IImageNoticesUnitOfWork imageNoticesUnitOfWork) : base(unitOfWork)
        {
            _imageNoticesUnitOfWork = imageNoticesUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _imageNoticesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _imageNoticesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}