using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _sounds;
    [Space]
    [SerializeField] private AudioClip _tap;
    [SerializeField] private AudioClip _startFinish;
    [SerializeField] private AudioClip _sport;
    [SerializeField] private AudioClip _timeBreak;

    public void PlayTap() => _sounds.PlayOneShot(_tap);
    
    public void PlayStartFinish() => _sounds.PlayOneShot(_startFinish);
    
    public void PlaySport() => _sounds.PlayOneShot(_sport);
    
    public void PlayTimeBreak() => _sounds.PlayOneShot(_timeBreak);
}
