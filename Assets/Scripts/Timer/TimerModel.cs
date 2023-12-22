public class TimerModel
{
    public int NumberRounds;
    public int CurrentRound;

    public int SportsTime;
    public int TimeBreaks;
    public int CurrentTime;

    public float UpdateFrequency;

    public TimerStatus TimerStatus;
    public bool IsRunning;
    
    public bool IsSport;
    public bool IsStart;

    public TimerModel(int numberRounds = SettingsValue.DefaultRounds, int sportsTime = SettingsValue.DefaultSport,
        int timeBreaks = SettingsValue.DefaultTieBreak, float updateFrequency = 1f)
    {
        NumberRounds = numberRounds;

        SportsTime = sportsTime;
        TimeBreaks = timeBreaks;
        CurrentTime = timeBreaks;

        UpdateFrequency = updateFrequency;

        TimerStatus = TimerStatus.Preparation;
        IsRunning = false;
        IsSport = false;
        IsStart = true;
    }
}