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
}