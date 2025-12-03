using Microsoft.Maui.Controls;

namespace ToDoApp.Services;

public class NavigationService
{
    public async Task NavigateTo(Page page)
    {
        await Application.Current.MainPage.Navigation.PushAsync(page);
    }

    public void SetMainPage(Page page)
    {
        Application.Current.MainPage = new NavigationPage(page);
    }

    public async Task GoBack()
    {
        if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}