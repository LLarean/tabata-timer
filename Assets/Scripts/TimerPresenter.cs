public class TimerPresenter
{
    private readonly TimerModel _timeModel;
    private readonly TimerView _timerView;

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

    public void Subsribe()
    {
        _timerView.OnStartStopClicked += StartStopClicked;
        _timerView.OnResetClicked += ResetClicked;
        _timerView.OnTimerChanged += TimerChanged;
    }

    public void Unsubscribe()
    {
        _timerView.OnStartStopClicked -= StartStopClicked;
        _timerView.OnResetClicked -= ResetClicked;
        _timerView.OnTimerChanged -= TimerChanged;
    }

    private void StartStopClicked()
    {
        if (_timeModel.IsRunning == true)
        {
            _timerView.StopTimer();
        }
        else
        {
            _timerView.StartTimer();
        }

        _timeModel.IsRunning = !_timeModel.IsRunning;
    }

    private void ResetClicked()
    {
        _timeModel.CurrentTime = 0;
        _timeModel.CurrentRound = 0;

        _timerView.StopTimer();
        _timerView.ResetTimer();
    }

    private void TimerChanged()
    {
        _timeModel.CurrentTime += 1;

        if (_timeModel.IsSport == false && _timeModel.CurrentTime == _timeModel.TimeBreaks)
        {
            _timeModel.IsSport = true;
            _timeModel.CurrentTime = 0;
            _timeModel.CurrentRound++;
            _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
        }

        if (_timeModel.IsSport == true && _timeModel.CurrentTime == _timeModel.SportsTime)
        {
            _timeModel.IsSport = false;
            _timeModel.CurrentTime = 0;
        }

        if (_timeModel.IsSport == false && _timeModel.CurrentTime == _timeModel.TimeBreaks &&
            _timeModel.CurrentRound == _timeModel.NumberRounds)
        {
            _timeModel.IsSport = false;
            _timeModel.CurrentTime = 0;
            _timerView.StopTimer();
        }

        _timerView.DisplayTime(_timeModel.CurrentTime);
    }
}