using DG.Tweening;
using UnityEngine;

public class ViewAnimation : MonoBehaviour
{
    [SerializeField] private Transform _showedPosition;
    [SerializeField] private Transform _hidedPosition;

    private float _showDuration = .3f;
    private float _hideDuration = .1f;
    
    public void Show()
    {
        gameObject.transform.DOLocalMoveX(_showedPosition.transform.localPosition.x, _showDuration);
    }

    public void Hide()
    {
        gameObject.transform.DOLocalMoveX(_hidedPosition.transform.localPosition.x, _hideDuration);
    }
}
