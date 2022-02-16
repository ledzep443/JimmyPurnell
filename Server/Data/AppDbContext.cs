using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Project> Projects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }

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
        }
    }
}
