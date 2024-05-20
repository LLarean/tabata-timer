using System;
using DG.Tweening;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private bool _isRight;

    private float _showDuration = .3f;
    private float _hideDuration = .1f;

    private float _showedPosition = 0;
    private float _hidedPosition = 2800;

    public event Action OnEnabled;

    public void Show()
    {
        gameObject.SetActive(true);
        gameObject.transform.DOLocalMoveX(_showedPosition, _showDuration);
    }

    public void Hide()
    {
        float hidedPosition = _isRight == true ? _hidedPosition : -_hidedPosition;

        gameObject.transform.DOLocalMoveX(hidedPosition, _hideDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnEnable() => OnEnabled?.Invoke();
}