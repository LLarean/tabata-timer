using UnityEngine;

public interface IProgressBarView
{
    void StartAnimation(float duration);
    void PauseAnimation();
    void ResumeAnimation(float duration);
    void ResetAnimation();
    void SetColor(Color color);
}