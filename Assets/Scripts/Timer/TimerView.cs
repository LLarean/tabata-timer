using System;
using System.Collections;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : View
{
    [Space]
    [SerializeField] private Button _settings;
    [SerializeField] private TMP_Text _seconds;
    [SerializeField] private TMP_Text _rounds;
    [SerializeField] private TMP_Text _status;
    [Space]
    [SerializeField] private Button _start;
    [SerializeField] private TMP_Text _startLabel;
    [SerializeField] private Button _reset;

    private Coroutine _coroutine = null;
    private bool _isRunning = false;
    private float _updateFrequency = 1f;
    private float _timeBreaks = 1f;

    public event Action OnSettingsClicked;
    public event Action OnStartClicked;
    public event Action OnResetClicked;
    public event Action OnTimerUpdated;

    public void SetUpdateFrequency(float updateFrequency) => _updateFrequency = updateFrequency;

    public void DisplayRounds(int currentRounds, int numberRounds) => _rounds.text = $"{currentRounds}/{numberRounds}";
    
    public void SetTimeBreaks(int timeBreaks)
    {
        _timeBreaks = timeBreaks;
        _seconds.text = $"{_timeBreaks:00}";
    }

    public void StartTimeCounting()
    {
        _isRunning = true;
        _coroutine = StartCoroutine(TimeCounting());
        _startLabel.text = LocalizationManager.Localize(LocalizationKeys.Stop);
    }

    public void StopTimeCounting()
    {
        _isRunning = false;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _startLabel.text = LocalizationManager.Localize(LocalizationKeys.Start);
        DisplayStatus(LocalizationManager.Localize(LocalizationKeys.Pause));
    }

    public void ResetTimeCounting()
    {
        StopTimeCounting();
        _startLabel.text = LocalizationManager.Localize(LocalizationKeys.Start);
        _seconds.text = $"{_timeBreaks:00}";
    }

    public void DisplayTime(float timeToDisplay) => _seconds.text = $"{timeToDisplay:00}";
    
    public void DisplayStatus(string timerStatus) => _status.text = timerStatus;
    
    private void Start()
    {
        _settings.onClick.AddListener(ClickSettings);
        _start.onClick.AddListener(ClickStart);
        _reset.onClick.AddListener(ClickReset);
        ResetDisplayedData();

        LocalizationManager.OnLocalizationChanged += ChangeLocalization;
    }

    private void ChangeLocalization()
    {
        _startLabel.text = LocalizationManager.Localize(LocalizationKeys.Start);
        DisplayStatus(LocalizationManager.Localize(LocalizationKeys.Pause));
    }

    private void ClickSettings() => OnSettingsClicked?.Invoke();

    private void ClickStart() => OnStartClicked?.Invoke();

    private void ClickReset() => OnResetClicked?.Invoke();

    private void ResetDisplayedData()
    {
        _startLabel.text = LocalizationManager.Localize(LocalizationKeys.Start);
        _rounds.text = String.Empty;
        _seconds.text = $"{_timeBreaks:00}";
    }

    private IEnumerator TimeCounting()
    {
        while (_isRunning == true)
        {
            OnTimerUpdated?.Invoke();
            yield return new WaitForSeconds(_updateFrequency);
        }
    }
}