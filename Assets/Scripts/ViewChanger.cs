using UnityEngine;

public class ViewChanger : MonoBehaviour
{
    [SerializeField] private TimerView _timerView;
    [SerializeField] private SettingsView _settingsView;

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