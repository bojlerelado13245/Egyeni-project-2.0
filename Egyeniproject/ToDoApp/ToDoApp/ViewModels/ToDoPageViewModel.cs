using System.Net.Http.Json;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Views;
using System.Collections.ObjectModel;

namespace ToDoApp.ViewModels;

public class ToDoPageViewModel
{
    private readonly NavigationService _navigation;
    private readonly HttpClient _httpClient;
    public ObservableCollection<TaskModel> Tasks { get; set; } = new();

    public ICommand NewTask { get; }

    public ToDoPageViewModel(NavigationService navigation)
    {
        _navigation = navigation;
        _httpClient = new HttpClient();

        NewTask = new Command(async () =>
        {
            await _navigation.NavigateTo(new CardCreate(new CardCreateViewModel(_navigation)));
        });

        MessengerService.RefreshRequested += OnRefreshRequested;

       
        RefreshCards();
    }

    private void OnRefreshRequested()
    {
        RefreshCards();
    }

    private async Task RefreshCards()
    {
        var tasks = await _httpClient.GetFromJsonAsync<List<Tasks>>("http://localhost:5093/api/Tasks");

        if (tasks == null)
            return;

        Tasks.Clear();

        foreach (var t in tasks)
        {
            var task = new TaskModel
            {
                Id = t.Id,
                OwnerId = t.OwnerId,
                Title = t.TaskName,
                Description = t.TaskDesc,
                IsDone = t.IsDone, 
            
                DoneCommand = new Command<TaskModel>(OnDone),
                DeleteCommand = new Command<TaskModel>(OnDelete)
            };

            Tasks.Add(task);
        }
    }




    private async void OnDone(TaskModel task)
    {
        if (task == null) return;
        
        await _httpClient.PatchAsync($"http://localhost:5093/api/tasks/{task.Id}/done", null);
        
        task.IsDone = true;
    }


    private async void OnDelete(TaskModel task)
    {
        if (task == null) return;
        
        await _httpClient.DeleteAsync($"http://localhost:5093/api/tasks/{task.Id}");
        
        Tasks.Remove(task);
    }

}
