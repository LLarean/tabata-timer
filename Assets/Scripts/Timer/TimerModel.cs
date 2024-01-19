public class TimerModel
{
    public int NumberRounds;
    public int CurrentRound;

    public int SportsTime;
    public int TimeBreaks;
    public int CurrentTime;

    public float UpdateFrequency;

    public bool IsRunning;
    
    public bool IsSport;
    public bool IsStart;

    public TimerModel(int numberRounds = DefaultSettingsValue.NumberRounds, int sportsTime = DefaultSettingsValue.TrainingTime,
        int timeBreaks = DefaultSettingsValue.RestTime, float updateFrequency = 1f)
    {
        NumberRounds = numberRounds;

        SportsTime = sportsTime;
        TimeBreaks = timeBreaks;
        CurrentTime = timeBreaks;

        UpdateFrequency = updateFrequency;

        IsRunning = false;
        IsSport = false;
        IsStart = true;
    }
}