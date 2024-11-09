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
    public class UsersController : GenericController<User>
    {
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly string _container;

        public UsersController(IGenericUnitOfWork<User> unitOfWork, IUsersUnitOfWork usersUnitOfWork, IFileStorage fileStorage) : base(unitOfWork)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
            _container = "users";
        }

        [HttpGet("GetUserPhoto/{userId:guid}")]
        public async Task<IActionResult> GetUserPhotoAsync(Guid userId)
        {
            var user = await _usersUnitOfWork.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { success = false, message = "Usuario no encontrado" });
            }
            var userIdString = $"{userId.ToString()}.jpg";
            byte[]? photoData = await _fileStorage.GetFileAsync(userIdString, _container);
            if (user.Photo == null)
            {
                return NotFound(new { success= false, message = "Foto no encontrada / El usuario no tiene foto registrada." });
            }
            return Ok(new { Photo = user.Photo });
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _usersUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _usersUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _usersUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("allLeaders")]
        public async Task<IActionResult> GetAllLeaderAsync()
        {
            var response = await _usersUnitOfWork.GetAllLeaderAsync();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("getDetail/{userId:guid}")]
        public async Task<IActionResult> GetDetailAsync(Guid userId)
        {
            var response = await _usersUnitOfWork.GetDetailAsync(userId);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
