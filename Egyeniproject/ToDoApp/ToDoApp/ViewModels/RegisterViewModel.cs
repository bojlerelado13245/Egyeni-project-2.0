using System.Windows.Input;
using ToDoApp.Services;

namespace ToDoApp.ViewModels;

public class RegisterViewModel
{
    private readonly NavigationService _navigation;

    public RegisterViewModel(NavigationService navigation)
    {
        _navigation = navigation;

        GoToLoginCommand = new Command(async () =>
        {
            await _navigation.GoBack();
        });
    }

    public ICommand GoToLoginCommand { get; }
}