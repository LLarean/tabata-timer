using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsItem : MonoBehaviour
{
    [SerializeField] private SettingsType _settingsType;
    [Space]
    [SerializeField] private Button _reduce;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Button _increase;

    private int _currentValue;

    public event Action<SettingsType, int> OnValueChanged;

    public void DisplayValue(int value)
    {
        _currentValue = value;

        if (_settingsType == SettingsType.Language && value == 0)
        {
            _value.text = GlobalStrings.Ru;
        }
        else if (_settingsType == SettingsType.Language && value == 1)
        {
            _value.text = GlobalStrings.En;
        }
        else
        {
            _value.text = value.ToString();
        }
    }
    
    private void Start()
    {
        _reduce.onClick.AddListener(ReduceClick);
        _increase.onClick.AddListener(IncreaseClick);
    }

    private void OnDestroy()
    {
        _reduce.onClick.RemoveAllListeners();
        _increase.onClick.RemoveAllListeners();
    }

    private void ReduceClick()
    {
        _currentValue--;
        OnValueChanged?.Invoke(_settingsType, _currentValue);
    }

    private void IncreaseClick()
    {
        _currentValue++;
        OnValueChanged?.Invoke(_settingsType, _currentValue);
    }
}