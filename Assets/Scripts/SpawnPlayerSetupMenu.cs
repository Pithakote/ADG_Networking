using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    GameObject _mainLayout;

    [SerializeField]
    GameObject PlayerSetupMenuPrefab;

    PlayerInput _playerInput;

  
    private void Awake()
    {
        
        _playerInput = GetComponent<PlayerInput>();
        //_mainLayout = GameObject.Find("UI_MainLayout");
       // _mainLayout = GameObject.Find("Canvas_LocalPlay");
        _mainLayout = GameObject.Find("MultiplayerCanvas");
     //   _mainLayout = GameObject.Find("Canvas");
        if (_mainLayout != null)
        {
            GameObject _gameMenu = Instantiate(PlayerSetupMenuPrefab, _mainLayout.transform);
            PlayerConfigurationManager.Instance.ListOfMenuUI.Add(_gameMenu);
            _playerInput.uiInputModule = _gameMenu.GetComponentInChildren<InputSystemUIInputModule>();//assigns the input to control the UI elements
            _gameMenu.GetComponent<PlayerSetupMenu>().SetPlayerIndex(_playerInput.playerIndex);
           }
    }
}
