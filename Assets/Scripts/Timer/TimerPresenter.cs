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
        _timerView.DisplayRounds(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.SetUpdateFrequency(_timeModel.UpdateFrequency);
    }

    public void SubscribeChangesView()
    {
        _timerView.OnEnabled += ViewEnabled;
        _timerView.OnSettingsClicked += SettingsClicked;
        _timerView.OnStartClicked += StartClicked;
        _timerView.OnResetClicked += ResetClicked;
        _timerView.OnTimerUpdated += TimerUpdated;
    }

    public void SetProgressBar(ProgressBarPresenter progressBarPresenter) => _progressBarPresenter = progressBarPresenter;

    private void ViewEnabled() => SetModel();

    private void SettingsClicked()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());
        EventBus.RaiseEvent<IChangeViewHandler>(handler => handler.HandleShowSettings());
        StopTimer();
    }

    private void StartClicked()
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
        
        _timerView.DisplayStatus(_timerStatus);
        _progressBarPresenter.SetColor(_timeModel.IsSport);
        
        _timerView.StartTimeCounting();
        _progressBarPresenter.StartAnimation(_timeModel.CurrentTime);
    }

    private void StopTimer()
    {
        _timeModel.IsRunning = false;
        
        _timerView.StopTimeCounting();
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

        _timerView.DisplayRounds(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.ResetTimeCounting();
        
        _timerStatus = GlobalStrings.Preparation;
        _timerView.DisplayStatus(GlobalStrings.Pause);
        
        _progressBarPresenter.PauseAnimation();
    }

    private void TimerUpdated()
    {
        _timeModel.CurrentTime -= 1;

        if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == false &&
            _timeModel.CurrentRound == _timeModel.NumberRounds)
        {
            EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleToggleStatus());
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
        _timerView.DisplayRounds(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.DisplayStatus(_timerStatus);

        _progressBarPresenter.SetColor(_timeModel.IsSport);
        _progressBarPresenter.ChangeMaximumDuration(_timeModel.SportsTime);
    }

    private void SetTimeBreak()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTieBreak());

        _timeModel.IsSport = false;
        _timeModel.CurrentTime = _timeModel.TimeBreaks;

        _timerStatus = GlobalStrings.Rest;
        _timerView.DisplayStatus(_timerStatus);

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
            
            EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleToggleStatus());
        }
    }

    private void SetModel()
    {
        var numberRounds = PlayerPrefs.GetInt(SettingsType.NumberRounds.ToString(), DefaultSettingsValue.NumberRounds);
        var sportsTime = PlayerPrefs.GetInt(SettingsType.TrainingTime.ToString(), DefaultSettingsValue.TrainingTime);
        var timeBreaks = PlayerPrefs.GetInt(SettingsType.RestTime.ToString(), DefaultSettingsValue.RestTime);

        if (_timeModel.NumberRounds == numberRounds &&
            _timeModel.SportsTime == sportsTime &&
            _timeModel.TimeBreaks == timeBreaks)
        {
            return;
        }
        
        _timeModel.NumberRounds = numberRounds;
        _timeModel.SportsTime = sportsTime;
        _timeModel.TimeBreaks = timeBreaks;

        _timerView.DisplayRounds(_timeModel.CurrentRound, _timeModel.NumberRounds);
        ResetTimer();
        _progressBarPresenter.ResetAnimation();
    }
}