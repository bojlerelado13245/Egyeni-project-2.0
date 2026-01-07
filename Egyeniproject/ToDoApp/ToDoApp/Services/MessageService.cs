using System;
namespace ToDoApp.Services;

public static class MessengerService
{
    public static event Action RefreshRequested;

    public static void SendRefresh()
    {
        RefreshRequested?.Invoke();
    }
}