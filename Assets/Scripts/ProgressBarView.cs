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
        KillTweeners();
        
        _fill.fillAmount = 1;
        // _colorTweener = _fill.DOColor(GlobalColors.Workout, _colorDuration);
        _fillTweener = _fill.DOFillAmount(0, duration).SetEase(Ease.Linear);
    }
    
    public void PauseAnimation()
    {
        KillTweeners();

        _colorTweener = _fill.DOColor(GlobalColors.Pause, _colorDuration);
    }

    public void ResumeAnimation(float duration)
    {
        KillTweeners();
        
        // _colorTweener = _fill.DOColor(GlobalColors.Workout, _colorDuration);
        _fillTweener = _fill.DOFillAmount(0, duration).SetEase(Ease.Linear);
    }
    
    public void ResetAnimation()
    {
        KillTweeners();
        
        _fill.fillAmount = 1;
        // _colorTweener = _fill.DOColor(GlobalColors.Pause, _colorDuration);
    }
    
    public void SetColor(Color color)
    {
        // KillTweeners();
        
        if (_colorTweener != null)
        {
            _colorTweener.Kill();
        }
        
        _colorTweener = _fill.DOColor(color, _colorDuration);
    }
    
    private void Start()
    {
        _fill.fillAmount = 1;
        _fill.color = GlobalColors.Pause;
    }

    private void OnDestroy() => KillTweeners();

    private void KillTweeners()
    {
        // if (_colorTweener != null)
        // {
        //     _colorTweener.Kill();
        // }
        
        if (_fillTweener != null)
        {
            _fillTweener.Kill();
        }
    }
}