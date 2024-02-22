public class ProgressBarModel
{
    public float MaximumDuration;
    public float CurrentDuration;
    
    public ProgressBarModel(float maximumDuration = 0)
    {
        MaximumDuration = maximumDuration;
        CurrentDuration = MaximumDuration;
    }
}