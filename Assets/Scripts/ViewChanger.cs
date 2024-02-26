public class ViewChanger
{
    private readonly TimerView _timerView;
    private readonly SettingsView _settingsView;

    public ViewChanger(TimerView timerView, SettingsView settingsView)
    {
        _timerView = timerView;
        _settingsView = settingsView;
    }
    
    public void ShowTimer()
    {
        _timerView.Show();
        _settingsView.Hide();
    }

    public void ShowSettings()
    {
        _timerView.Hide();
        _settingsView.Show();
    }
}