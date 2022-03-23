using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;


namespace Server.Data
{
    public class AppDbContext : IdentityDbContext
    {
        private IConfiguration _config;
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Project> Projects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options) { 
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var pgSqlConnectionString = _config.GetConnectionString("DefaultConnection");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(pgSqlConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Categories seed

            BlogCategory[] categoriesToSeed = new BlogCategory[6];

            for(int i = 1; i < 7; i++)
            {
                categoriesToSeed[i - 1] = new BlogCategory
                {
                    CategoryId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Name = $"Category {i}",
                    Description = $"A description of category {i}"
                };
            }

            modelBuilder.Entity<BlogCategory>().HasData(categoriesToSeed);

            #endregion

            modelBuilder.Entity<Post>(
                entity =>
                {
                    entity.HasOne(post => post.Category)
                    .WithMany(category => category.Posts)
                    .HasForeignKey("CategoryId");
                });

            #region Posts seed

            Post[] postsToSeed = new Post[6];

            for (int i = 1; i < 7; i++)
            {
                string postTitle = string.Empty;
                int categoryId = 0;

                switch(i)
                {
                    case 1:
                        postTitle = "First Post";
                        categoryId = 1;
                        break;
                    case 2:
                        postTitle = "Second Post";
                        categoryId = 2;
                        break;
                    case 3:
                        postTitle = "Third Post";
                        categoryId = 3;
                        break;
                    case 4:
                        postTitle = "Fourth Post";
                        categoryId = 4;
                        break;
                    case 5:
                        postTitle = "Fifth Post";
                        categoryId = 5;
                        break;
                    case 6:
                        postTitle = "Sixth Post";
                        categoryId = 6;
                        break;
                    default:
                        break;
                }

                postsToSeed[i - 1] = new Post
                {
                    PostId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Title = postTitle,
                    Excerpt = $"This is the excerpt for post {i}",
                    Content = string.Empty,
                    PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm"),
                    Published = true,
                    Author = "Jimmy Purnell",
                    CategoryId = categoryId
                };
            }

            modelBuilder.Entity<Post>().HasData(postsToSeed);

            #endregion
            #region Project categories seed
            ProjectCategory[] projectCategoriesToSeed = new ProjectCategory[6];

            for (int i = 1; i < 7; i++)
            {
                projectCategoriesToSeed[i - 1] = new ProjectCategory
                {
                    ProjectCategoryId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Name = $"Category {i}",
                    Description = $"A description of category {i}"
                };
            }
            #endregion
            modelBuilder.Entity<ProjectCategory>().HasData(projectCategoriesToSeed);

            modelBuilder.Entity<Project>(
                entity =>
                {
                    entity.HasOne(project => project.Category)
                    .WithMany(category => category.Projects)
                    .HasForeignKey("ProjectCategoryId");
                });
            #region Seed projects
            Project[] projectsToSeed = new Project[6];
            for (int i = 1; i < 7; i++)
            {
                string projectTitle = string.Empty;
                int categoryId = 0;

                switch(i)
                {
                    case 1:
                        projectTitle = "First project";
                        categoryId = 1;
                        break;
                    case 2:
                        projectTitle = "Second project";
                        categoryId = 2;
                        break;
                    case 3:
                        projectTitle = "Thrid project";
                        categoryId = 3;
                        break;
                    case 4:
                        projectTitle = "Fourth project";
                        categoryId = 4;
                        break;
                    case 5:
                        projectTitle = "Fifth project";
                        categoryId = 5;
                        break;
                    case 6:
                        projectTitle = "Sixth project";
                        categoryId = 6;
                        break;
                    default:
                        break;
                }
                projectsToSeed[i - 1] = new Project
                {
                    ProjectId = i,
                    ScreenshotImagePath = "uploads/placeholder.jpg",
                    Name = projectTitle,
                    ProjectCategoryId = categoryId,
                    GitHub = "/",
                    URL = "/",
                    Description = $"This is the description for project {i}",
                    DescriptionExcerpt = $"This is the description excerpt for project {i}",
                    PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm")
                };
            }

            #endregion
            modelBuilder.Entity<Project>().HasData(projectsToSeed);
        }
    }
}
