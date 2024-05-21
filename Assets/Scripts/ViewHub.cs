using EventBusSystem;
using NaughtyAttributes;
using UnityEngine;

public class ViewHub : MonoBehaviour, IChangeViewHandler
{
    [SerializeField] private TimerView _timerView;
    [SerializeField] private ProgressBarView _progressBarView;
    [SerializeField] private SettingsView _settingsView;

    public TimerView TimerView => _timerView;
    public ProgressBarView ProgressBarView => _progressBarView;
    public SettingsView SettingsView => _settingsView;

    public void HandleShowTimer() => ShowTimer();

    public void HandleShowSettings() => ShowSettings();

    private void Awake() => EventBus.Subscribe(this);

    private void OnDestroy() => EventBus.Unsubscribe(this);
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) == true && _settingsView.gameObject.activeSelf == true)
        {
            ShowTimer();
        }
    }
    
    private void ShowTimer()
    {
        _timerView.Show();
        _settingsView.Hide();
    }

    private void ShowSettings()
    {
        _timerView.Hide();
        _settingsView.Show();
    }

    [Button]
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