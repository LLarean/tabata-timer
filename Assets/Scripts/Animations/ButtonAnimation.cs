using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _duration = .2f;
    private float _minimumScale = .9f;
    private float _maximumScale = 1;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.transform.DOScale(_minimumScale, _duration);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.transform.DOScale(_maximumScale, _duration);
    }
}