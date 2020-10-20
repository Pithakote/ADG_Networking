using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Linq;
using Photon.Realtime;

public class Networked_GameManager : MonoBehaviourPunCallbacks
{
    public GameObject _playerPrefab;
    [SerializeField]Vector2[] _playerSpawnsPositions;
    [SerializeField]float _minPosValue, _maxPosValue;
    [SerializeField] PlayerColorAndShape _playerShapesAndColor;
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


        
       if (Networked_PlayerManager.LocalPlayerInstance == null)
       {
        var _player = PhotonNetwork.Instantiate(this._playerPrefab.name,
                                                Vector3.zero,
                                                Quaternion.identity
                                                );
            _player.transform.parent = gameObject.transform;
       }
       else
       {
           Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
       }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Networked_PlayerManager>().SpriteRendererComponent.sprite = _playerShapesAndColor._playerShape[i];

        }
      
    }

    private void SetSpawnPosition(int i)
    {
        _playerSpawnsPositions[i] = Vector2.zero;
        Vector2 _newSpawnPos = new Vector2(Random.Range(_minPosValue, _maxPosValue),
                                         Random.Range(_minPosValue, _maxPosValue));


        if (_playerSpawnsPositions == null &&
            _playerSpawnsPositions.Any(sp => sp == _newSpawnPos))
            return;

        _playerSpawnsPositions[i] = _newSpawnPos;
    }
}
