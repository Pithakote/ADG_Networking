using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Networked_RoomManager : MonoBehaviourPunCallbacks
{
    public static Networked_RoomManager Instance { get; set; }

    List<NetworkedPlayerDataConfiguration> _networkedDataConfig;
    public List<NetworkedPlayerDataConfiguration> NetworkedDataConfig
    { get {return _networkedDataConfig; } set { _networkedDataConfig = value; } }
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            NetworkedDataConfig = new List<NetworkedPlayerDataConfiguration>();
        }

    }


}
