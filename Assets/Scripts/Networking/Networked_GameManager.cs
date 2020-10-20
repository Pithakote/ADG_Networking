using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Networked_GameManager : MonoBehaviourPunCallbacks
{
    public GameObject _playerPrefab;

    private void Start()
    {
        PhotonNetwork.Instantiate(Path.Combine("NetworkedPrefabs", "PlayerConfiguration&MenuSetup_Networked"),
                                      Vector3.zero,
                                      Quaternion.identity);
        /*
       if (Networked_PlayerManager.LocalPlayerInstance == null)
       {
         PhotonNetwork.Instantiate(this._playerPrefab.name, Vector3.zero, Quaternion.identity);

       }
       else
       {
           Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
       }
       */
    }
}
