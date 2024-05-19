using EventBusSystem;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, ISoundHandler
{
    [SerializeField] private AudioSource _sounds;
    [Space]
    [SerializeField] private AudioClip _tap;
    [SerializeField] private AudioClip _startFinish;
    [SerializeField] private AudioClip _sport;
    [SerializeField] private AudioClip _timeBreak;
    
    public void HandleTap() => _sounds.PlayOneShot(_tap);

    public void HandleToggle() => _sounds.PlayOneShot(_startFinish);

    public void HandleSport() => _sounds.PlayOneShot(_sport);

    public void HandleTieBreak() => _sounds.PlayOneShot(_timeBreak);
    
    private void Awake() => EventBus.Subscribe(this);

    private void OnDestroy() => EventBus.Unsubscribe(this);
}