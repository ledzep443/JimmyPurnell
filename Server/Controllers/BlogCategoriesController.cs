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
    public class BlogCategoriesController : ControllerBase
    {
        private readonly AppDbContext _appDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogCategoriesController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDBContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            List<BlogCategory> categories = await _appDBContext.BlogCategories.ToListAsync();

            return Ok(categories);
        }
        [HttpGet("withposts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWithPosts()
        {
            List<BlogCategory> categories = await _appDBContext.BlogCategories
                .Include(category => category.Posts)
                .ToListAsync();

            return Ok(categories);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            BlogCategory category = await GetCategoryByCategoryId(id, true);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogCategory categoryToCreate)
        {
            try
            {
                if (categoryToCreate == null)
                {
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                await _appDBContext.BlogCategories.AddAsync(categoryToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
                }
                else
                {
                    return Created("Create", categoryToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on out side. Please contact the administrator. Error message: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BlogCategory updatedCategory)
        {
            try
            {
                if (id < 1 || updatedCategory == null || id != updatedCategory.CategoryId)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.BlogCategories.AnyAsync(category => category.CategoryId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                _appDBContext.BlogCategories.Update(updatedCategory);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our end. Please contact the administrator.");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our end. Please contact the Administrator. Error message: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.BlogCategories.AnyAsync(category => category.CategoryId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                BlogCategory categoryToDelete = await GetCategoryByCategoryId(id, false);

                if (categoryToDelete.ThumbnailImagePath != "uploads/placeholder.jpg")
                {
                    string fileName = categoryToDelete.ThumbnailImagePath.Split('/').Last();

                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
                }

                _appDBContext.BlogCategories.Remove(categoryToDelete);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
                }

                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our end. Please contact the administrator. Error message: {e.Message}");
            }
        }

        #region UtilityFunctions

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PersistChangesToDatabase()
        {
            int amountOfChanges = await _appDBContext.SaveChangesAsync();

            return amountOfChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<BlogCategory> GetCategoryByCategoryId(int categoryId, bool withPosts)
        {
            BlogCategory categoryToGet = null;

            if (withPosts == true)
            {
                categoryToGet = await _appDBContext.BlogCategories
                    .Include(category => category.Posts)
                    .FirstAsync(category => category.CategoryId == categoryId);
            }
            else
            {
                categoryToGet = await _appDBContext.BlogCategories
                    .FirstAsync(category => category.CategoryId == categoryId);
            }

            return categoryToGet;
        }
        #endregion
    }
}
