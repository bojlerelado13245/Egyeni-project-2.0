using System.Windows.Input;
using ToDoApp.Services;
using ToDoApp.Views;

namespace ToDoApp.ViewModels;

public class LoginViewModel
{
    private readonly NavigationService _navigation;

    public LoginViewModel(NavigationService navigation)
    {
        _navigation = navigation;

        GoToRegisterCommand = new Command(async () =>
        {
            await _navigation.NavigateTo(new Register(new RegisterViewModel(_navigation)));
        });
    }

    public ICommand GoToRegisterCommand { get; }
}