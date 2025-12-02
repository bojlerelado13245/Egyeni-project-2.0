using ToDoApp.ViewModels;

namespace ToDoApp.Views;

public partial class Login : ContentPage
{
    public Login(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}