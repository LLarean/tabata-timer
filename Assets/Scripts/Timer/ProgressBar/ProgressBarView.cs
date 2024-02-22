using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarView : MonoBehaviour
{
    [SerializeField] private Image _fill;

    private Tweener _fillTweener;
    private Tweener _colorTweener;
    
    private float _colorDuration = .5f;
    
    public void StartAnimation(float duration)
    {
        KillFillTweener();
        _fill.fillAmount = 1;
        _fillTweener = _fill.DOFillAmount(0, duration).SetEase(Ease.Linear);
    }
    
    public void PauseAnimation()
    {
        KillFillTweener();
        _colorTweener = _fill.DOColor(GlobalColors.Pause, _colorDuration);
    }

    public void ResumeAnimation(float duration)
    {
        KillFillTweener();
        _fillTweener = _fill.DOFillAmount(0, duration).SetEase(Ease.Linear);
    }
    
    public void ResetAnimation()
    {
        KillFillTweener();
        _fill.fillAmount = 1;
    }
    
    public void SetColor(Color color)
    {
        KillColorTweener();
        _colorTweener = _fill.DOColor(color, _colorDuration);
    }
    
    private void Start()
    {
        _fill.fillAmount = 1;
        _fill.color = GlobalColors.Pause;
    }

    private void OnDestroy() => KillAllTweeners();

    public void KillAllTweeners()
    {
        KillFillTweener();
        KillColorTweener();
    }
    
    private void KillFillTweener()
    {
        if (_fillTweener != null)
        {
            _fillTweener.Kill();
        }
    }
    
    private void KillColorTweener()
    {
        if (_colorTweener != null)
        {
            _colorTweener.Kill();
        }
    }
}