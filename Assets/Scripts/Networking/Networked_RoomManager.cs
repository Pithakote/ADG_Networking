using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Networked_RoomManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] GameObject _colorSelectionPanel;
    [SerializeField] Transform [] _spawnPoints;
    public static Networked_RoomManager Instance { get; set; }

    List<NetworkedPlayerDataConfiguration> _networkedDataConfig;
    [SerializeField] Player[] players;
    [SerializeField] Button _startGameButton;
    public List<NetworkedPlayerDataConfiguration> NetworkedDataConfig
    { get {return _networkedDataConfig; } set { _networkedDataConfig = value; } }


    public int ColorNumber { get; set; }
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
        }
        NetworkedDataConfig = new List<NetworkedPlayerDataConfiguration>();

    }

    private void Start()
    {
        players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            photonView.RPC("SpawnSelector", players[i], _spawnPoints[i].position, _spawnPoints[i].rotation);
           
        }
        if(_startGameButton)
        _startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);//only be interactable if the player is the host

    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (_startGameButton)
            _startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);//only be interactable if the player is the host
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

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("Testing");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
