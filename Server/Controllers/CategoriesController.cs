using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _appDBContext;

        public CategoriesController(AppDbContext appDbContext)
        {
            _appDBContext = appDbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = await _appDBContext.Categories.ToListAsync();

            return Ok(categories);
        }
        [HttpGet("withposts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWithPosts()
        {
            List<Category> categoties = await _appDBContext.Categories
                .Include(category => category.Posts)
                .ToListAsync();

            return Ok(categoties);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            Category category = await GetCategoryByCategoryId(id, true);

            return Ok(category);
        }


        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Category> GetCategoryByCategoryId(int categoryId, bool withPosts)
        {
            Category categoryToGet = null;

            if (withPosts == true)
            {
                categoryToGet = await _appDBContext.Categories
                    .Include(category => category.Posts)
                    .FirstAsync(category => category.CategoryId == categoryId);
            }
            else
            {
                categoryToGet = await _appDBContext.Categories
                    .FirstAsync(category => category.CategoryId == categoryId);
            }

            return categoryToGet;
        }

    }
}
