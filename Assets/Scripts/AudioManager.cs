using Unity.VisualScripting;
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
        if (_instance == null)
        {
            _instance = this;
        }
    }
    private void Start()
    {
        PlayBGM();
    }
    [SerializeField] AudioSource _bgm;
    [SerializeField] AudioSource[] _SFXAudioSources;


    public void PlayBGM()
    {
        _bgm.PlayDelayed(0.3f);
    }
    public void PauseBGM()
    {
        _bgm.Pause();
    }
    
    public void PlaySFX(AudioClip audio/*, float vol*/)
    {
        for (int i = 0; i < _SFXAudioSources.Length; i++)
        {
            if (!_SFXAudioSources[i].isPlaying)
            {
                _SFXAudioSources[i].clip = audio;
                //_SFXAudioSources[i].volume = vol;
                _SFXAudioSources[i].Play();
                break;
            }
        }
    }
  
}
