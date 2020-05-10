using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNet.Security.OAuth.Validation;
using MCJPortal.Domain.Context;
using MCJPortal.Domain.Models.Authorization;
using MCJPortal.ViewModels.ViewModels.Authorization;
using MCJPortal.ViewModels.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace MCJPortal.Controllers.Authentication
{
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme, Roles = "Admin,CustomerSupport")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseUserController
    {
        private readonly MainDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UsersController(
            MainDbContext ctx,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            _ctx = ctx;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<string> GetCurrentUserName()
        {
            var user = await _userManager.FindByIdAsync(this.GetUserId());
            return $"{user.FirstName} {user.LastName}";
        }

        [HttpGet("[action]")]
        public async Task<UserViewModel> GetCurrentUser()
        {
            var userId = this.GetUserId();
            var user = await _userManager.Users.Include(x => x.Country).ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }

        [HttpGet("[action]")]
        public async Task<List<UserViewModel>> GetAllAsync([FromQuery]string name, [FromQuery]int? countryId)
        {
            var users = _ctx.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(x => x.UserProjects)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                var term = name.ToLower();
                users = users.Where(x => x.UserName.ToLower().Contains(term));
            }

            if (countryId != null)
            {
                users = users.Where(x => x.CountryId == countryId);
            }

            var resultDbData = await users.ToListAsync();

            var userList = resultDbData
                .Select(x => _mapper.Map<UserViewModel>(x))
                .OrderByDescending(u => u.UserName)
                .ToList();

            return userList;
        }

        [HttpPost("[action]")]
        public async Task<UserViewModel> Update([FromBody] UserViewModel model)
        {
            var user = await _ctx.Users
                .Include(x=> x.UserRoles)
                .Include(x => x.UserProjects)
                .FirstAsync(x => x.Id == model.Id);
            _mapper.Map<UserViewModel, ApplicationUser>(model, user);

            if(!string.IsNullOrEmpty(model.Password))
            {
               //var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, model.Password);
               // user.PasswordHash = hashedNewPassword;

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, model.Password);
            }

            await _ctx.SaveChangesAsync();

            return model;
        }

        [HttpPut("[action]")]
        public async Task<UserViewModel> Create([FromBody] UserViewModel model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            user.Id = Guid.NewGuid().ToString();
            user.EmailConfirmed = true;

            var r = await _userManager.CreateAsync(user, model.Password);
            if (!r.Succeeded)
            {
                throw new Exception("Error");
            }

            await _ctx.SaveChangesAsync();

            return model;
        }

        [HttpDelete("[action]/{id}")]
        public async Task Delete(string id)
        {
            var user = await _ctx.Users
                .Include(x => x.UserProjects)
                .FirstAsync(x => x.Id == id);

            _ctx.Users.Remove(user);

            await _ctx.SaveChangesAsync();
        }

        /// <param name="userId"></param>
        /// <returns></returns>
        /// <example>
        /// http://localhost:60001/api/users/get?userId=2E9198F8-A351-4A1F-8C4C-E99C17B916D6
        /// </example>
        [HttpGet("[action]/{userId}")]
        public async Task<ApplicationUser> GetUser(string userId)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

    }
}
