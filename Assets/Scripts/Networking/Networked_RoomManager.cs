using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Networked_RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _colorSelectionPanel;
    [SerializeField] Transform [] _spawnPoints;
    public static Networked_RoomManager Instance { get; set; }

    List<NetworkedPlayerDataConfiguration> _networkedDataConfig;
    [SerializeField] Player[] players;
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

    private void Start()
    {
        players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            photonView.RPC("SpawnSelector", players[i], _spawnPoints[i].position, _spawnPoints[i].rotation);
           
        }
    }

    [PunRPC]
    void SpawnSelector(Vector3 pos, Quaternion rot)
    {
        if (Networked_ColorSelector.LocalSelectorInstance == null)
        {
            PhotonNetwork.Instantiate(this._colorSelectionPanel.name,
                                       pos,
                                        rot
                                        );
        }
    }
}
