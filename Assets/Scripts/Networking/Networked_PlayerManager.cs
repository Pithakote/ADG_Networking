using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Networked_PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] GameObject _colorSelectionPanel;
    [SerializeField] Transform [] _spawnPoints;
    public static Networked_PlayerManager Instance { get; set; }

    List<NetworkedPlayerDataConfiguration> _networkedDataConfig;
    [SerializeField] Player[] players;
    [SerializeField] Button _startGameButton;
    [SerializeField] TMP_Text _readyText;
    [SerializeField] public int _noOfRediedPlayers { get  ;  set ;  }
    public List<NetworkedPlayerDataConfiguration> NetworkedDataConfig
    { get {return _networkedDataConfig; } set { _networkedDataConfig = value; } }

   public bool _isReady;
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
        _isReady = false;
        players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            photonView.RPC("SpawnSelector", players[i], _spawnPoints[i].position, _spawnPoints[i].rotation);
           
        }
        _readyText.text = _noOfRediedPlayers + " / " + PhotonNetwork.PlayerList.Length.ToString() + " Players are ready";


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
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void OnDestroy()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;

    }
    public void EventRaiser()
    {
        object[] data = new object[] { _noOfRediedPlayers };

     //   PhotonNetwork.RaiseEvent(PLAYER_READY_EVENT, data, RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);

    }
    private void NetworkingClient_EventReceived(ExitGames.Client.Photon.EventData obj)
    {
      //  if (obj.Code == PLAYER_READY_EVENT)
      //  {
      //      Debug.Log("Event raised");
     //       object[] data = (object[])obj.CustomData;
        //    int _readyPlayers = (int)data[0];

        //    _noOfRediedPlayers = _readyPlayers;

        //    _noOfRediedPlayers++;
        
        //}
    }

    void Update()
    {
        if (_isReady)
        {
            _noOfRediedPlayers++;
            _readyText.text = _noOfRediedPlayers + " / " + PhotonNetwork.PlayerList.Length.ToString() + " Players are ready";

            if (_startGameButton &&
                _noOfRediedPlayers == PhotonNetwork.PlayerList.Length &&
                PhotonNetwork.IsMasterClient)
                _startGameButton.gameObject.SetActive(true);//only be interactable if the player is the host

            _isReady = false; 
        }

       
        //check the deadplayer and play music or show text
          
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("Testing");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      
    }
}
