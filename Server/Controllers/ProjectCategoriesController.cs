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

        public ProjectCategoriesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

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

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<ProjectCategory> GetProjectCategoryById(int id, bool withProjects)
        {
            ProjectCategory categoryToGet = null;

            if(withProjects == true)
            {
                categoryToGet = await _appDbContext.ProjectCategories
                    .Include(category => category.Projects)
                    .FirstAsync(category => category.CategoryId == id);
            }
            else
            {
                categoryToGet = await _appDbContext.ProjectCategories
                    .FirstAsync(category => category.CategoryId == id);
            }

            return categoryToGet;
        }
    }
}
