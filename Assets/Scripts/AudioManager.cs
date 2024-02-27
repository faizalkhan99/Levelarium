using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("AudioManager:NULL");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        PlayBGM();
    }
    [SerializeField] AudioSource _bgm;
    [SerializeField] AudioSource _playSFX;
    public void PlayBGM()
    {
        _bgm.PlayDelayed(0.3f);
    }
    public void PauseBGM()
    {
        _bgm.Pause();
    }
    public void PLaySFX(AudioClip _clip)
    {
        _playSFX.clip = _clip;
        _playSFX.Play();        
    }
}
