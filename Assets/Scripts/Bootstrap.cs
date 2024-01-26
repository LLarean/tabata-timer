using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private ViewChanger _viewChanger;
    [Space]
    [SerializeField] private TimerView _timerView;
    [SerializeField] private ProgressBarView progressBarView;
    [SerializeField] private SettingsView _settingsView;

    private TimerPresenter _timerPresenter;
    private ProgressBarPresenter _progressBarPresenter;
    private SettingsPresenter _settingsPresenter;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        InitializePresenters();
        InstallDependencies();
        
        _viewChanger.ShowTimer();
    }

    private void OnDestroy()
    {
        _timerPresenter.Unsubscribe();
        _settingsPresenter.Unsubscribe();
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

        _timerPresenter = new TimerPresenter(timerModel, _timerView);
        _timerPresenter.SetData();
        _timerPresenter.Subsribe();
    }

    private void InitializeProgressBar()
    {
        ProgressBarModel progressBarModel = new ProgressBarModel();

        _progressBarPresenter = new ProgressBarPresenter(progressBarModel, progressBarView);
        _progressBarPresenter.ResetAnimation();
    }

    private void InitializeSettings()
    {
        SettingsModel settingsModel = new SettingsModel();
        _settingsPresenter = new SettingsPresenter(settingsModel, _settingsView);
        _settingsPresenter.Subscribe();
        _settingsPresenter.DisplayValue();
    }

    private TimerModel GetTimerModel()
    {
        SetDefaultValue();

        var numberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString());
        var sportsTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString());
        var timeBreaks = PlayerPrefs.GetInt(SettingsType.RestTime.ToString());

        TimerModel timerModel = new TimerModel(numberRounds, sportsTime, timeBreaks);
        return timerModel;
    }

    private void SetDefaultValue()
    {
        if (PlayerPrefs.HasKey(SettingsType.NumberRounds.ToString()) == true)
        {
            return;
        }

        PlayerPrefs.SetInt(SettingsType.NumberRounds.ToString(), DefaultSettingsValue.NumberRounds);
        PlayerPrefs.SetInt(SettingsType.TrainingTime.ToString(), DefaultSettingsValue.TrainingTime);
        PlayerPrefs.SetInt(SettingsType.RestTime.ToString(), DefaultSettingsValue.RestTime);
        PlayerPrefs.Save();
    }
    
    private void InstallDependencies()
    {
        _timerPresenter.SetProgressBar(_progressBarPresenter);
        _timerPresenter.SetAudioPlayer(_audioPlayer);
        _timerPresenter.SetViewChanger(_viewChanger);
        
        _settingsPresenter.SetViewChanger(_viewChanger);
        _settingsPresenter.SetAudioPlayer(_audioPlayer);
    }
}