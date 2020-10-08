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
        _mainLayout = GameObject.Find("UI_MainLayout");
        if (_mainLayout != null)
        {
            GameObject _gameMenu = Instantiate(PlayerSetupMenuPrefab, _mainLayout.transform);
            _playerInput.uiInputModule = _gameMenu.GetComponentInChildren<InputSystemUIInputModule>();//assigns the input to control the UI elements
            _gameMenu.GetComponent<PlayerSetupMenu>().SetPlayerIndex(_playerInput.playerIndex);
           //  Debug.Log("The number of players: " + PlayerConfigurationManager.Instance.MaxPlayers);
        }
    }
}
