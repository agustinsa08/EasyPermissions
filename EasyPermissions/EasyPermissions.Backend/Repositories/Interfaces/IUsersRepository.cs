﻿using EasyPermissions.Shared.DTOs;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace EasyPermissions.Backend.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<User> GetUserAsync(string email);

        Task<User> GetUserAsync(Guid userId);

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();

        Task<User> GetUserByIdAsync(Guid userId);

        Task<ActionResponse<IEnumerable<User>>> GetAsync();

        Task<ActionResponse<IEnumerable<User>>> GetAsync(PaginationDTO pagination);

        Task<List<User>> GetAllLeaderAsync();

        Task<List<User>> GetAllAdminAsync();

        Task<User> GetDetailAsync(string email);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<List<User>> GetUserByTypeAsync(int? userType);

    }
}