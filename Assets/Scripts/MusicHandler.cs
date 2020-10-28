using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler Instance { get; set; }

     public AudioSource _audioSource;
    [SerializeField] AudioClip _generalMusic, _winMusic, _loseMusic;
    [SerializeField] Dictionary<MusicType, AudioClip> _audioClips;


    private void Awake()
    {

        Debug.Log("Awake Debug");
        if (Instance)
        {
            Destroy(this.gameObject);

            
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if (this._audioSource == null)
            this._audioSource = GetComponent<AudioSource>();

        _audioClips = new Dictionary<MusicType, AudioClip>();
        _audioClips.Add(MusicType.WinMusic, _winMusic);
        _audioClips.Add(MusicType.LoseMusic, _loseMusic);
        _audioClips.Add(MusicType.GeneralMusic, _generalMusic);

        this._audioSource.clip = _audioClips[MusicType.GeneralMusic];

        this._audioSource.Play();
    }

    private void OnEnable()
    {


        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private void OnDisable()
    {

        SceneManager.sceneLoaded -= OnSceneChanged;
    }

    private void OnDestroy()
    {
        

            SceneManager.sceneLoaded -= OnSceneChanged;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Debug");
       
    }

    public void ChangeMusic(MusicType _musicType)
    {


        this._audioSource.Pause();
        this._audioSource.clip = _audioClips[_musicType];
        this._audioSource.Play();
    }
    void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {

        Debug.Log("SceneLoaded Debug");
        if (this._audioSource.clip != _audioClips[MusicType.GeneralMusic] &&
            scene.name != "Testing" &&
            this._audioSource != null 
            )
        {
            //   if(_audioPlayer.isPlaying)
            //  _audioPlayer.Stop();
            
            
             
            ChangeMusic(MusicType.GeneralMusic);
        }
    }
}

public enum MusicType 
{ 
    WinMusic,
    LoseMusic,
    GeneralMusic
}
