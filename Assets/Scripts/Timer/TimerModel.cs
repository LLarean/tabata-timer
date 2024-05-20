public class TimerModel
{
    public readonly float UpdateFrequency;
    
    public int NumberRounds;
    public int CurrentRound;

    public int SportsTime;
    public int TimeBreaks;
    public int CurrentTime;
    
    public TimerStatus TimerStatus;
    public WorkoutStatus WorkoutStatus;

    public TimerModel(int numberRounds = DefaultSettingsValue.NumberRounds, int sportsTime = DefaultSettingsValue.TrainingTime,
        int timeBreaks = DefaultSettingsValue.RestTime, float updateFrequency = 1f)
    {
        NumberRounds = numberRounds;

        SportsTime = sportsTime;
        TimeBreaks = timeBreaks;
        CurrentTime = timeBreaks;

        UpdateFrequency = updateFrequency;

        TimerStatus = TimerStatus.Reset;
        WorkoutStatus = WorkoutStatus.Preparation;
    }
}