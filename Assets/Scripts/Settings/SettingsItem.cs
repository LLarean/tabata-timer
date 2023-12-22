using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsItem : MonoBehaviour
{
    [SerializeField] private Button _reduce;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Button _add;
    [Space]
    [SerializeField] private SettingsType _settingsType;

    private int _currentValue;



    public event Action<SettingsType, int> OnValueChanged;

    private void Start()
    {
        _reduce.onClick.AddListener(MinusClicked);
        _add.onClick.AddListener(PlusClicked);

        SetValue();
    }

    private void OnDestroy()
    {
        _reduce.onClick.RemoveAllListeners();
        _add.onClick.RemoveAllListeners();
    }

    private void MinusClicked()
    {
        _currentValue--;

        if (_settingsType == SettingsType.Rounds)
        {
            if (_currentValue < SettingsValue.MinimalRounds)
            {
                _currentValue = SettingsValue.MinimalRounds;
            }
        }
        else if (_settingsType == SettingsType.Sport)
        {
            if (_currentValue < SettingsValue.MinimalSport)
            {
                _currentValue = SettingsValue.MinimalSport;
            }
        }
        else if (_settingsType == SettingsType.TieBreak)
        {
            if (_currentValue < SettingsValue.MinimalTieBreak)
            {
                _currentValue = SettingsValue.MinimalTieBreak;
            }
        }

        _value.text = _currentValue.ToString();

        PlayerPrefs.SetInt($"{_settingsType}", _currentValue);
        PlayerPrefs.Save();

        OnValueChanged?.Invoke(_settingsType, _currentValue);
    }

    private void PlusClicked()
    {
        _currentValue++;

        if (_settingsType == SettingsType.Rounds)
        {
            if (_currentValue > SettingsValue.MaximumRounds)
            {
                _currentValue = SettingsValue.MaximumRounds;
            }
        }
        else if (_settingsType == SettingsType.Sport)
        {
            if (_currentValue > SettingsValue.MaximumSport)
            {
                _currentValue = SettingsValue.MaximumSport;
            }
        }
        else if (_settingsType == SettingsType.TieBreak)
        {
            if (_currentValue > SettingsValue.MaximumTieBreak)
            {
                _currentValue = SettingsValue.MaximumTieBreak;
            }
        }

        _value.text = _currentValue.ToString();

        PlayerPrefs.SetInt($"{_settingsType}", _currentValue);
        PlayerPrefs.Save();

        OnValueChanged?.Invoke(_settingsType, _currentValue);
    }

    private void SetValue()
    {
        if (PlayerPrefs.HasKey(_settingsType.ToString()) == false)
        {
            SetDefaultValue();
        }

        _currentValue = PlayerPrefs.GetInt(_settingsType.ToString());
        _value.text = _currentValue.ToString();
    }

    private void SetDefaultValue()
    {
        switch (_settingsType)
        {
            case SettingsType.Rounds:
                PlayerPrefs.SetInt(_settingsType.ToString(), SettingsValue.DefaultRounds);
                break;
            case SettingsType.Sport:
                PlayerPrefs.SetInt(_settingsType.ToString(), SettingsValue.DefaultSport);
                break;
            case SettingsType.TieBreak:
                PlayerPrefs.SetInt(_settingsType.ToString(), SettingsValue.DefaultTieBreak);
                break;
        }

        PlayerPrefs.Save();
    }
}