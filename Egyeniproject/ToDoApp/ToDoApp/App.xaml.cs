using ToDoApp.Views;
using ToDoApp.ViewModels;
using ToDoApp.Services;

namespace ToDoApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var navigationService = new NavigationService();
        var loginVM = new LoginViewModel(navigationService);
        MainPage = new NavigationPage(new Login(loginVM));
    }
}