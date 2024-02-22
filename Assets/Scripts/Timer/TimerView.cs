using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : View
{
    [Space]
    [SerializeField] private Button _settings;
    [SerializeField] private TMP_Text _seconds;
    [SerializeField] private TMP_Text _roundCounter;
    [SerializeField] private TMP_Text _status;
    [Space]
    [SerializeField] private Button _startStop;
    [SerializeField] private TMP_Text _startStopLabel;
    [SerializeField] private Button _reset;

    private Coroutine _coroutine = null;
    private bool _isRunning = false;
    private float _updateFrequency = 1f;

    public event Action OnSettingsClicked;
    public event Action OnStartStopClicked;
    public event Action OnResetClicked;
    public event Action OnTimerChanged;

    public void SetUpdateFrequency(float updateFrequency) => _updateFrequency = updateFrequency;

    public void SetRound(int currentRounds, int numberRounds) => _roundCounter.text = $"{currentRounds}/{numberRounds}";

    public void StartTimer()
    {
        _isRunning = true;
        _coroutine = StartCoroutine(Timer());
        _startStopLabel.text = GlobalStrings.Stop;
    }

    public void StopTimer()
    {
        _isRunning = false;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _startStopLabel.text = GlobalStrings.Start;
        SetStatus(GlobalStrings.Pause);
    }

    public void ResetTimer()
    {
        StopTimer();
        _startStopLabel.text = GlobalStrings.Start;
        _seconds.text = "00";
    }

    public void DisplayTime(float timeToDisplay) => _seconds.text = $"{timeToDisplay:00}";
    
    public void SetStatus(string timerStatus) => _status.text = timerStatus;
    
    private void Start()
    {
        _settings.onClick.AddListener(ClickSettings);
        _startStop.onClick.AddListener(ClickStartStop);
        _reset.onClick.AddListener(ClickReset);
        ResetDisplayedData();
    }

    private void OnDestroy()
    {
        _settings.onClick.RemoveAllListeners();
        _startStop.onClick.RemoveAllListeners();
        _reset.onClick.RemoveAllListeners();
    }

    private void ClickSettings() => OnSettingsClicked?.Invoke();

    private void ClickStartStop() => OnStartStopClicked?.Invoke();

    private void ClickReset() => OnResetClicked?.Invoke();

    private void ResetDisplayedData()
    {
        _startStopLabel.text = GlobalStrings.Start;
        _roundCounter.text = String.Empty;
        _seconds.text = "00";
    }

    private IEnumerator Timer()
    {
        while (_isRunning == true)
        {
            OnTimerChanged?.Invoke();
            yield return new WaitForSeconds(_updateFrequency);
        }
    }
}