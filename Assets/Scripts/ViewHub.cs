using EventBusSystem;
using UnityEngine;

public class ViewHub : MonoBehaviour
{
    [SerializeField] private TimerView _timerView;
    [SerializeField] private ProgressBarView _progressBarView;
    [SerializeField] private SettingsView _settingsView;

    public TimerView TimerView => _timerView;
    public ProgressBarView ProgressBarView => _progressBarView;
    public SettingsView SettingsView => _settingsView;

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
    
    private void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Escape) == true && _settingsView.gameObject.activeSelf == true)
        {
            ShowTimer();
        }
    }

    [ContextMenu("SetReferences")]
    private void SetReferences()
    {
        var timerViews = FindObjectsOfType<TimerView>();

        if (timerViews.Length == 1)
        {
            _timerView = timerViews[0];
        }
        
        var progressBarViews = FindObjectsOfType<ProgressBarView>();

        if (progressBarViews.Length == 1)
        {
            _progressBarView = progressBarViews[0];
        }
        
        var settingsViews = FindObjectsOfType<SettingsView>();

        if (settingsViews.Length == 1)
        {
            _settingsView = settingsViews[0];
        }
    }
}