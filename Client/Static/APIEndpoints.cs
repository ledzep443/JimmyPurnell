namespace Client.Static
{
    internal static class APIEndpoints
    {
#if DEBUG
        internal const string ServerBaseUrl = "https://localhost:7245";
#else
        internal const string ServerBaseUrl = "https://jimmypurnellbackend.azurewebsites.net";
#endif

        internal readonly static string s_categories = $"{ServerBaseUrl}/api/blogcategories";
        internal readonly static string s_categoriesWithPosts = $"{ServerBaseUrl}/api/blogcategories/withposts";
        internal readonly static string s_projectCategories = $"{ServerBaseUrl}/api/projectcategories";
        internal readonly static string s_projectCategoriesWithProjects = $"{ServerBaseUrl}/api/projectcategories/withprojects";
        internal readonly static string s_posts = $"{ServerBaseUrl}/api/posts";
        internal readonly static string s_postsDTO = $"{ServerBaseUrl}/api/posts/dto";
        internal readonly static string s_projects = $"{ServerBaseUrl}/api/projects";
        internal readonly static string s_projectsDTO = $"{ServerBaseUrl}/api/projects/dto";
        internal readonly static string s_imageUpload = $"{ServerBaseUrl}/api/imageupload";
        internal readonly static string s_signIn = $"{ServerBaseUrl}/api/signin";
    }
}
