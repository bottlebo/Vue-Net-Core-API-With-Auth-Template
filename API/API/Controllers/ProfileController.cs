
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager, ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        // GET api/profile/me
        [HttpGet]
        public async Task<IActionResult> Me()
        {
            await Task.Delay(5250);

            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            //var customer = await _appDbContext.Customers.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            var user = await _userManager.FindByIdAsync(userId.Value);
            return new OkObjectResult(new
            {
                user.FirstName, user.LastName, user.PictureUrl, user.FacebookId
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
