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

    public void SetPlayerColor(int index, Color32 spriteColor)
    {
        _playerConfigs[index].PlayerSpriteColor.color = spriteColor;
    }

    public void ReadyPlayer(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.Count == _maxPlayers && _playerConfigs.All(p => p.IsReady = true))
        {
            SceneManager.LoadScene("SampleScene");
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
