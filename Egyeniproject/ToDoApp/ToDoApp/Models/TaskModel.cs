using System.ComponentModel;
using System.Windows.Input;


public class TaskModel : INotifyPropertyChanged
{
    private bool _isDone;

    public Guid Id { get; set; }
    
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public bool IsDone
    {
        get => _isDone;
        set
        {
            if (_isDone == value) return;
            _isDone = value;
            OnPropertyChanged(nameof(IsDone));
        }
    }

    public ICommand DoneCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
