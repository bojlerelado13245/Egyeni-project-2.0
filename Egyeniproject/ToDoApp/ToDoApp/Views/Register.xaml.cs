using ToDoApp.ViewModels;

namespace ToDoApp.Views;

public partial class Register : ContentPage
{
    public Register(RegisterViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}