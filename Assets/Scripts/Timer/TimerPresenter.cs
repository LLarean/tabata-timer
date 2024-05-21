using EventBusSystem;
using UnityEngine;

public class TimerPresenter
{
    private readonly TimerModel _timeModel;
    private readonly TimerView _timerView;

    private ProgressBarPresenter _progressBarPresenter;
    private string _workoutStatus = GlobalStrings.Preparation;

    public TimerPresenter(TimerModel timeModel, TimerView timerView)
    {
        _timeModel = timeModel;
        _timerView = timerView;
    }

    public void SetData()
    {
        _timerView.DisplayRounds(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.SetTimeBreaks(_timeModel.TimeBreaks);
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

    private void ViewEnabled() => UpdateModel();

    private void SettingsClicked()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());
        EventBus.RaiseEvent<IChangeViewHandler>(handler => handler.HandleShowSettings());
        StopTimer();
    }

    private void StartClicked()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());

        if (_timeModel.TimerStatus != TimerStatus.Counting)
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }
    }

    private void ResetClicked()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTap());
        ResetTimer();
    }

    private void StartTimer()
    {
        _timeModel.TimerStatus = TimerStatus.Counting;
        
        _timerView.DisplayStatus(_workoutStatus);
        _timerView.StartTimeCounting();
        
        _progressBarPresenter.SetColor(_timeModel.WorkoutStatus);
        _progressBarPresenter.StartAnimation(_timeModel.CurrentTime);
    }

    private void StopTimer()
    {
        _timeModel.TimerStatus = TimerStatus.Pause;
        
        _timerView.StopTimeCounting();
        _progressBarPresenter.PauseAnimation();
    }

    private void ResetTimer()
    {
        StopTimer();
        
        _timeModel.TimerStatus = TimerStatus.Reset;
        _timeModel.WorkoutStatus = WorkoutStatus.Preparation;
        _timeModel.CurrentRound = 0;
        _timeModel.CurrentTime = _timeModel.TimeBreaks;

        _timerView.DisplayRounds(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.SetTimeBreaks(_timeModel.TimeBreaks);
        _timerView.ResetTimeCounting();
        
        _workoutStatus = GlobalStrings.Preparation;
        _timerView.DisplayStatus(GlobalStrings.Pause);
        
        _progressBarPresenter.ResetAnimation();
    }

    private void TimerUpdated()
    {
        _timeModel.CurrentTime -= (int)_timeModel.UpdateFrequency;

        if (_timeModel.CurrentTime == 0 && _timeModel.WorkoutStatus == WorkoutStatus.Workout &&
            _timeModel.CurrentRound == _timeModel.NumberRounds)
        {
            EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleFinish());
            ResetTimer();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.WorkoutStatus == WorkoutStatus.Preparation)
        {
            SetWorkout();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.WorkoutStatus == WorkoutStatus.Rest)
        {
            SetWorkout();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.WorkoutStatus == WorkoutStatus.Workout)
        {
            SetTimeBreak();
        }

        _timerView.DisplayTime(_timeModel.CurrentTime);
    }

    private void SetWorkout()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleSport());

        _timeModel.WorkoutStatus = WorkoutStatus.Workout;
        
        _timeModel.CurrentTime = _timeModel.SportsTime;
        _timeModel.CurrentRound++;

        _workoutStatus = GlobalStrings.Workout;
        _timerView.DisplayRounds(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.DisplayStatus(_workoutStatus);

        _progressBarPresenter.SetColor(_timeModel.WorkoutStatus);
        _progressBarPresenter.ChangeMaximumDuration(_timeModel.SportsTime);
    }

    private void SetTimeBreak()
    {
        EventBus.RaiseEvent<ISoundHandler>(handler => handler.HandleTieBreak());

        _timeModel.WorkoutStatus = WorkoutStatus.Rest;
        _timeModel.CurrentTime = _timeModel.TimeBreaks;

        _workoutStatus = GlobalStrings.Rest;
        _timerView.DisplayStatus(_workoutStatus);

        _progressBarPresenter.SetColor(_timeModel.WorkoutStatus);
        _progressBarPresenter.ChangeMaximumDuration(_timeModel.TimeBreaks);
    }

    private void UpdateModel()
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

        _timeModel.TimerStatus = TimerStatus.Pause;
        _timeModel.WorkoutStatus = WorkoutStatus.Preparation;

        ResetTimer();
    }
}