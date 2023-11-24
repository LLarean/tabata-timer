public class TimerPresenter
{
    private readonly TimerModel _timeModel;
    private readonly TimerView _timerView;
    
    private AudioPlayer _audioPlayer;

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

    public void SetAudioPlayer(AudioPlayer audioPlayer) => _audioPlayer = audioPlayer;

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
        PlaySound();
        
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
        _timerView.StopTimer();
        _timerView.ResetTimer();
        
        ResetTimer();
    }

    private void TimerChanged()
    {
        _timeModel.CurrentTime -= 1;

        if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == false && _timeModel.CurrentRound == _timeModel.NumberRounds)
        {
            _timerView.StopTimer();
            _timerView.ResetTimer();
            ResetTimer();
            
            _audioPlayer.PlayStartFinish();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == false)
        {
            _timeModel.IsSport = true;
            _timeModel.CurrentTime = _timeModel.SportsTime;
            _timeModel.CurrentRound++;
            
            _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
            _timerView.SetCircle(_timeModel.SportsTime);
            
            _audioPlayer.PlaySport();
        }
        else if (_timeModel.CurrentTime == 0 && _timeModel.IsSport == true)
        {
            _timeModel.IsSport = false;
            _timeModel.CurrentTime = _timeModel.TimeBreaks;
            
            _timerView.SetCircle(_timeModel.TimeBreaks);
            
            _audioPlayer.PlayTimeBreak();
        }

        _timerView.DisplayTime(_timeModel.CurrentTime);
    }

    private void ResetTimer()
    {
        _timeModel.IsSport = false;
        _timeModel.IsStart = true;
        
        _timeModel.CurrentRound = 0;
        _timeModel.CurrentTime = _timeModel.TimeBreaks;
        
        _timeModel.IsRunning = !_timeModel.IsRunning;
        
        _timerView.SetRound(_timeModel.CurrentRound, _timeModel.NumberRounds);
        _timerView.ResetTimer();
    }

    private void PlaySound()
    {
        if (_timeModel.IsStart == true)
        {
            _timeModel.IsStart = false;
            _timerView.SetCircle(_timeModel.TimeBreaks);
            _audioPlayer.PlayStartFinish();
        }
    }
}