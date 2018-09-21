using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using API.Data;
using API.Models.Entities;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    [Authorize(Policy = "RequireAdmin")]
    public class AdminController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager, ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            List<string> res = new List<string>();

            foreach (var claim in _caller.Claims)
            {
                res.Add(claim.Type + ":" + claim.Value);
            }
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            //var customer = await _appDbContext.Customers.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            var user = await _userManager.FindByIdAsync(userId.Value);

            var _isadmin = await _userManager.IsInRoleAsync(user, "Admin");


            return new OkObjectResult(new
            {
                user.FirstName,
                user.LastName,
                user.PictureUrl,
                user.FacebookId
                //customer.Identity.FirstName,
                //customer.Identity.LastName,
                //customer.Identity.PictureUrl,
                //customer.Identity.FacebookId,
                //customer.Location,
                //customer.Locale,
                //customer.Gender
            });
        }
    }
}