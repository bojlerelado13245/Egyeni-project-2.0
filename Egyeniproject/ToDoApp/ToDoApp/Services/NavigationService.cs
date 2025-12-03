using Microsoft.Maui.Controls;

namespace ToDoApp.Services;

public class NavigationService
{
    public async Task NavigateTo(Page page)  //arra ha kifejezetten arra a pagere akarunk menni
    {
        await Application.Current.MainPage.Navigation.PushAsync(page);
    }

    public void SetMainPage(Page page)
    {
        Application.Current.MainPage = new NavigationPage(page);
    }

    public async Task GoBack() //azert jo a goback mert sokszor nem kell megirni kulon hogy hova vigyen hanem csak visszabb megy egyel
    {
        if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}