using ToDoApp.ViewModels;

namespace ToDoApp.Views;

public partial class ToDoPage : ContentPage
{
    public ToDoPage(ToDoPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}