using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AudioPlayer _audioPlayer;
    [Space]
    [SerializeField] private TimerView _timerView;
    [SerializeField] private ProgressBarView _progressBarView;
    
    private TimerPresenter _timerPresenter;
    private ProgressBarPresenter _progressBarPresenter;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        InitializePresenters();
        SetData();
    }

    private void InitializePresenters()
    {
        TimerModel timerModel = new TimerModel();

        _timerPresenter = new TimerPresenter(timerModel, _timerView);
        _timerPresenter.SetData();
        _timerPresenter.Subsribe();

        ProgressBarModel progressBarModel = new ProgressBarModel();

        _progressBarPresenter = new ProgressBarPresenter(progressBarModel, _progressBarView);
    }

    private void SetData()
    {
        _timerPresenter.SetAudioPlayer(_audioPlayer);
        _timerPresenter.SetProgressBar(_progressBarPresenter);
    }

    private void OnDestroy()
    {
        _timerPresenter.Unsubscribe();
    }
}
