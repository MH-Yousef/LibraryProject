 using Core.Domains;
using Data.DTOs.UserDTO;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.User
{
    public interface IAppUserService
    {
        public bool IsSignIn();
        public int GetUserId();
        public Task<ApplicationUser> GetUser();
        public Task<List<UserDTO>> GetAddFreindList(int id);
        public Task<UserDTO> GetUserDTO();
        public Task<ValidationResult> Register(UserDTO userDTO);
        public Task<ValidationResult> Login(UserDTO userDTO);
        public Task LogoutAsync();
        public Task<ValidationResult> UpdateProfile(UserDTO userDTO);

    }
}
