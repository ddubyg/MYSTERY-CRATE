namespace MysteryCrate.Services;

public class ToastMessage
{
    public string Id { get; } = Guid.NewGuid().ToString("N")[..8];
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "success"; // success, info, warning, error
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}

public class NotificationService
{
    private readonly List<ToastMessage> _toasts = new();

    public event Action? OnChange;

    public IReadOnlyList<ToastMessage> Toasts => _toasts;

    public void ShowToast(string message, string type = "success")
    {
        var toast = new ToastMessage { Message = message, Type = type };
        _toasts.Add(toast);
        NotifyStateChanged();

        // Automatically dismiss toast after 4 seconds
        _ = Task.Run(async () =>
        {
            await Task.Delay(4000);
            DismissToast(toast.Id);
        });
    }

    public void DismissToast(string id)
    {
        var toast = _toasts.FirstOrDefault(t => t.Id == id);
        if (toast != null)
        {
            _toasts.Remove(toast);
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

