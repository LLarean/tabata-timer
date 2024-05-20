using EventBusSystem;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, ISoundHandler
{
    [SerializeField] private AudioSource _sounds;
    [Space]
    [SerializeField] private AudioClip _tap;
    [SerializeField] private AudioClip _sport;
    [SerializeField] private AudioClip _timeBreak;
    [SerializeField] private AudioClip _finish;
    
    public void HandleTap() => _sounds.PlayOneShot(_tap);

    public void HandleSport() => _sounds.PlayOneShot(_sport);

    public void HandleTieBreak() => _sounds.PlayOneShot(_timeBreak);
    public void HandleFinish() => _sounds.PlayOneShot(_finish);

    private void Awake() => EventBus.Subscribe(this);

    private void OnDestroy() => EventBus.Unsubscribe(this);
}