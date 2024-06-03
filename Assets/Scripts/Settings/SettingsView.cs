using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : View
{
    [Space]
    [SerializeField] private Button _back;
    [Space]
    [SerializeField] private SettingsItem _language;
    [SerializeField] private SettingsItem _numberRounds;
    [SerializeField] private SettingsItem _trainingTime;
    [SerializeField] private SettingsItem _restTime;
    [Space]
    [SerializeField] private TMP_Text _version;
    
    public event Action OnBackClicked;
    public event Action<SettingsType, int> OnSettingsChanged;
    
    public void DisplayValue(SettingsType settingsType, int value)
    {
        switch (settingsType)
        {
            case SettingsType.Language:
                _language.DisplayValue(value);
                break;
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

        _language.OnValueChanged += ValueChanged;
        _numberRounds.OnValueChanged += ValueChanged;
        _trainingTime.OnValueChanged += ValueChanged;
        _restTime.OnValueChanged += ValueChanged;

        _version.text = Application.version;
    }

    private void BackClicked() => OnBackClicked?.Invoke();

    private void ValueChanged(SettingsType settingsType, int value) => OnSettingsChanged?.Invoke(settingsType, value);
}