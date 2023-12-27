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

    public void DisplayValue(int value)
    {
        _currentValue = value;
        _value.text = value.ToString();
    }
    
    private void Start()
    {
        _reduce.onClick.AddListener(ReduceClicked);
        _add.onClick.AddListener(AddClicked);
    }

    private void OnDestroy()
    {
        _reduce.onClick.RemoveAllListeners();
        _add.onClick.RemoveAllListeners();
    }

    private void ReduceClicked()
    {
        _currentValue--;
        OnValueChanged?.Invoke(_settingsType, _currentValue);
    }

    private void AddClicked()
    {
        _currentValue++;
        OnValueChanged?.Invoke(_settingsType, _currentValue);
    }
}