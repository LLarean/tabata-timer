using Assets.SimpleLocalization.Scripts;
using EventBusSystem;
using UnityEngine;

public class SettingsPresenter
{
    private readonly SettingsModel _settingsModel;
    private readonly SettingsView _settingsView;

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
    
    public void DisplayValue()
    {
        var numberLanguage = PlayerPrefs.GetInt(SettingsType.Language.ToString(), DefaultSettingsValue.NumberLanguage);
        var numberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString(), DefaultSettingsValue.NumberRounds);
        var trainingTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString(), DefaultSettingsValue.TrainingTime);
        var restTime = PlayerPrefs.GetInt(SettingsType.RestTime.ToString(), DefaultSettingsValue.RestTime);
        
        _settingsView.DisplayValue(SettingsType.Language, numberLanguage);
        _settingsView.DisplayValue(SettingsType.NumberRounds, numberRounds);
        _settingsView.DisplayValue(SettingsType.TrainingTime, trainingTime);
        _settingsView.DisplayValue(SettingsType.RestTime, restTime);
    }

    private void BackClicked()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());
        EventBus.RaiseEvent<IChangeViewHandler>(handler => handler.HandleShowTimer());
    }
    
    private void SettingsChanged(SettingsType settingsType, int value)
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());

        value = settingsType switch
        {
            SettingsType.Language => GetNumberLanguage(value),
            SettingsType.NumberRounds => GetNumberRounds(value),
            SettingsType.TrainingTime => GetTrainingTime(value),
            SettingsType.RestTime => GetRestTime(value),
            _ => value
        };

        PlayerPrefs.SetInt($"{settingsType}", value);
        PlayerPrefs.Save();

        UpdateLanguage(settingsType, value);
        _settingsView.DisplayValue(settingsType, value);
        UpdateSettingsModel();
    }

    private void UpdateSettingsModel()
    {
        _settingsModel.NumberRounds = PlayerPrefs.GetInt(SettingsType.Language.ToString(), DefaultSettingsValue.NumberLanguage);
        _settingsModel.NumberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString(), DefaultSettingsValue.NumberRounds);
        _settingsModel.TrainingTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString(), DefaultSettingsValue.TrainingTime);
        _settingsModel.RestTime = PlayerPrefs.GetInt(SettingsType.RestTime.ToString(), DefaultSettingsValue.RestTime);
    }
    
    private static int GetNumberLanguage(int value)
    {
        value = value switch
        {
            < ExtremeValues.MinimulLanguage => ExtremeValues.MinimulLanguage,
            > ExtremeValues.MaximumLanguage => ExtremeValues.MaximumLanguage,
            _ => value
        };

        return value;
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
    
    private void UpdateLanguage(SettingsType settingsType, int value)
    {
        if (settingsType != SettingsType.Language)
        {
            return;
        }

        LocalizationManager.Language = value switch
        {
            0 => "Russian",
            1 => "English",
            _ => "English"
        };
    }
}
