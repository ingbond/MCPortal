using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AspNet.Security.OAuth.Validation;
using MCJPortal.Domain.Models.Authorization;
using MCJPortal.ViewModels.ViewModels;
using MCJPortal.Domain.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MCJPortal.ViewModels.ViewModels.Authorization;
using MCJPortal.Domain.Models;
using MCJPortal.Application.Services.Interfaces;

namespace MCJPortal.Controllers.Authentication
{
    //[Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme, Roles = "Admin,CustomerSupport")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly MainDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IListItemsService _listItemsService;

        public RolesController(
            MainDbContext ctx,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper,
            IListItemsService listItemsService)
        {
            _ctx = ctx;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _listItemsService = listItemsService;
        }

        public class RoleParams
        {
            public string roleName;
            public string userId;
        }

        [HttpGet("[action]")]
        public async Task<List<RoleViewModel>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var rolesList = roles.Select(x => _mapper.Map<RoleViewModel>(x)).ToList();

            return rolesList;
        }

        [HttpGet("[action]/{roleId}")]
        public async Task<List<EditPermissionViewModel>> GetAllPermissions(string roleId)
        {
            var permissions = await _listItemsService.GetItemsForTypeAsync(Domain.Models.Enums.ListTypeEnum.Permission);

            // need only permissions for concrete role
            foreach(var p in permissions)
            {
                p.Permissions = p.Permissions.Where(x => x.RoleId == roleId).ToList();
            }

            var result = permissions.Select(x => {
                var resultt = _mapper.Map<EditPermissionViewModel>(x);
                return resultt;
            }).ToList();

            return result;
        }

        [HttpPut("[action]/{roleId}")]
        public async Task SetPermissionAsync(List<EditPermissionViewModel> model, string roleId)
        {
            var existingData = await _ctx.RolePermissions.Where(x => x.RoleId == roleId).ToListAsync();

            _ctx.RolePermissions.RemoveRange(existingData);

            foreach(var permission in model)
            {
                if (permission.Access.HasValue)
                {
                    RolePermission newPermission = new RolePermission
                    {
                        PermissionId = permission.Id,
                        RoleId = roleId,
                        Access = permission.Access.Value
                    };
                    await _ctx.RolePermissions.AddAsync(newPermission);
                }
            }

            await _ctx.SaveChangesAsync();
        }

        [HttpGet("[action]")]
        public async Task<List<RolePermissionViewModel>> GetPermissionsAsync(string roleId)
        {
            var permissions = _ctx.RolePermissions.Where(x => x.RoleId == roleId);
            var result = await permissions.Select(x => _mapper.Map<RolePermissionViewModel>(x)).ToListAsync();

            return result;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            return Json(roles);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SetRole([FromBody] RoleParams data)
        {
            var roleExists = await _roleManager.RoleExistsAsync(data.roleName);
            var user = await _userManager.FindByIdAsync(data.userId);

            if (roleExists)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, data.roleName);
                if (roleResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RemoveRole([FromBody] RoleParams data)
        {
            var roleExists = await _roleManager.RoleExistsAsync(data.roleName);
            var user = await _userManager.FindByIdAsync(data.userId);

            if (roleExists)
            {
                var roleResult = await _userManager.RemoveFromRoleAsync(user, data.roleName);
                if (roleResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <example>
        /// http://localhost:60001/api/roles/get?roleId=2E9198F8-A351-4A1F-8C4C-E99C17B916D6
        /// </example>
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            RoleViewModel model = MapToViewModel(role);

            return Json(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "invalid ViewModel" });
            }

            var role = _ctx.Roles.Where(r => r.Id == model.Id).FirstOrDefault();

            if (role == null)
            {
                return BadRequest(new { error = "unable to update role" });
            }

            role.Name = model.Name;

            await _ctx.SaveChangesAsync();

            return Ok(new { status = "success", Id = model.Id });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody] RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "invalid ViewModel" });
            }

            var r = await _roleManager.CreateAsync(new ApplicationRole() { Name = model.Name});
            if (!r.Succeeded)
            {
                return BadRequest(new { error = "unable to create role" });
            }

            return Ok(new { status = "success" });
        }

        private static RoleViewModel MapToViewModel(IdentityRole role)
        {
            return new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.NormalizedName,
            };
        }
    }
}
