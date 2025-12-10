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
        
    }

    public ICommand CancelNewTask { get; }
}