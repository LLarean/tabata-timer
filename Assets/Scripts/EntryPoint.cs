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
        
        _viewHub.ShowTimer();
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
        ProgressBarModel progressBarModel = new ProgressBarModel();

        _progressBarPresenter = new ProgressBarPresenter(progressBarModel, _viewHub.ProgressBarView);
        _progressBarPresenter.ResetAnimation();
    }

    private void InitializeSettings()
    {
        SettingsModel settingsModel = new SettingsModel();
        _settingsPresenter = new SettingsPresenter(settingsModel, _viewHub.SettingsView);
        _settingsPresenter.Subscribe();
        _settingsPresenter.DisplayValue();
    }

    private TimerModel GetTimerModel()
    {
        var numberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString(), DefaultSettingsValue.NumberRounds);
        var sportsTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString(), DefaultSettingsValue.TrainingTime);
        var timeBreaks = PlayerPrefs.GetInt(SettingsType.RestTime.ToString(), DefaultSettingsValue.RestTime);

        TimerModel timerModel = new TimerModel(numberRounds, sportsTime, timeBreaks);
        return timerModel;
    }

    private void InstallDependencies()
    {
        _timerPresenter.SetViewChanger(_viewHub);
        _timerPresenter.SetProgressBar(_progressBarPresenter);
        
        _settingsPresenter.SetViewChanger(_viewHub);
    }
}