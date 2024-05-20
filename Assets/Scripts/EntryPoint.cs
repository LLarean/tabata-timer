using EventBusSystem;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ViewHub _viewHub;
    
    private TimerPresenter _timerPresenter;
    private ProgressBarPresenter _progressBarPresenter;
    private SettingsPresenter _settingsPresenter;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        InitializePresenters();
        InstallDependencies();
        
        EventBus.RaiseEvent<IChangeViewHandler>(handler => handler.HandleShowTimer());
    }

    private void InitializePresenters()
    {
        InitializeTimer();
        InitializeProgressBar();
        InitializeSettings();
    }

    private void InitializeTimer()
    {
        TimerModel timerModel = GetTimerModel();

        _timerPresenter = new TimerPresenter(timerModel, _viewHub.TimerView);
        _timerPresenter.SetData();
        _timerPresenter.Subsribe();
    }

    private void InitializeProgressBar()
    {
        var progressBarModel = new ProgressBarModel();

        _progressBarPresenter = new ProgressBarPresenter(progressBarModel, _viewHub.ProgressBarView);
        _progressBarPresenter.ResetAnimation();
    }

    private void InitializeSettings()
    {
        var settingsModel = new SettingsModel();
        _settingsPresenter = new SettingsPresenter(settingsModel, _viewHub.SettingsView);
        _settingsPresenter.Subscribe();
        _settingsPresenter.DisplayValue();
    }

    private TimerModel GetTimerModel()
    {
        var numberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString(), DefaultSettingsValue.NumberRounds);
        var sportsTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString(), DefaultSettingsValue.TrainingTime);
        var timeBreaks = PlayerPrefs.GetInt(SettingsType.RestTime.ToString(), DefaultSettingsValue.RestTime);

        var timerModel = new TimerModel(numberRounds, sportsTime, timeBreaks);
        return timerModel;
    }

    private void InstallDependencies()
    {
        _timerPresenter.SetProgressBar(_progressBarPresenter);
    }
}