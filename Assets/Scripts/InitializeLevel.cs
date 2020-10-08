using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    Transform[] _playerSpawns;
    [SerializeField]
    GameObject _playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PlayerConfiguration[] _playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < _playerConfigs.Length; i++)

        {

            var player = Instantiate(_playerPrefab,
                                     _playerSpawns[i].position,
                                     _playerSpawns[i].rotation,
                                     gameObject.transform);

            player.GetComponent<PlayerController>().InitializePlayer(_playerConfigs[i]);
        }
    }

}
