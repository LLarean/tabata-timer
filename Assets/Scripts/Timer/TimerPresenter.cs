using UnityEngine;

public class TimerPresenter
{
    private readonly TimerModel _timeModel;
    private readonly TimerView _timerView;

    private AudioPlayer _audioPlayer;
    private ProgressBarPresenter _progressBarPresenter;
    private ViewChanger _viewChanger;

    public TimerPresenter(TimerModel timeModel, TimerView timerView)
    {
        _timeModel = timeModel;
        _timerView = timerView;
    }

    public void SetData()
    {
        _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.SetUpdateFrequency(_timeModel.UpdateFrequency);
    }

    public void SetProgressBar(ProgressBarPresenter progressBarPresenter) => _progressBarPresenter = progressBarPresenter;

    public void SetAudioPlayer(AudioPlayer audioPlayer) => _audioPlayer = audioPlayer;

    public void SetViewChanger(ViewChanger viewChanger) => _viewChanger = viewChanger;

    public void Subsribe()
    {
        _timerView.OnEnabled += ViewEnabled;
        _timerView.OnSettingsClicked += SettingsClicked;
        _timerView.OnStartStopClicked += StartStopClicked;
        _timerView.OnResetClicked += ResetClicked;
        _timerView.OnTimerChanged += TimerChanged;
    }

    public void Unsubscribe()
    {
        _timerView.OnEnabled -= ViewEnabled;
        _timerView.OnSettingsClicked -= SettingsClicked;
        _timerView.OnStartStopClicked -= StartStopClicked;
        _timerView.OnResetClicked -= ResetClicked;
        _timerView.OnTimerChanged -= TimerChanged;
    }

    private void ViewEnabled() => SetModel();

    private void SettingsClicked()
    {
        _audioPlayer.PlayTap();

        _timerView.StopTimer();
        _progressBarPresenter.PauseAnimation();
        _timeModel.IsRunning = false;
        _timeModel.TimerStatus = TimerStatus.Preparation;

        _viewChanger.ShowSettings();
    }

    private void StartStopClicked()
    {
        PlaySound();

        if (_timeModel.IsRunning == true)
        {
            _timerView.StopTimer();
            _progressBarPresenter.PauseAnimation();
        }
        else
        {
            _timerView.StartTimer();
            _progressBarPresenter.SetColor(_timeModel.TimerStatus);
            _progressBarPresenter.StartAnimation(_timeModel.CurrentTime);
        }

        _timeModel.IsRunning = !_timeModel.IsRunning;
    }

    private void ResetClicked()
    {
        _audioPlayer.PlayTap();

        _timerView.StopTimer();
        _timerView.ResetTimer();

        _progressBarPresenter.ResetAnimation();
        _timeModel.TimerStatus = TimerStatus.Preparation;

        ResetTimer();
    }

    private void TimerChanged()
    {
        _timeModel.CurrentTime -= 1;
        
        // if (_timeModel.CurrentTime == 0 && _timeModel.TimerStatus != TimerStatus.Workout &&
        //     _timeModel.CurrentRound == _timeModel.NumberRounds)
        // {
        //     _timeModel.TimerStatus = TimerStatus.Preparation;
        //
        //     _timerView.StopTimer();
        //     _timerView.ResetTimer();
        //     ResetTimer();
        //     _progressBarPresenter.SetPauseColor();
        //
        //     _audioPlayer.PlayStartFinish();
        // }
        // else if (_timeModel.CurrentTime == 0 && _timeModel.TimerStatus != TimerStatus.Workout)
        // {
        //     _timeModel.IsSport = true;
        //     _timeModel.TimerStatus = TimerStatus.Workout;
        //
        //     _timeModel.CurrentTime = _timeModel.SportsTime;
        //     _timeModel.CurrentRound++;
        //
        //     _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
        //     _progressBarPresenter.SetColor(_timeModel.TimerStatus);
        //     _progressBarPresenter.ChangeMaximumDuration(_timeModel.SportsTime);
        //
        //     _audioPlayer.PlaySport();
        // }
        // else if (_timeModel.CurrentTime == 0 && _timeModel.TimerStatus == TimerStatus.Workout)
        // {
        //     _timeModel.IsSport = false;
        //     _timeModel.TimerStatus = TimerStatus.Rest;
        //
        //     _timeModel.CurrentTime = _timeModel.TimeBreaks;
        //
        //     _progressBarPresenter.SetColor(_timeModel.TimerStatus);
        //     _progressBarPresenter.ChangeMaximumDuration(_timeModel.TimeBreaks);
        //     _audioPlayer.PlayTimeBreak();
        // }

        if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == false &&
            _timeModel.CurrentRound == _timeModel.NumberRounds)
        {
            _timeModel.TimerStatus = TimerStatus.Preparation;
        
            _timerView.StopTimer();
            _timerView.ResetTimer();
            ResetTimer();
        
            _audioPlayer.PlayStartFinish();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == false)
        {
            _timeModel.IsSport = true;
            _timeModel.TimerStatus = TimerStatus.Workout;
        
            _timeModel.CurrentTime = _timeModel.SportsTime;
            _timeModel.CurrentRound++;
        
            _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
            _progressBarPresenter.ChangeMaximumDuration(_timeModel.SportsTime);
        
            _audioPlayer.PlaySport();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == true)
        {
            _timeModel.IsSport = false;
            _timeModel.TimerStatus = TimerStatus.Rest;
        
            _timeModel.CurrentTime = _timeModel.TimeBreaks;
        
            _progressBarPresenter.ChangeMaximumDuration(_timeModel.TimeBreaks);
            _audioPlayer.PlayTimeBreak();
        }
        
        _timerView.DisplayTime(_timeModel.CurrentTime);
        _timerView.SetStatus(_timeModel.TimerStatus);
        _progressBarPresenter.SetColor(_timeModel.TimerStatus);
    }

    private void ResetTimer()
    {
        _timeModel.IsRunning = false;
        _timeModel.TimerStatus = TimerStatus.Rest;
        
        _timeModel.IsSport = false;
        _timeModel.IsStart = true;

        _timeModel.CurrentRound = 0;
        _timeModel.CurrentTime = _timeModel.TimeBreaks;

        _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.ResetTimer();
    }

    private void PlaySound()
    {
        _audioPlayer.PlayTap();

        if (_timeModel.IsStart == true)
        {
            _timeModel.IsStart = false;
            _progressBarPresenter.ChangeMaximumDuration(_timeModel.TimeBreaks);
            _audioPlayer.PlayStartFinish();
        }
    }

    private void SetModel()
    {
        var numberRounds = PlayerPrefs.GetInt(SettingsType.Rounds.ToString());
        var sportsTime = PlayerPrefs.GetInt(SettingsType.Sport.ToString());
        var timeBreaks = PlayerPrefs.GetInt(SettingsType.TieBreak.ToString());

        if (_timeModel.NumberRounds != numberRounds
            || _timeModel.SportsTime != sportsTime
            || _timeModel.TimeBreaks != timeBreaks)
        {
            _timeModel.NumberRounds = numberRounds;
            _timeModel.SportsTime = sportsTime;
            _timeModel.TimeBreaks = timeBreaks;


            _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
            ResetTimer();
        }
    }
}