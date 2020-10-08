﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerConfigurationManager : MonoBehaviour
{
    List<PlayerConfiguration> _playerConfigs;

    [SerializeField]
    int _currentPlayers;
    PlayerInputManager _playerInputManager;
    public static PlayerConfigurationManager Instance { get; private set; }

    public int CurrentPlayers
    {
        get { return _currentPlayers; }
        set { _currentPlayers = value; }
    }
    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
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
    private void Start()
    {
        _currentPlayers = 0;
    }
    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return _playerConfigs;
    }
    //for setting the color of the sprites/players
    public void SetPlayerColor(int index, Image spriteColor)
    {
        Debug.Log("Player: "+(index+1)+" "+_playerConfigs[index].IsReady);
        _playerConfigs[index].PlayerSpriteColor = spriteColor.color;
    }

    //for indicating when the players press "Ready"
    public void ReadyPlayer(int index)
    {
        if(!_playerConfigs[index].IsReady)
             _playerConfigs[index].IsReady = true;

        for (int i = 0; i < _playerConfigs.Count; i++)
        {
            Debug.Log("Player: " + (index + 1) + " " + _playerConfigs[index].IsReady);
        }
        if (_playerConfigs.Count == _currentPlayers
                                && _currentPlayers <= _playerInputManager.maxPlayerCount
                                && _playerConfigs.All(p => p.IsReady == true))
                     {            
                          SceneManager.LoadScene("SampleScene");
                          _playerInputManager.DisableJoining();//stops people from joining after the game has been set
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
            _playerConfigs[pi.playerIndex].IsReady = false;
            _currentPlayers += 1;

        }
        foreach (var player in _playerConfigs)
        Debug.Log("The number of players are: "+_playerConfigs.Count);
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
    public Color PlayerSpriteColor {get; set;}
}
