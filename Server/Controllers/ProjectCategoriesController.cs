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
    public class ProjectCategoriesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectCategoriesController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        #region CRUD Operations
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            List<ProjectCategory> projectCategories = await _appDbContext.ProjectCategories.ToListAsync();

            return Ok(projectCategories);
        }

        [HttpGet("withprojects")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWithProjects()
        {
            List<ProjectCategory> projectCategories = await _appDbContext.ProjectCategories
                .Include(category => category.Projects)
                .ToListAsync();

            return Ok(projectCategories);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            ProjectCategory category = await GetProjectCategoryById(id, true);

            return Ok(category);
        }

        [HttpGet("withprojects/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWithProjects(int id)
        {
            ProjectCategory projectCategory = await GetProjectCategoryById(id, true);

            return Ok(projectCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectCategory categoryToCreate)
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

                await _appDbContext.ProjectCategories.AddAsync(categoryToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our end. Please contact the administrator.");
                }

                else
                {
                    return Created("Create", categoryToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our end. Please contact the administrator. Error message: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectCategory updatedCategory)
        {
            try
            {
                if (id < 1 || updatedCategory == null || id != updatedCategory.ProjectCategoryId)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDbContext.ProjectCategories.AnyAsync(category => category.ProjectCategoryId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                _appDbContext.ProjectCategories.Update(updatedCategory);

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
                return StatusCode(500, $"Somethin went wrong on our end. Please contact the administrator. Error message: {e.Message}");
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

                bool exists = await _appDbContext.ProjectCategories.AnyAsync(category => category.ProjectCategoryId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                ProjectCategory categoryToDelete = await GetProjectCategoryById(id, false);

                if (categoryToDelete.ThumbnailImagePath != "uploads/placeholder.jpg")
                {
                    string fileName = categoryToDelete.ThumbnailImagePath.Split('/').Last();

                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
                }

                _appDbContext.ProjectCategories.Remove(categoryToDelete);

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
                return StatusCode(500, $"Something went wrong on our end. Please contact the administrator. Error message: {e.Message}");
            }
        }
#endregion
        #region Utility Methods

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PersistChangesToDatabase()
        {
            int amountOfChanges = await _appDbContext.SaveChangesAsync();

            return amountOfChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<ProjectCategory> GetProjectCategoryById(int id, bool withProjects)
        {
            ProjectCategory categoryToGet = null;

            if(withProjects == true)
            {
                categoryToGet = await _appDbContext.ProjectCategories
                    .Include(category => category.Projects)
                    .FirstAsync(category => category.ProjectCategoryId == id);
            }
            else
            {
                categoryToGet = await _appDbContext.ProjectCategories
                    .FirstAsync(category => category.ProjectCategoryId == id);
            }

            return categoryToGet;
        }
        #endregion
    }
}
