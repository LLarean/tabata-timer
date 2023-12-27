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
        var rounds = PlayerPrefs.GetInt(SettingsType.Rounds.ToString(), SettingsValue.DefaultRounds);
        var tieBreak = PlayerPrefs.GetInt(SettingsType.TieBreak.ToString(), SettingsValue.DefaultTieBreak);
        var sport = PlayerPrefs.GetInt(SettingsType.Sport.ToString(), SettingsValue.DefaultSport);
        
        _settingsView.DisplayValue(SettingsType.Rounds, rounds);
        _settingsView.DisplayValue(SettingsType.TieBreak, tieBreak);
        _settingsView.DisplayValue(SettingsType.Sport, sport);
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

        if (settingsType == SettingsType.Rounds)
        {
            if (value < SettingsValue.MinimalRounds)
            {
                value = SettingsValue.MinimalRounds;
            } 
            else if (value > SettingsValue.MaximumRounds)
            {
                value = SettingsValue.MaximumRounds;
            }
        }
        else if (settingsType == SettingsType.Sport)
        {
            if (value < SettingsValue.MinimalSport)
            {
                value = SettingsValue.MinimalSport;
            }
            else if (value > SettingsValue.MaximumSport)
            {
                value = SettingsValue.MaximumSport;
            }
        }
        else if (settingsType == SettingsType.TieBreak)
        {
            if (value < SettingsValue.MinimalTieBreak)
            {
                value = SettingsValue.MinimalTieBreak;
            }
            else if (value > SettingsValue.MaximumTieBreak)
            {
                value = SettingsValue.MaximumTieBreak;
            }
        }
        
        PlayerPrefs.SetInt($"{settingsType}", value);
        PlayerPrefs.Save();

        _settingsView.DisplayValue(settingsType, value);
    }
}
