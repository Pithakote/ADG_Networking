using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Networked_GameManager : MonoBehaviourPunCallbacks
{
    public GameObject _playerPrefab;

    private void Start()
    {
        if (Networked_PlayerManager.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(this._playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }
}
