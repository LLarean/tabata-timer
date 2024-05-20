using UnityEngine;

public class ProgressBarPresenter
{
    private readonly ProgressBarModel _progressBarModel;
    private readonly ProgressBarView _progressBarView;
    
    public ProgressBarPresenter(ProgressBarModel progressBarModel, ProgressBarView progressBarView)
    {
        _progressBarModel = progressBarModel;
        _progressBarView = progressBarView;
    }

    public void StartAnimation(float duration)
    {
        _progressBarModel.CurrentDuration = duration;
        _progressBarView.ResumeAnimation(_progressBarModel.CurrentDuration);
    }

    public void PauseAnimation() => _progressBarView.PauseAnimation();

    public void ChangeMaximumDuration(float duration)
    {
        _progressBarModel.MaximumDuration = duration;
        _progressBarView.StartAnimation(_progressBarModel.MaximumDuration);
    }

    public void ResetAnimation() => _progressBarView.ResetAnimation();

    public void SetColor(WorkoutStatus workoutStatus)
    {
        Color color = workoutStatus == WorkoutStatus.Workout ? GlobalColors.Workout : GlobalColors.Rest;
        _progressBarView.SetColor(color);
    }
}