public static class UserEndpoints
{

    const string port = "5052";

    // Auth URL
    public static string LoginURL = $"http://localhost:{port}/api/user/login";
    public static string RegisterURL = $"http://localhost:{port}/api/user/register";
    public static string NewUserURL = $"http://localhost:{port}/api/users";

    // Get URL
    public static string GetUserByIdURL = $"http://localhost:{port}/api/user/by-id";
    public static string GetUserByNameURL = $"http://localhost:{port}/api/user/by-name";

    // Read URL
    public static string AllStudentsURL = $"http://localhost:{port}/api/user/all-users";

    // Delete URL
    public static string DeleteUserURL = $"http://localhost:{port}/api/users";

    // Update URL
    public static string UpdateUserURL = $"http://localhost:{port}/api/users";

}