public class TimerModel
{
    public int NumberRounds;
    public int CurrentRound;

    public int WaitingTime;
    public int SportsTime;
    public int TimeBreaks;
    public int CurrentTime;

    public float UpdateFrequency;

    public bool IsRunning;
    public bool IsSport;

    public TimerModel(int waitingTime = 10, int numberRounds = 8, int sportsTime = 20, int timeBreaks = 10,
        int currentTime = 0, float updateFrequency = 1f)
    {
        WaitingTime = waitingTime;
        NumberRounds = numberRounds;

        SportsTime = sportsTime;
        TimeBreaks = timeBreaks;
        CurrentTime = currentTime;

        UpdateFrequency = updateFrequency;
        IsRunning = false;
        IsSport = false;
    }
}