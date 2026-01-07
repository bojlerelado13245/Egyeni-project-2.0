using System.Net.Http.Json;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Views;
namespace ToDoApp.ViewModels;

public class RegisterViewModel
{
    private readonly NavigationService _navigation;
    
    private readonly HttpClient _httpClient;
    public RegisterViewModel(NavigationService navigation)
    {
        _navigation = navigation;
        
        _httpClient = new HttpClient(); 

         /// itt adom meg hogy ha a gombra ranyomok mit hivjon meg/mi tortenjen
      
        GoToLoginCommand = new Command(async () =>
        {
            await _navigation.GoBack();
        });
        
        RegisterCommand = new Command(async () => await Register()); //pl ha erre ranyomok akkor meghivom a Register() functiont
    }

    //itt adom bele a bindingot a scriptnek
    
    public string _Email { get; set; }  // itt azert nem hasznalok ICommand ot mert ez nem egy gomb ami parancsod ad lenyomaskor hanem szimplan valtozokent megadom egy mezo tartalmat
    public string _Username { get; set; }
    public string _Password { get; set; }
    public ICommand GoToLoginCommand { get; }
    public ICommand RegisterCommand { get; }
    
    
    private async Task Register()
    {
        if (string.IsNullOrWhiteSpace(_Username) || string.IsNullOrWhiteSpace(_Password) || string.IsNullOrWhiteSpace(_Email))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Fill all fields", "OK");
            return;
        }

        var user = new User
        {
            UserName = _Username,
            Email = _Email,
            Password = _Password,
        };

            try
            {
                string url = "http://localhost:5093/api/Users";

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, user);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "User registered!", "OK");
                    await _navigation.NavigateTo(new Login(new LoginViewModel(_navigation)));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to add user. Status: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
            }

    }
    
    
    
    
}