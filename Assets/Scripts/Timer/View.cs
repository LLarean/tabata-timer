using System;
using UnityEngine;

public class View : MonoBehaviour
{
    public event Action OnEnabled;
    
    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);
    
    private void OnEnable() => OnEnabled?.Invoke();
}