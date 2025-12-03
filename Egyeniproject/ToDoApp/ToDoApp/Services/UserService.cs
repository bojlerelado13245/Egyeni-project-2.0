using System.Net.Http.Json;
using System.Text.Json;
using ToDoApp.Models;

namespace ToDoApp.Services;

public class UserService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<List<User>> GetUsersAsync()
    {
        string url = "http://localhost:5093/api/Users";
        return await _httpClient.GetFromJsonAsync<List<User>>(url) ?? new List<User>();
    }

    public async Task SaveUserToLocalFile(User user)
    {
        string filePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");

        var newData = new
        {
            id = user.Id.ToString(),
            userName = user.UserName
        };

        string json = JsonSerializer.Serialize(newData, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }
}