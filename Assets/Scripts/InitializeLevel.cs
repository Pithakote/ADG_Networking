using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
//using Unity.Mathematics;

public class InitializeLevel : MonoBehaviour
{
  //  [SerializeField]
   // GameObject _spawnPrefab;
    [SerializeField]
    Vector2[] _playerSpawnsPositions;
    [SerializeField]
    GameObject _playerPrefab;

  
   // [SerializeField]
    //Transform[] _spawnPosition;
    // Start is called before the first frame update

   // Random rnd = new Random();
    [SerializeField]
    float _minPosValue, _maxPosValue;
    void Start()
    {
       
        //sets the arrya length to the current number of players 
        //since, PlayerConfigurationManager is don't destroy on load, it'll find it
        _playerSpawnsPositions = new Vector2[PlayerConfigurationManager.Instance.CurrentPlayers];

       
        PlayerDataConfiguration[] _playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < _playerConfigs.Length; i++)

        {
            SetSpawnPosition(i);

            var player = Instantiate(_playerPrefab,
                                     _playerSpawnsPositions[i],
                                     _playerPrefab.transform.rotation,
                                     gameObject.transform);

            player.GetComponent<OfflinePlayer>().InitializePlayer(_playerConfigs[i]);
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
