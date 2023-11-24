using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _time;
    [SerializeField] private TMP_Text _round;
    [Space]
    [SerializeField] private Button _startStop;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _reset;
    
    private Coroutine _coroutine = null;
    private bool _isRunning = false;
    private float _updateFrequency = 1f;

    public event Action OnStartStopClicked;
    public event Action OnResetClicked;
    public event Action OnTimerChanged;

    public void SetUpdateFrequency(float updateFrequency)
    {
        _updateFrequency = updateFrequency;
    }
    
    public void SetRound(int currentRounds, int numberRounds)
    {
        _round.text = $"{currentRounds}/{numberRounds}";
    }
    
    public void StartTimer()
    {
        _isRunning = true;
        _coroutine = StartCoroutine(Timer());
        _label.text = GlobalStrings.Stop;
    }

    public void StopTimer()
    {
        _isRunning = false;
        
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _label.text = GlobalStrings.Start;
    }
    
    public void ResetTimer()
    {
        StopTimer();
        _label.text = GlobalStrings.Start;
        DisplayTime(0);
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _time.text = $"{minutes:00}:{seconds:00}";
    }

    private void Start()
    {
        _startStop.onClick.AddListener(ClickStartStop);
        _reset.onClick.AddListener(ClickReset);

        _label.text = GlobalStrings.Start;
        _round.text = String.Empty;
        DisplayTime(0);
    }

    private void OnDestroy()
    {
        _startStop.onClick.RemoveAllListeners();
        _reset.onClick.RemoveAllListeners();
    }
    
    private void ClickStartStop() => OnStartStopClicked?.Invoke();
    
    private void ClickReset() => OnResetClicked?.Invoke();

    private IEnumerator Timer()
    {
        while (_isRunning == true)
        {
            OnTimerChanged?.Invoke();
            yield return new WaitForSeconds(_updateFrequency);
        }
    }
}
