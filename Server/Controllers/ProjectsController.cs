using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public ProjectsController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        #region CRUD Operations

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            List<Project> projects = await _appDbContext.Projects
                .Include(project => project.Category)
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("dto/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDTO(int id)
        {
            Project project = await GetProjectByProjectId(id);
            ProjectDTO projectDTO = _mapper.Map<ProjectDTO>(project);

            return Ok(projectDTO);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            Project project = await GetProjectByProjectId(id);

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectDTO projectToCreateDTO)
        {
            try
            {
                if (projectToCreateDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Project projectToCreate = _mapper.Map<Project>(projectToCreateDTO);

                if (projectToCreate.IsPublished == true)
                {
                    projectToCreate.PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm");
                }

                await _appDbContext.Projects.AddAsync(projectToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
                }
                else
                {
                    return Created("Create", projectToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our end. Please contact the administrator. Error message: {e.Message}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectDTO updatedProjectDTO)
        {
            try
            {
                if (id < 1 || updatedProjectDTO == null || id != updatedProjectDTO.Id)
                {
                    return BadRequest(ModelState);
                }

                Project oldProject = await _appDbContext.Projects.FindAsync(id);

                if (oldProject == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Project updatedProject = _mapper.Map<Project>(updatedProjectDTO);

                if (updatedProject.IsPublished == true)
                {
                    if (oldProject.IsPublished == false)
                    {
                        updatedProject.PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm");
                    }
                    else
                    {
                        updatedProject.PublishDate = oldProject.PublishDate;
                    }
                }
                else
                {
                    updatedProject.PublishDate = string.Empty;
                }

                //Detach old project from EF, or else it can't be updated
                _appDbContext.Entry(oldProject).State = EntityState.Detached;

                _appDbContext.Projects.Update(updatedProject);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our end. Please contact the administrator.");
                }
                else
                {
                    return Created("Create", updatedProject);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our end. Please contact the administrator. Error message: {e.Message}.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDbContext.Projects.AnyAsync(project => project.Id == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Project projectToDelete = await GetProjectByProjectId(id);

                if (projectToDelete.ScreenshotImagePath != "uploads/placeholder.jpg")
                {
                    string fileName = projectToDelete.ScreenshotImagePath.Split('/').Last();

                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
                }

                _appDbContext.Projects.Remove(projectToDelete);

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
                return StatusCode(500, $"Something went wrong on our end. Please contact the administrator. Error message: {e.Message}.");
            }
        }

        #endregion
        #region Utility methods

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PersistChangesToDatabase()
        {
            int amountOfChanges = await _appDbContext.SaveChangesAsync();
            return amountOfChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Project> GetProjectByProjectId(int projectId)
        {
            Project projectToGet = await _appDbContext.Projects
                .Include(project => project.Category)
                .FirstAsync(project => project.Id == projectId);

            return projectToGet;
        }

        #endregion
    }
}
