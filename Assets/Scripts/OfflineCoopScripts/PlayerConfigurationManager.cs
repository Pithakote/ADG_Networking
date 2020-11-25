using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerConfigurationManager : MonoBehaviour
{
        List<PlayerDataConfiguration> _playerConfigs;
        public List<PlayerDataConfiguration> PlayerConfigs { get { return _playerConfigs; } set { _playerConfigs = value; } }
        [SerializeField]int _currentPlayers;
        PlayerInputManager _playerInputManager;
        public static PlayerConfigurationManager Instance { get; private set; }
    
        [SerializeField] List<GameObject> _listOfMenuUI;

        public List<GameObject> ListOfMenuUI { get { return _listOfMenuUI; } set { _listOfMenuUI = value; } }
        [SerializeField] PlayerPropertySetting _setPlayerSetting;

        public delegate void onSelectLocal(Button _btn);
        public event onSelectLocal ButtonSelectEvent;
  
         public PlayerPropertySetting SetPlayerProperty { get {return _setPlayerSetting; } }
         [SerializeField] TMP_Text _noOfPlayersJoinedText; 
        public int CurrentPlayers
        {
            get { return _currentPlayers; }
            set { _currentPlayers = value; }
        }
        private void Awake()
        {
         PhotonNetwork.OfflineMode = true;

        //   ListOfMenuUI = new List<GameObject>();
        _setPlayerSetting = GetComponent<PlayerPropertySetting>();
           _playerInputManager = GetComponent<PlayerInputManager>();
            if (Instance != null)
            {
            //}
              Destroy(this);
            }
            else
            { 
                
                Instance = this;
                DontDestroyOnLoad(Instance);
                _playerConfigs = new List<PlayerDataConfiguration>();
            }

       //SceneManager.activeSceneChanged += DestroyInMainMenu;
        }
        private void Start()
        {
            _currentPlayers = 0;
        _noOfPlayersJoinedText.text = _currentPlayers + "/4 Players Joined";
    }
  
        public List<PlayerDataConfiguration> GetPlayerConfigs()
        {
            return _playerConfigs;
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
                   SceneManager.LoadScene("LocalGameLevel");
                   _playerInputManager.DisableJoining();//stops people from joining after the game has been set
                }
        }

        public void HandlePlayerJoin(PlayerInput pi)
        {
            //checking to seee if we haven't already added this player
            if (!_playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
            {
                //since this gameobject is not destroyed when switching to other scenes,
                //the same needs to happen to the players too
                pi.transform.SetParent(transform);
                _playerConfigs.Add(new PlayerDataConfiguration(pi));
                 //_setPlayerSetting.PlayerConfigs.Add(new PlayerDataConfiguration(pi));
                _playerConfigs[pi.playerIndex].IsReady = false;
               
                _currentPlayers += 1;

            _noOfPlayersJoinedText.text = _currentPlayers + "/4 Players Joined";

            }
        }
    public void SetPlayerColor(int index, Image spriteColor)
    {
        Debug.Log("Player: " + (index + 1) + " " + _playerConfigs[index].IsReady);
        _playerConfigs[index].PlayerSpriteColor = spriteColor.color;
    }
    public void SetShape(int index, Sprite PlayerShape)
    {
        _playerConfigs[index].PlayerShape = PlayerShape;
    }
    public void ButtonEventRaiser(Button _btn)
    {
        if (ButtonSelectEvent != null)
            ButtonSelectEvent(_btn);
    }

   
    /*

public delegate void OnButtonSelect(Button _btn);
public  event OnButtonSelect ButtonSelectEvent;

public void ButtonEventRaiser(Button _btn)
{ 
  if (ButtonSelectEvent != null)
      ButtonSelectEvent(_btn);
}


   public void SetPlayerShape(int index, Sprite spriteShape)
   {
       if (ButtonSelectEvent == null)
           return;

       _playerConfigs[index].PlayerShape = spriteShape;
   }
*/
}
/*
public class PlayerDataConfiguration
{
    public PlayerDataConfiguration(PlayerInput pi)
    {
        Input = pi;
        PlayerIndex = pi.playerIndex;
       
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; } 
    public Color PlayerSpriteColor {get; set;}
}
*/
