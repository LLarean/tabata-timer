using EventBusSystem;

public interface IProgressBarHandler : IGlobalSubscriber
{
    void SetColor(WorkoutStatus workoutStatus);
    void StartAnimation(int currentTime);
    void PauseAnimation();
    void ResetAnimation();
    void ChangeMaximumDuration(int duration);
}