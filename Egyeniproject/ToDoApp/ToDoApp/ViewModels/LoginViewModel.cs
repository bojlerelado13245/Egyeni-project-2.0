using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Views;

namespace ToDoApp.ViewModels;

public class LoginViewModel
{
    private readonly NavigationService _navigation;
    private readonly HttpClient _httpClient;

    public LoginViewModel(NavigationService navigation)
    {
        _navigation = navigation;
        _httpClient = new HttpClient();

        GoToRegisterCommand = new Command(async () =>
        {
            await _navigation.NavigateTo(new Register(new RegisterViewModel(_navigation)));
        });

        LoginCommand = new Command(async () => await Login());
        
        
    }

    public string Username { get; set; }
    public string Password { get; set; }

    public ICommand GoToRegisterCommand { get; }
    public ICommand LoginCommand { get; }

    private async Task Login()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Fill all fields", "OK");
            return;
        }

        try
        {
        
            var users = await _httpClient.GetFromJsonAsync<List<User>>("http://localhost:5093/api/Users");

            var matched = users?.FirstOrDefault(u =>
                u.UserName.Equals(Username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == Password
            );

            if (matched == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
                return;
            }

            await Application.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");

           
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");
            var newData = new { id = matched.Id.ToString(), userName = matched.UserName };
            string json = JsonSerializer.Serialize(newData, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
            
            await _navigation.NavigateTo(new ToDoPage(new ToDoPageViewModel()));
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
