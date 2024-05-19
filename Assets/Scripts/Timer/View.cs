using System;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private ViewHub _viewHub;
    [SerializeField] private ViewAnimation _viewAnimation;

    public event Action OnEnabled;

    public void Show() => _viewAnimation.Show();

    public void Hide() => _viewAnimation.Hide();
    
    private void OnEnable() => OnEnabled?.Invoke();
}