using UnityEngine;

public class SettingsPresenter
{
    private readonly SettingsModel _settingsModel;
    private readonly SettingsView _settingsView;
    
    private ViewChanger _viewChanger;
    private AudioPlayer _audioPlayer;

    public SettingsPresenter(SettingsModel settingsModel, SettingsView settingsView)
    {
        _settingsModel = settingsModel;
        _settingsView = settingsView;
    }

    public void Subscribe()
    {
        _settingsView.OnBackClicked += BackClicked;
        _settingsView.OnSettingsChanged += SettingsChanged;
    }
    
    public void Unsubscribe()
    {
        _settingsView.OnBackClicked -= BackClicked;
        _settingsView.OnSettingsChanged -= SettingsChanged;
    }
    
    public void DisplayValue()
    {
        var numberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString(), DefaultSettingsValue.NumberRounds);
        var trainingTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString(), DefaultSettingsValue.TrainingTime);
        var restTime = PlayerPrefs.GetInt(SettingsType.RestTime.ToString(), DefaultSettingsValue.RestTime);
        
        _settingsView.DisplayValue(SettingsType.NumberRounds, numberRounds);
        _settingsView.DisplayValue(SettingsType.TrainingTime, trainingTime);
        _settingsView.DisplayValue(SettingsType.RestTime, restTime);
    }

    public void SetViewChanger(ViewChanger viewChanger) => _viewChanger = viewChanger;
    
    public void SetAudioPlayer(AudioPlayer audioPlayer) => _audioPlayer = audioPlayer;

    private void BackClicked()
    {
        _audioPlayer.PlayTap();
        _viewChanger.ShowTimer();
    }
    
    private void SettingsChanged(SettingsType settingsType, int value)
    {
        _audioPlayer.PlayTap();

        value = settingsType switch
        {
            SettingsType.NumberRounds => GetNumberRounds(value),
            SettingsType.TrainingTime => GetTrainingTime(value),
            SettingsType.RestTime => GetRestTime(value),
            _ => value
        };

        PlayerPrefs.SetInt($"{settingsType}", value);
        PlayerPrefs.Save();

        _settingsView.DisplayValue(settingsType, value);
        UpdateSettingsModel();
    }

    private void UpdateSettingsModel()
    {
        _settingsModel.NumberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString(), DefaultSettingsValue.NumberRounds);
        _settingsModel.TrainingTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString(), DefaultSettingsValue.TrainingTime);
        _settingsModel.RestTime = PlayerPrefs.GetInt(SettingsType.RestTime.ToString(), DefaultSettingsValue.RestTime);
    }

    private static int GetNumberRounds(int value)
    {
        value = value switch
        {
            < ExtremeValues.MinimalRounds => ExtremeValues.MinimalRounds,
            > ExtremeValues.MaximumRounds => ExtremeValues.MaximumRounds,
            _ => value
        };

        return value;
    }

    private static int GetTrainingTime(int value)
    {
        value = value switch
        {
            < ExtremeValues.MinimalSport => ExtremeValues.MinimalSport,
            > ExtremeValues.MaximumSport => ExtremeValues.MaximumSport,
            _ => value
        };

        return value;
    }

    private static int GetRestTime(int value)
    {
        value = value switch
        {
            < ExtremeValues.MinimalTieBreak => ExtremeValues.MinimalTieBreak,
            > ExtremeValues.MaximumTieBreak => ExtremeValues.MaximumTieBreak,
            _ => value
        };

        return value;
    }
}
