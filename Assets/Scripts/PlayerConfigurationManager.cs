using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    List<PlayerConfiguration> _playerConfigs;

    [SerializeField]
    int _maxPlayers = 2;
    
    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Singleton already exists");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            _playerConfigs = new List<PlayerConfiguration>();
        }
    }

    //for setting the color of the sprites/players
    public void SetPlayerColor(int index, Color32 spriteColor)
    {
        _playerConfigs[index].PlayerSpriteColor.color = spriteColor;
    }

    //for indicating when the players press "Ready"
    public void ReadyPlayer(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.Count == _maxPlayers && _playerConfigs.All(p => p.IsReady = true))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Number: " + pi.playerIndex + " Joined");
        
       
        //checking to seee if we haven't already added this player
        if (!_playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            //since this gameobject is not destroyed when switching to other scenes,
            //the same needs to happen to the players too
            pi.transform.SetParent(transform);
            _playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        Input = pi;
        PlayerIndex = pi.playerIndex;
       
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public SpriteRenderer PlayerSpriteColor {get; set;}
}
