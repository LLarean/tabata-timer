public class SettingsPresenter
{
    private readonly SettingsModel _settingsModel;
    private readonly SettingsView _settingsView;
    
    private ViewChanger _viewChanger;

    public SettingsPresenter(SettingsModel settingsModel, SettingsView settingsView)
    {
        _settingsModel = settingsModel;
        _settingsView = settingsView;
    }

    public void Subscribe()
    {
        _settingsView.OnBackClicked += BackClicked;
    }

    public void Unsubscribe()
    {
        _settingsView.OnBackClicked += BackClicked;
    }

    public void SetViewChanger(ViewChanger viewChanger)
    {
        _viewChanger = viewChanger;
    }

    private void BackClicked()
    {
        _viewChanger.ShowTimer();
    }
}
