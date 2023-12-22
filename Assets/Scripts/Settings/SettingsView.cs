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
    
    private AudioPlayer _audioPlayer;

    public event Action OnBackClicked;
    
    private void Start()
    {
        _back.onClick.AddListener(BackClicked);

        _rounds.OnValueChanged += RoundsChanged;
        _sport.OnValueChanged += SportChanged;
        _tieBreak.OnValueChanged += TieBreakChanged;
    }

    private void OnDestroy()
    {
        _back.onClick.RemoveAllListeners();
    }

    private void BackClicked() => OnBackClicked?.Invoke();

    private void RoundsChanged(SettingsType settingsType, int i)
    {
        // _audioPlayer.PlayTap();
    }

    private void SportChanged(SettingsType settingsType, int i)
    {
        // _audioPlayer.PlayTap();
    }

    private void TieBreakChanged(SettingsType settingsType, int i)
    {
        // _audioPlayer.PlayTap();
    }
}
