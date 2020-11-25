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

    //----------Subsctibing and UnSubscribing the OnSceneChanged function 
    //----------which is triggered everytime the scene changes
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

    //--------------------------------------


    //used awake instead of start because the code needs to declare 
    //some of the variables before the SceneManager.sceneload event triggers
    private void Awake()
    {

        Debug.Log("Awake Debug");


        //---------Creating singleton----------------
        #region Singleton
        if (Instance)
        {
            Destroy(this.gameObject);

            
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        #endregion
        //-----------------------


        //null check
        if (this._audioSource == null)
            this._audioSource = GetComponent<AudioSource>();



        //creating a new Dictionary object to store music type and music
        //the key is enum for music type and the value is audio clip
        _audioClips = new Dictionary<MusicType, AudioClip>();
        _audioClips.Add(MusicType.WinMusic, _winMusic);
        _audioClips.Add(MusicType.LoseMusic, _loseMusic);
        _audioClips.Add(MusicType.GeneralMusic, _generalMusic);

        //-----sets the initial music to general type
        //-----plays the general music from the dictionary when the application
        //-----first starts
        this._audioSource.clip = _audioClips[MusicType.GeneralMusic];

        this._audioSource.Play();

        //----------------------
    }



    //---------a public function used by game managers to change the music type 
    //---------according to the condition.
    public void ChangeMusic(MusicType _musicType)
    {


        //pause the currently playing music to prevent multiple audio clips playing at the same time
        this._audioSource.Pause();
        this._audioSource.clip = _audioClips[_musicType];
        this._audioSource.Play();
    }

    //-------------------------------------------------------------------------------------------


    //----------function that has been subscribed to SceneManager.sceneload to change music when the scene changes
    void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {

        Debug.Log("SceneLoaded Debug");

        //null check and scene check    
        if (this._audioSource.clip != _audioClips[MusicType.GeneralMusic] &&
            scene.name != "NetworkedGameLevel" &&
            this._audioSource != null 
            )
        {
           
            
            
             
            ChangeMusic(MusicType.GeneralMusic);
        }
    }

    //-----------------------------------------------------------------
}


//------Enum to be used to set and change the music type
public enum MusicType 
{ 
    WinMusic,
    LoseMusic,
    GeneralMusic
}

//-----------------------------------------------------------------