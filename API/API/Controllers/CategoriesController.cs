using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models.Entities;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        public CategoriesController( ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public  IActionResult All()
        {
            return Ok(_appDbContext.Categories);
        }
    }
}