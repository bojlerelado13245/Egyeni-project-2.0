using ToDoApp.ViewModels;

namespace ToDoApp.Views;

public partial class CardCreate : ContentPage
{
    public CardCreate(CardCreateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}