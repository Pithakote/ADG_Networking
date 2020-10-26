using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class SpawnPlayerSetupMenu : MonoBehaviourPunCallbacks
{
    GameObject _mainLayout;

    [SerializeField]
    GameObject PlayerSetupMenuPrefab;

    PlayerInput _playerInput;

    [SerializeField] bool _isNetworked = false;
    GameObject _gameMenu;
    private void Awake()
    {

        _playerInput = GetComponent<PlayerInput>();
        _mainLayout = GameObject.Find("MultiplayerCanvas");
        //_mainLayout = GameObject.Find("UI_MainLayout");
        // _mainLayout = GameObject.Find("Canvas_LocalPlay");

        //   _mainLayout = GameObject.Find("Canvas");
    }

    private void Start()
    {
        if (_mainLayout != null)
        {
            if (!_isNetworked)
            {
                
                _gameMenu = Instantiate(PlayerSetupMenuPrefab, _mainLayout.transform);
                PlayerConfigurationManager.Instance.ListOfMenuUI.Add(_gameMenu);
                _playerInput.uiInputModule = _gameMenu.GetComponentInChildren<InputSystemUIInputModule>();//assigns the input to control the UI elements
                _gameMenu.GetComponent<PlayerSetupMenu>().SetPlayerIndex(_playerInput.playerIndex);

            }
           // if (_isNetworked && Networked_PlayerManager.LocalPlayerInstance == null)
         
        }
        else 
        {
           _gameMenu  =  PhotonNetwork.Instantiate(this.PlayerSetupMenuPrefab.name, Vector3.zero, Quaternion.identity) ;
            _gameMenu.GetComponent<NetworkedPlayer>().PlayerInput = _playerInput;
            //  PhotonNetwork.Instantiate(Path.Combine("NetworkedPrefabs", "Player_Networked"),
           //                     Vector3.zero,
            //                    Quaternion.identity);

        }
    }
}

