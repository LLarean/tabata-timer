using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private TimerView _timerView;
    
    private TimerPresenter _timerPresenter;

    private void Start()
    {
        TimerModel timerModel = new TimerModel();
        
        _timerPresenter = new TimerPresenter(timerModel, _timerView);
        _timerPresenter.SetData();
        _timerPresenter.Subsribe();
    }

    private void OnDestroy()
    {
        _timerPresenter.Unsubscribe();
    }
}
