namespace ToDoApp.Services;

public class NavigationService
{
    public async Task NavigateTo(Page page)
    {
        await Application.Current.MainPage.Navigation.PushAsync(page);
    }

    public async Task GoBack()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}