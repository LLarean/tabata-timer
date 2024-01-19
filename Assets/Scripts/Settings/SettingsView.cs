using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : View
{
    [SerializeField] private Button _back;
    [Space]
    [SerializeField] private SettingsItem _numberRounds;
    [SerializeField] private SettingsItem _trainingTime;
    [SerializeField] private SettingsItem _restTime;
    
    public event Action OnBackClicked;
    public event Action<SettingsType, int> OnSettingsChanged;
    
    public void DisplayValue(SettingsType settingsType, int value)
    {
        switch (settingsType)
        {
            case SettingsType.NumberRounds:
                _numberRounds.DisplayValue(value);
                break;
            case SettingsType.TrainingTime:
                _trainingTime.DisplayValue(value);
                break;
            case SettingsType.RestTime:
                _restTime.DisplayValue(value);
                break;
        }
    }
    
    private void Start()
    {
        _back.onClick.AddListener(BackClicked);

        _numberRounds.OnValueChanged += ValueChanged;
        _trainingTime.OnValueChanged += ValueChanged;
        _restTime.OnValueChanged += ValueChanged;
    }

    private void OnDestroy()
    {
        _back.onClick.RemoveAllListeners();
        
        _numberRounds.OnValueChanged -= ValueChanged;
        _trainingTime.OnValueChanged -= ValueChanged;
        _restTime.OnValueChanged -= ValueChanged;
    }

    private void BackClicked() => OnBackClicked?.Invoke();

    private void ValueChanged(SettingsType settingsType, int value) => OnSettingsChanged?.Invoke(settingsType, value);
}