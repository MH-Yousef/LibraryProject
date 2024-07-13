using Core.Domains;
using Core.Lookups;
using Core.Validations;
using Data.Context;
using Data.DTOs.UserDTO;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace Service.User
{
    public class AppUserService : IAppUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IValidator<UserDTO> _userValidator;
        private readonly AppDbContext _context;
        private readonly IImageService _imageService;
        public AppUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IValidator<UserDTO> userValidator, AppDbContext context, IImageService imageService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _userValidator = userValidator;
            _context = context;
            _imageService = imageService;
        }

        public int GetUserId()
        {
            var id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).AsInt();
            return id;
        }

        public bool IsSignIn()
        {
            var IsSignIn = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            return IsSignIn;
        }

        public async Task<ApplicationUser> GetUser()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == GetUserId());
            return user;
        }

        public async Task<UserDTO> GetUserDTO()
        {
            var user = await GetUser();
            var userDTO = new UserDTO
            {
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                Image = user.Image
            };
            return userDTO;
        }

        #region Register
        public async Task<ValidationResult> Register(UserDTO userDTO)
        {

            ValidationResult validator = _userValidator.Validate(userDTO);
            if (validator.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    FullName = userDTO.FullName,
                };
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        validator.Errors.Add(new ValidationFailure(error.Code, error.Description));
                    }

                }
            }
            return validator;
        }
        #endregion

        #region Login
        public async Task<ValidationResult> Login(UserDTO user)
        {
            UserValidator userValidator = new UserValidator(IsLogin: true);
            ValidationResult validator = userValidator.Validate(user, options =>
            {
                if (user.Email != null && user.UserName == null)
                {
                    options.IncludeProperties(x => x.Email);
                }
                else if (user.UserName == null && user.Email == null)
                {
                    options.IncludeProperties(x => x.UserName);
                }
                else
                {
                    options.IncludeProperties(x => x.UserName);
                }
                options.IncludeProperties(x => x.Password);
            }
            );
            if (validator.IsValid)
            {
                if (user.UserName != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
                    if (!result.Succeeded)
                    {
                        validator.Errors.Add(new ValidationFailure("Login", "Kullanıcı adı veya şifre hatalı"));
                    }
                }
                if (user.Email != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
                    if (!result.Succeeded)
                    {
                        validator.Errors.Add(new ValidationFailure("Login", "Email veya şifre hatalı"));
                    }
                }
            }
            return validator;
        }
        #endregion

        #region Logout
        public async Task LogoutAsync()
        {
            var cheackUser = IsSignIn();
            if (cheackUser)
            {
                await _signInManager.SignOutAsync();

            }
        }
        #endregion



        #region Profile
        public async Task<ValidationResult> UpdateProfile(UserDTO userDTO)
        {
            UserValidator userValidator = new UserValidator(IsProfile: true);
            ValidationResult validator = userValidator.Validate(userDTO);
            if (validator.IsValid)
            {
                var user = await GetUser();
                user.FullName = userDTO.FullName;
                user.Email = userDTO.Email;
                if (userDTO.file != null)
                {
                    string type = ImageType.ProfilePhoto;

                    user.Image = await _imageService.UploadImage(userDTO.file, type);
                }
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            return validator;
        }
        #endregion


        private async Task<string> UploadImage(IFormFile file, string type)
        {
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, "wwwroot", "Images", type);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(path, fileName);
                var stream = new FileStream(saveLocation, FileMode.Create);
                await file.CopyToAsync(stream);

                return fileName;
            }
            return null;
        }

        public async Task<List<UserDTO>> GetAddFreindList(int id)
        {
            try
            {
                var sentRequests = await _context.Friendships.Where(x => x.SenderId == id).Select(x => x.ReceiverId).ToListAsync();
                var receivedRequests = await _context.Friendships.Where(x => x.ReceiverId == id).Select(x => x.SenderId).ToListAsync();

                var users = await _context.Users
                    .Where(x => x.Id != id && !sentRequests.Contains(x.Id) && !receivedRequests.Contains(x.Id)).Select(x => new UserDTO
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Image = x.Image
                }).ToListAsync();

               
                if (users == null)
                    return null;
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
