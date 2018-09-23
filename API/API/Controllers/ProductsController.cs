using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        public ProductsController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IList<Product>> Get(int? categoryId)
        {
            if (categoryId != null)
            {
                var _products = await _appDbContext.Products.Include(p => p.Images).Where(p => p.categoryId == categoryId).ToListAsync();
                return _products;
            }
            else
            {
                return _appDbContext.Products.Include(p => p.Images).AsEnumerable().ToList();//.ToListAsync();
            }
        }
    }
}