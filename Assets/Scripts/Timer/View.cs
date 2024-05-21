using System;
using DG.Tweening;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _isRight;

    private float _showDuration = .1f;
    private float _hideDuration = .1f;

    private float _showedPosition = 0;
    private float _hidedPosition = 700;

    public event Action OnEnabled;

    public void Show()
    {
        gameObject.SetActive(true);
        
        _canvasGroup.DOFade(1, _showDuration);
        gameObject.transform.DOLocalMoveX(_showedPosition, _showDuration).SetEase(Ease.InSine);
    }

    public void Hide()
    {
        float hidedPosition = _isRight == true ? _hidedPosition : -_hidedPosition;

        _canvasGroup.DOFade(0, _hideDuration);
        gameObject.transform.DOLocalMoveX(hidedPosition, _hideDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnEnable() => OnEnabled?.Invoke();
}