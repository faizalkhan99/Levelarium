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
    [SerializeField] AudioSource _fireBulletSFX;
    [SerializeField] AudioSource _keySpawnedSFX;
    [SerializeField] AudioSource _keyObtainedSFX;
    [SerializeField] AudioSource _evilMachineDeadSFX;
    [SerializeField] AudioSource _levelCompletedSFX;
    public void PlayBGM()
    {
        _bgm.PlayDelayed(0.3f);
    }
    public void PauseBGM()
    {
        _bgm.Pause();
    }
    public void FireBulletSFX(AudioClip _clip)
    {
        if (_clip)
        {
            _fireBulletSFX.clip = _clip;
            _fireBulletSFX.Play();
        }
    }
    public void KeySpawnedSFX(AudioClip _clip)
    {
        if (_clip)
        {
            _keySpawnedSFX.clip = _clip;
            _keySpawnedSFX.Play();
        }
    }public void KeyObtainedSFX(AudioClip _clip)
    {
        if (_clip)
        {
            _keyObtainedSFX.clip = _clip;
            _keyObtainedSFX.Play();
        }
    }
    public void LevelCompletedSFX(AudioClip _clip)
    {
        if (_clip)
        {
            _levelCompletedSFX.clip = _clip;
            _levelCompletedSFX.Play();
        }
    }
    public void EvilMachineDeadSFX(AudioClip _clip)
    {
        if (_clip)
        {
            _evilMachineDeadSFX.clip = _clip;
            _evilMachineDeadSFX.Play();
        }
    }
}
