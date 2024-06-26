﻿using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.UnitsOfWork.Interfaces
{
    public interface ICategoryNoticesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<CategoryNotice>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}
