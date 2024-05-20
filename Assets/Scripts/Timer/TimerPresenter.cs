using EventBusSystem;
using UnityEngine;

public class TimerPresenter
{
    private readonly TimerModel _timeModel;
    private readonly TimerView _timerView;

    private ProgressBarPresenter _progressBarPresenter;
    private string _timerStatus = GlobalStrings.Preparation;

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

    public void Subsribe()
    {
        _timerView.OnEnabled += ViewEnabled;
        _timerView.OnSettingsClicked += SettingsClicked;
        _timerView.OnStartStopClicked += StartStopClicked;
        _timerView.OnResetClicked += ResetClicked;
        _timerView.OnTimerChanged += TimerChanged;
    }

    private void ViewEnabled() => SetModel();

    private void SettingsClicked()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());
        StopTimer();
        EventBus.RaiseEvent<IChangeViewHandler>(handler => handler.HandleShowSettings());
    }

    private void StartStopClicked()
    {
        PlaySound();

        if (_timeModel.IsRunning == true)
        {
            StopTimer();
        }
        else
        {
            StartTimer();
        }
    }

    private void ResetClicked()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());
        ResetTimer();
    }

    private void StartTimer()
    {
        _timeModel.IsRunning = true;
        
        _timerView.SetStatus(_timerStatus);
        _progressBarPresenter.SetColor(_timeModel.IsSport);
        
        _timerView.StartTimer();
        _progressBarPresenter.StartAnimation(_timeModel.CurrentTime);
    }

    private void StopTimer()
    {
        _timeModel.IsRunning = false;
        
        _timerView.StopTimer();
        _progressBarPresenter.PauseAnimation();
    }

    private void ResetTimer()
    {
        StopTimer();
        
        _timeModel.IsRunning = false;
        _timeModel.IsSport = false;
        _timeModel.IsStart = true;

        _timeModel.CurrentRound = 0;
        _timeModel.CurrentTime = _timeModel.TimeBreaks;

        _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.ResetTimer();
        
        _timerStatus = GlobalStrings.Preparation;
        _timerView.SetStatus(GlobalStrings.Pause);
        
        _progressBarPresenter.PauseAnimation();
    }

    private void TimerChanged()
    {
        _timeModel.CurrentTime -= 1;

        if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == false &&
            _timeModel.CurrentRound == _timeModel.NumberRounds)
        {
            EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleToggle());
            ResetTimer();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == false)
        {
            SetWorkout();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == true)
        {
            SetTimeBreak();
        }

        _timerView.DisplayTime(_timeModel.CurrentTime);
    }

    private void SetWorkout()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleSport());

        _timeModel.IsSport = true;
        _timeModel.CurrentTime = _timeModel.SportsTime;
        _timeModel.CurrentRound++;

        _timerStatus = GlobalStrings.Workout;
        _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.SetStatus(_timerStatus);

        _progressBarPresenter.SetColor(_timeModel.IsSport);
        _progressBarPresenter.ChangeMaximumDuration(_timeModel.SportsTime);
    }

    private void SetTimeBreak()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTieBreak());

        _timeModel.IsSport = false;
        _timeModel.CurrentTime = _timeModel.TimeBreaks;

        _timerStatus = GlobalStrings.Rest;
        _timerView.SetStatus(_timerStatus);

        _progressBarPresenter.SetColor(_timeModel.IsSport);
        _progressBarPresenter.ChangeMaximumDuration(_timeModel.TimeBreaks);
    }

    private void PlaySound()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());

        if (_timeModel.IsStart == true)
        {
            _timeModel.IsStart = false;
            _progressBarPresenter.ChangeMaximumDuration(_timeModel.TimeBreaks);
            
            EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleToggle());
        }
    }

    private void SetModel()
    {
        var numberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString());
        var sportsTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString());
        var timeBreaks = PlayerPrefs.GetInt(SettingsType.RestTime.ToString());

        if (_timeModel.NumberRounds != numberRounds ||
            _timeModel.SportsTime != sportsTime ||
            _timeModel.TimeBreaks != timeBreaks)
        {
            _timeModel.NumberRounds = numberRounds;
            _timeModel.SportsTime = sportsTime;
            _timeModel.TimeBreaks = timeBreaks;

            _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
            ResetTimer();
            _progressBarPresenter.ResetAnimation();
        }
    }
}