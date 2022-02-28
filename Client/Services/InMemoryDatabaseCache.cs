
using Client.Static;
using Shared.Models;
using System.Net.Http.Json;

namespace Client.Services
{
    public class InMemoryDatabaseCache
    {
        private readonly HttpClient _httpClient;

        public InMemoryDatabaseCache(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #region categories
        private List<BlogCategory> _categories = null;
        internal List<BlogCategory> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                _categories = value;
                NotifyCategoriesDataChanged();
            }
        }

        internal async Task<BlogCategory> GetCategoryByCategoryId(int categoryId, bool withPosts)
        {
            if (_categories == null)
            {
                await GetCategoriesFromDatabaseAndCache(withPosts);
            }

            BlogCategory categoryToReturn = _categories.First(category => category.CategoryId == categoryId);

            if(categoryToReturn.Posts == null && withPosts == true)
            {
                categoryToReturn = await _httpClient.GetFromJsonAsync<BlogCategory>($"{ APIEndpoints.s_categoriesWithPosts}/{ categoryToReturn.CategoryId}");
            }

            return categoryToReturn;
        }

        internal async Task<BlogCategory> GetCategoryByCategoryName(string categoryName, bool withPosts, bool nameToLowerFromUrl)
        {
            if (_categories == null)
            {
                await GetCategoriesFromDatabaseAndCache(withPosts);
            }

            BlogCategory categoryToReturn = null;

            if (nameToLowerFromUrl == true)
            {
                categoryToReturn = _categories.First(category => category.Name.ToLowerInvariant() == categoryName);
            }
            else
            {
                categoryToReturn = _categories.First(category => category.Name == categoryName);
            }

            if (categoryToReturn.Posts == null && withPosts == true)
            {
                categoryToReturn = await _httpClient.GetFromJsonAsync<BlogCategory>($"{APIEndpoints.s_categoriesWithPosts}/{categoryToReturn.CategoryId}");
            }

            return categoryToReturn;
        }

        private bool _gettingCategoriesFromDatabaseAndCaching = false;
        internal async Task GetCategoriesFromDatabaseAndCache(bool withPosts)
        {
            // Only allow one Get request to run at a time.
            if (_gettingCategoriesFromDatabaseAndCaching == false)
            {
                _gettingCategoriesFromDatabaseAndCaching = true;

                List<BlogCategory> categoriesFromDatabase = null;

                if (_categories != null)
                {
                    _categories = null;
                }

                if (withPosts == true)
                {
                    categoriesFromDatabase = await _httpClient.GetFromJsonAsync<List<BlogCategory>>(APIEndpoints.s_categoriesWithPosts);
                }
                else
                {
                    categoriesFromDatabase = await _httpClient.GetFromJsonAsync<List<BlogCategory>>(APIEndpoints.s_categories);
                }

                _categories = categoriesFromDatabase.OrderByDescending(category => category.CategoryId).ToList();

                if (withPosts == true)
                {
                    List<Post> postsFromCategories = new List<Post>();

                    foreach (var category in _categories)
                    {
                        if (category.Posts.Count != 0)
                        {
                            foreach (var post in category.Posts)
                            {
                                BlogCategory postCategoryWithoutPosts = new BlogCategory
                                {
                                    CategoryId = category.CategoryId,
                                    ThumbnailImagePath = category.ThumbnailImagePath,
                                    Name = category.Name,
                                    Description = category.Description,
                                    Posts = null
                                };

                                post.Category = postCategoryWithoutPosts;

                                postsFromCategories.Add(post);
                            }
                        }
                    }

                    _posts = postsFromCategories.OrderByDescending(post => post.PostId).ToList();
                }

                _gettingCategoriesFromDatabaseAndCaching = false;
            }
        }


        internal event Action OnCategoriesDataChanged;
        private void NotifyCategoriesDataChanged() => OnCategoriesDataChanged?.Invoke();

        #endregion
        #region Posts

        private List<Post> _posts = null;
        internal List<Post> Posts
        {
            get
            {
                return _posts;
            }
            set
            {
                _posts = value;
                NotifyPostsDataChanged();
            }
        }

        internal async Task<Post> GetPostByPostId(int postId)
        {
            if (_posts == null)
            {
                await GetPostsFromDatabaseAndCache();
            }

            return _posts.First(post => post.PostId == postId);
        }

        internal async Task<PostDTO> GetPostDTOByPostId(int postId) => await _httpClient.GetFromJsonAsync<PostDTO>($"{APIEndpoints.s_postsDTO}/{postId}");

        private bool _gettingPostsFromDatabaseAndCaching = false;
        internal async Task GetPostsFromDatabaseAndCache()
        {
            // Only allow one Get to run at a time
            if (_gettingPostsFromDatabaseAndCaching == false)
            {
                _gettingPostsFromDatabaseAndCaching = true;

                if (_posts != null)
                {
                    _posts = null;
                }

                List<Post> postsFromDatabase = await _httpClient.GetFromJsonAsync<List<Post>>(APIEndpoints.s_posts);

                _posts = postsFromDatabase.OrderByDescending(post => post.PostId).ToList();

                _gettingPostsFromDatabaseAndCaching = false;
            }
        }

        internal event Action OnPostsDataChanged;
        private void NotifyPostsDataChanged() => OnPostsDataChanged?.Invoke();

        #endregion
        #region ProjectCategories
        private List<ProjectCategory> _projectCategories = null;
        internal List<ProjectCategory> ProjectCategories
        {
            get
            {
                return _projectCategories;
            }

            set
            {
                _projectCategories = value;
                NotifyProjectCategoriesDataChanged();
            }
        }

        internal async Task<ProjectCategory> GetProjectCategoryByCategoryId(int categoryId, bool withProjects)
        {
            if(_projectCategories == null)
            {
                await GetProjectCategoriesFromDatabaseAndCache(withProjects);
            }

            ProjectCategory projectCategoryToReturn = _projectCategories.First(category => category.ProjectCategoryId == categoryId);

            if(projectCategoryToReturn.Projects == null && withProjects == true)
            {
                projectCategoryToReturn = await _httpClient.GetFromJsonAsync<ProjectCategory>($"{APIEndpoints.s_projectCategories}/{projectCategoryToReturn.ProjectCategoryId}");
            }
            return projectCategoryToReturn;
        }

        internal async Task<ProjectCategory> GetProjectCategoryByCategoryName(string categoryName, bool withProjects, bool nameToLowerFromUrl)
        {
            if (_projectCategories == null)
            {
                await GetProjectCategoriesFromDatabaseAndCache(withProjects);
            }

            ProjectCategory categoryToReturn = null;

            if(nameToLowerFromUrl == true)
            {
                categoryToReturn = _projectCategories.First(category => category.Name.ToLowerInvariant() == categoryName);
            }
            else
            {
                categoryToReturn = _projectCategories.First(category => category.Name == categoryName);
            }

            if (categoryToReturn.Projects == null && withProjects == true)
            {
                categoryToReturn = await _httpClient.GetFromJsonAsync<ProjectCategory>($"{APIEndpoints.s_projectCategoriesWithProjects}/{categoryToReturn.ProjectCategoryId}");
            }

            return categoryToReturn;
        }

        private bool _gettingProjectCategoriesFromDatabaseAndCaching = false;
        internal async Task GetProjectCategoriesFromDatabaseAndCache(bool withProjects)
        {
            // Only allow one Get request to run at a time.
            if (_gettingProjectCategoriesFromDatabaseAndCaching == false)
            {
                _gettingProjectCategoriesFromDatabaseAndCaching = true;

                List<ProjectCategory> categoriesFromDatabase = null;

                if (_projectCategories != null)
                {
                    _projectCategories = null;
                }

                if (withProjects == true)
                {
                    categoriesFromDatabase = await _httpClient.GetFromJsonAsync<List<ProjectCategory>>(APIEndpoints.s_projectCategoriesWithProjects);
                }
                else
                {
                    categoriesFromDatabase = await _httpClient.GetFromJsonAsync<List<ProjectCategory>>(APIEndpoints.s_projectCategories);
                }

                _projectCategories = categoriesFromDatabase.OrderByDescending(category => category.ProjectCategoryId).ToList();

                if (withProjects == true)
                {
                    List<Project> projectsFromCategories = new List<Project>();

                    foreach (var category in _projectCategories)
                    {
                        if (category.Projects.Count != 0)
                        {
                            foreach (var project in category.Projects)
                            {
                                ProjectCategory postCategoryWithoutPosts = new ProjectCategory
                                {
                                    ProjectCategoryId = category.ProjectCategoryId,
                                    ThumbnailImagePath = category.ThumbnailImagePath,
                                    Name = category.Name,
                                    Description = category.Description,
                                    Projects = null
                                };

                                project.Category = postCategoryWithoutPosts;

                                projectsFromCategories.Add(project);
                            }
                        }
                    }

                    _projects = projectsFromCategories.OrderByDescending(project => project.ProjectId).ToList();
                }

                _gettingCategoriesFromDatabaseAndCaching = false;
            }
        }


        internal event Action OnProjectCategoriesDataChanged;
        private void NotifyProjectCategoriesDataChanged() => OnCategoriesDataChanged?.Invoke();
        #endregion
        #region Projects
        private List<Project> _projects = null;
        public List<Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
                NotifyProjectDataChanged();
            }
        }

        internal async Task<Project> GetProjectByProjectId(int projectId)
        {
            if (_projects == null)
            {
                await GetProjectsFromDatabaseAndCache();
            }
            return _projects.First(project => project.ProjectId == projectId);
        }

        internal async Task<ProjectDTO> GetProjectDTOByProjectId(int projectId) => await _httpClient.GetFromJsonAsync<ProjectDTO>($"{APIEndpoints.s_projectsDTO}/{projectId}");

        private bool _gettingProjectsFromDatabaseAndCaching = false;
        internal async Task GetProjectsFromDatabaseAndCache()
        {
            if (_gettingProjectsFromDatabaseAndCaching == false)
            {
                _gettingProjectsFromDatabaseAndCaching = true;
                
                if (_projects != null)
                {
                    _projects = null;
                }

                List<Project> projectsFromDatabase = await _httpClient.GetFromJsonAsync<List<Project>>(APIEndpoints.s_projects);

                _projects = projectsFromDatabase.OrderByDescending(project => project.ProjectId).ToList();

                _gettingProjectsFromDatabaseAndCaching = false;
            }
        }

        internal event Action OnProjectDataChanged;
        private void NotifyProjectDataChanged() => OnProjectDataChanged?.Invoke();
        #endregion
    }
}
