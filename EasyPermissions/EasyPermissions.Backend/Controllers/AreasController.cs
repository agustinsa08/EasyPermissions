﻿using EasyPermissions.Backend.UnitsOfWork.Implementations;
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
    public class AreasController : GenericController<Area>
    {
        private readonly IAreasUnitOfWork _areasUnitOfWork;

        public AreasController(IGenericUnitOfWork<Area> unitOfWork, IAreasUnitOfWork areasUnitOfWork) : base(unitOfWork)
        {
            _areasUnitOfWork = areasUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _areasUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _areasUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("withoutLeader")]
        public async Task<IActionResult> GetAllWhithoutLeaderAsync()
        {
            var response = await _areasUnitOfWork.GetAllWhithoutLeaderAsync();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}