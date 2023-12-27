using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : View
{
    [SerializeField] private Button _back;
    [Space]
    [SerializeField] private SettingsItem _rounds;
    [SerializeField] private SettingsItem _sport;
    [SerializeField] private SettingsItem _tieBreak;
    
    public event Action OnBackClicked;
    public event Action<SettingsType, int> OnSettingsChanged;
    
    public void DisplayValue(SettingsType settingsType, int value)
    {
        switch (settingsType)
        {
            case SettingsType.Rounds:
                _rounds.DisplayValue(value);
                break;
            case SettingsType.Sport:
                _sport.DisplayValue(value);
                break;
            case SettingsType.TieBreak:
                _tieBreak.DisplayValue(value);
                break;
        }
    }
    
    private void Start()
    {
        _back.onClick.AddListener(BackClicked);

        _rounds.OnValueChanged += ValueChanged;
        _sport.OnValueChanged += ValueChanged;
        _tieBreak.OnValueChanged += ValueChanged;
    }

    private void OnDestroy()
    {
        _back.onClick.RemoveAllListeners();
        
        _rounds.OnValueChanged -= ValueChanged;
        _sport.OnValueChanged -= ValueChanged;
        _tieBreak.OnValueChanged -= ValueChanged;
    }

    private void BackClicked() => OnBackClicked?.Invoke();

    private void ValueChanged(SettingsType settingsType, int value) => OnSettingsChanged?.Invoke(settingsType, value);
}