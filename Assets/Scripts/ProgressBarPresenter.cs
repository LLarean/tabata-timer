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

    public void PauseAnimation()
    {
        SetPauseColor();
        _progressBarView.PauseAnimation();
    }

    public void ChangeMaximumDuration(float duration)
    {
        _progressBarModel.MaximumDuration = duration;
        _progressBarView.StartAnimation(_progressBarModel.MaximumDuration);
    }

    public void ResetAnimation() => _progressBarView.ResetAnimation();

    public void SetColor(TimerStatus timerStatus)
    {
        Color color = Color.cyan;
        
        switch (timerStatus)
        {
            case TimerStatus.Preparation:
                color = GlobalColors.Rest;
                break;
            case TimerStatus.Workout:
                color = GlobalColors.Workout;
                break;
            case TimerStatus.Rest:
                color = GlobalColors.Rest;
                break;
        }

        _progressBarView.SetColor(color);
    }

    public void SetPauseColor()
    {
        Color color = GlobalColors.Pause;
        _progressBarView.SetColor(color);
    }
}