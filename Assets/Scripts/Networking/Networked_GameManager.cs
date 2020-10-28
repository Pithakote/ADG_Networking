﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Linq;
using Photon.Realtime;
using UnityEngine.InputSystem;

public class Networked_GameManager : MonoBehaviourPunCallbacks
{
    public GameObject _playerPrefab;
    [SerializeField]Vector2[] _playerSpawnsPositions;
    [SerializeField]float _minPosValue = -5, _maxPosValue = 5;
    [SerializeField] PlayerColorAndShape _playerShapesAndColor;

    private ExitGames.Client.Photon.Hashtable _myCustomProperty = new ExitGames.Client.Photon.Hashtable();
    public List<NetworkedPlayerDataConfiguration> _playerConfig; 
    //public Networked_GameManager Instance { get; set; }
    //private void Awake()
    //{
    //    if (Instance)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(this);
    //    }
    //    else
    //    {
    //        Destroy(this);
    //    }
    //}
    private void Start()
    {
        #region Newcode
        //replace with the number of players in the room
        /*
        _playerSpawnsPositions = new Vector2[PlayerConfigurationManager.Instance.CurrentPlayers];

        PlayerDataConfiguration[] _playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < _playerConfigs.Length; i++)

        {
            SetSpawnPosition(i);

           // var player = Instantiate(_playerPrefab,
          //                           _playerSpawnsPositions[i],
           //                          _playerPrefab.transform.rotation,
           //                          gameObject.transform);

            var player = PhotonNetwork.Instantiate(this._playerPrefab.name,
                                                   Vector3.zero,
                                                   Quaternion.identity);


            player.GetComponent<PlayerController>().InitializePlayer(_playerConfigs[i]);
        }
        */
        #endregion
        // PhotonNetwork.Instantiate(Path.Combine("NetworkedPrefabs", "PlayerConfiguration&MenuSetup_Networked"),
        //                      Vector3.zero,
        //                  Quaternion.identity);

        Debug.Log("The number of players are: "+ PhotonNetwork.PlayerList.Length);
        _playerConfig = Networked_RoomManager.Instance.NetworkedDataConfig;
        AssignShapes();



    }

   

    private void SetSpawnPosition(int i)
    {
       // _playerSpawnsPositions[i] = Vector2.zero;
        Vector2 _newSpawnPos = new Vector2(Random.Range(_minPosValue, _maxPosValue),
                                         Random.Range(_minPosValue, _maxPosValue));


        //  if (_playerSpawnsPositions == null &&
        if (_playerSpawnsPositions.Any(sp => sp == _newSpawnPos))
            return ;

        _playerSpawnsPositions[i] = _newSpawnPos;
        
    }

    void AssignShapes()
    {
        Player[] _players = PhotonNetwork.PlayerList;

        //  _myCustomProperty.Add("PlayerIndexNumber", photonView.Owner.ActorNumber);

       // _myCustomProperty.Add("PlayerIndexNumber", PhotonNetwork.LocalPlayer.ActorNumber);
     //   Debug.Log("GameManager actor number is " + PhotonNetwork.LocalPlayer.ActorNumber);
      //  PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperty);
      
        for (int i = 0;i<_players.Length;i++)
        {
            //  SetSpawnPosition(i);
            Vector2 _newSpawnPos = new Vector2(Random.Range(_minPosValue, _maxPosValue),
                                           Random.Range(_minPosValue, _maxPosValue));
            //photonView.RPC("RPCAssignPlayerData",
            //                _players[i],
            //                _newSpawnPos,
            //                Quaternion.identity,
            //                _players[i]); 

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

    [PunRPC]
    void RPCAssignActorNumber()
    {
        _myCustomProperty["PlayerIndexNumber"] = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("GameManager actor number is " + PhotonNetwork.LocalPlayer.ActorNumber);
        PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperty;
    }



    [PunRPC]
    void RPCAssignPlayerData(Vector2 _spawnPosition, Quaternion _spawnRotation,  Player thisPlayer)
    {
        if (NetworkedPlayer.LocalPlayerInstance == null)
        {
            

            var _player = PhotonNetwork.Instantiate(this._playerPrefab.name,
                                                    _spawnPosition,
                                                    _spawnRotation
                                                    );

           

        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }

        //for (int i = 0; i < gameObject.transform.childCount; i++)
        //{
        //    gameObject.transform.GetChild(i).GetComponent<Networked_PlayerManager>().SpriteRendererComponent.sprite = _playerShapesAndColor._playerShape[i];

        //}
    }
}
