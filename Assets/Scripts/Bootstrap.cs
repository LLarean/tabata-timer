using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private TimerView _timerView;
    [SerializeField] private AudioPlayer _audioPlayer;
    
    private TimerPresenter _timerPresenter;

    private void Start()
    {
        TimerModel timerModel = new TimerModel();
        
        _timerPresenter = new TimerPresenter(timerModel, _timerView);
        _timerPresenter.SetData();
        _timerPresenter.SetAudioPlayer(_audioPlayer);
        _timerPresenter.Subsribe();
    }

    private void OnDestroy()
    {
        _timerPresenter.Unsubscribe();
    }
}
