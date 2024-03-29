﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Linq;
using Photon.Realtime;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class Networked_GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject _playerPrefab;
    [SerializeField]Vector2[] _playerSpawnsPositions;
    [SerializeField]float _minPosValue = -5, _maxPosValue = 5;
    [SerializeField] PlayerColorAndShape _playerShapesAndColor;
    
    private ExitGames.Client.Photon.Hashtable _myCustomProperty = new ExitGames.Client.Photon.Hashtable();
    public List<NetworkedPlayerDataConfiguration> _playerConfig;
    [SerializeField] TMP_Text GameOverText;

    [SerializeField] int numberofdeadplayers;
    [SerializeField] bool isLocalPlayerDead;
    [SerializeField] int _playersInList;// = PhotonNetwork.PlayerList.Length;
    public int NumberOfDeadPlayers { get {return numberofdeadplayers; } set { numberofdeadplayers = value; } }
    public bool IsLocalClientDead { get {return isLocalPlayerDead; } set { isLocalPlayerDead = value; } }

    public static Networked_GameManager Instance { get; set; }

    // [SerializeField] AudioClip _winGameAudio, _loseGameAudio;
    //[SerializeField] AudioSource _audioPlayer;

    MusicHandler _musicHandlerInstance = MusicHandler.Instance;

    void Awake()
    {

        PhotonNetwork.SendRate = 40;
        PhotonNetwork.SerializationRate = 15;
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
     //   if(_audioPlayer == null)
      //  _audioPlayer = GetComponent<AudioSource>();
     //   SceneManager.sceneLoaded += OnSceneChanged;

        
    }
    private void Start()
    {
        _playersInList = PhotonNetwork.PlayerList.Length;
        NumberOfDeadPlayers = 0;
        IsLocalClientDead = false;

        Debug.Log("The number of players are: "+ PhotonNetwork.PlayerList.Length);
        _playerConfig = Networked_PlayerManager.Instance.NetworkedDataConfig;
        AssignShapes();



    }

    void Update()
    { 
        if(NumberOfDeadPlayers == _playersInList - 1)
        {
            if (IsLocalClientDead)
            {
                GameOverText.text = "Game Over";
                //  if (_loseGameAudio)
                //     _audioPlayer.clip = _loseGameAudio;

                _musicHandlerInstance.ChangeMusic(MusicType.LoseMusic);

            }
            else
            {

                GameOverText.text = "You won";
                //  if (_winGameAudio)
                //    _audioPlayer.clip = _winGameAudio;

                _musicHandlerInstance.ChangeMusic(MusicType.WinMusic);
            }
          //  if (!_musicHandlerInstance._audioSource.isPlaying)
          //  if(!_audioPlayer.isPlaying)//without this the _audioPlayer will start playing every frame
          //  _audioPlayer.Play();
        }
    }
    void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        if (scene != SceneManager.GetActiveScene())
        {
            //   if(_audioPlayer.isPlaying)
            //  _audioPlayer.Stop();
         //   _musicHandlerInstance.ChangeMusic(MusicType.GeneralMusic);

        }
    }

    private void SetSpawnPosition(int i)
    {
        Vector2 _newSpawnPos = new Vector2(Random.Range(_minPosValue, _maxPosValue),
                                         Random.Range(_minPosValue, _maxPosValue));


        if (_playerSpawnsPositions.Any(sp => sp == _newSpawnPos))
            return ;

        _playerSpawnsPositions[i] = _newSpawnPos;
        
    }

    void AssignShapes()
    {
        Player[] _players = PhotonNetwork.PlayerList;
      
        for (int i = 0;i<_players.Length;i++)
        {
            Vector2 _newSpawnPos = new Vector2(Random.Range(_minPosValue, _maxPosValue),
                                           Random.Range(_minPosValue, _maxPosValue));
           

            if (NetworkedPlayer.LocalPlayerInstance == null)
            {


                var _player = PhotonNetwork.Instantiate(this._playerPrefab.name,
                                                        _newSpawnPos,
                                                        transform.rotation          );

                _player.GetComponent<NetworkedPlayer>().InitialisePlayer(_playerConfig[i]);

                
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }


        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(NumberOfDeadPlayers);
           // stream.SendNext(IsLocalClientDead);
        }
        else
        {
            this.NumberOfDeadPlayers = (int)stream.ReceiveNext();
          //  this.IsLocalClientDead = (bool)stream.ReceiveNext();
        }
    }
}
