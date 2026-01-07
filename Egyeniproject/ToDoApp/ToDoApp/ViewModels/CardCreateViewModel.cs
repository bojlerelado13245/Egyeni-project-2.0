using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Views;
namespace ToDoApp.ViewModels;

public class CardCreateViewModel
{
    private readonly NavigationService _navigation;
    private readonly HttpClient _httpClient;

    public CardCreateViewModel(NavigationService navigation)
    {
        _navigation = navigation;
        _httpClient = new HttpClient();

        CancelNewTask = new Command(async () =>
        {
            await _navigation.NavigateTo(new ToDoPage(new ToDoPageViewModel(_navigation)));
        });

        CreateNewTask = new Command(async () => await NewTask());

       
    }
    
    public void SaveCard()
    {
        MessengerService.SendRefresh();
    }

    public string _TaskName { get; set; }
    public string _TaskDesc { get; set; }
    public ICommand CancelNewTask { get; }
    public ICommand CreateNewTask { get; }
    
    private async Task NewTask()
    {
        
        if (string.IsNullOrWhiteSpace(_TaskName) || string.IsNullOrWhiteSpace(_TaskDesc) )
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Fill all fields", "OK");
            return;
        }

        string filePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");

        if (!File.Exists(filePath))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "user.json not found", "OK");
            return;
        }

        string json = File.ReadAllText(filePath);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        string userId = root.GetProperty("id").GetString();
        
        var task = new Tasks
        {
            TaskName = _TaskName,
            TaskDesc = _TaskDesc,
           
        };
        
        
        
        try
        {
            string url = $"http://localhost:5093/api/tasks/{userId}/tasks";


            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, task);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Task created!", "OK");
                SaveCard();
                await _navigation.NavigateTo(new ToDoPage(new ToDoPageViewModel(_navigation)));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to create task. Status: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
        }

    }
    
    
    
    
   
}