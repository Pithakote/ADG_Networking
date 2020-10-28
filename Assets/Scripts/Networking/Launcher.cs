using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using WebSocketSharp;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _roomNameInputField, _playerNameInputField;
    [SerializeField] TMP_Text _errorText, _roomName;
    [SerializeField] Transform _roomListContent, _playerListContent;
    [SerializeField] GameObject _roomListItemPrefab, _playerListItemPrefab;
    [SerializeField] Button _startGameButton;
    [SerializeField] int _numberOfPlayersAllowedInRoom = 4, _numberOfMinimumPlayersNeeded = 2;
    public static Launcher Instance;

    

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;

        Destroy(GameObject.Find("Networked_PlayerManager"));
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.OfflineMode = false;
      //  _networkedPlayerConfig = new List<NetworkedPlayerDataConfiguration>();
        Debug.Log("Connecting to Master");
        // CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.LoadingScreen);

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void NameSetter()
    {
        if (_playerNameInputField.text.IsNullOrEmpty())
        {
            PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");

        }
        else
        {
            PhotonNetwork.NickName = _playerNameInputField.text;
        }
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.MultiplayerOptionsScene);
       // PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");

    }
    // Update is called once per frame
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomNameInputField.text))
            return;
        NameSetter();
        PhotonNetwork.CreateRoom(_roomNameInputField.text);
       

        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.LoadingScreen);
        //PhotonNetwork.CreateRoom("");  //for random room names
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.CurrentRoom.MaxPlayers = System.Convert.ToByte(_numberOfPlayersAllowedInRoom);
        if (!PhotonNetwork.CurrentRoom.IsOpen && !PhotonNetwork.CurrentRoom.IsVisible)
        {
            SetRoomVisibilityAndJoining(true);
           
            
            
        }
    }
    //works for both oncreate a room and onjoined room
    public override void OnJoinedRoom()
    {

        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.RoomMenu);
        _roomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform children in _playerListContent)
            Destroy(children.gameObject);

       // foreach (Transform colorSelectors in _colorSelectorPanel)
        //    Destroy(colorSelectors.gameObject);

        foreach (Player _player in players)
        {
            //GameObject _selector =  PhotonNetwork.Instantiate(this._colorSelector.name,
            //                                                  _colorSelectorPanel.transform.position,
            //                                                  Quaternion.identity,
            //                                                  0);
            //_selector.transform.parent = _colorSelectorPanel.transform;
            Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(_player);
            //if (!PhotonNetwork.LocalPlayer.IsLocal)
             //   _selector.GetComponent<Networked_ColorSelector>().GetEventSystem().SetActive(false);
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount >= _numberOfMinimumPlayersNeeded &&
           PhotonNetwork.IsMasterClient)
            _startGameButton.gameObject.SetActive(true);//only be interactable if the player is the host
        else
            _startGameButton.gameObject.SetActive(false);//only be interactable if the player is the host

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= _numberOfMinimumPlayersNeeded &&
           PhotonNetwork.IsMasterClient)
            _startGameButton.gameObject.SetActive(true);//only be interactable if the player is the host
        else
            _startGameButton.gameObject.SetActive(false);//only be interactable if the player is the host

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Room Creation failed: "+ message;
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.ErrorMenu);

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.LoadingScreen);
    }

    public void JoinRoom(RoomInfo info)
    {
        NameSetter();
        PhotonNetwork.JoinRoom(info.Name);
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.LoadingScreen);

       
    }

    public void StartGame()
    {
        SetRoomVisibilityAndJoining(false);
        PhotonNetwork.LoadLevel("NetworkedColorSelection");
        //PhotonNetwork.LoadLevel("Testing");
     //  PhotonNetwork.LoadLevel("NetworkedSampleScene");
       // PhotonNetwork.LoadLevel("PlayerSelection");
    }
    public override void OnLeftRoom()
    {

        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.MultiplayerOptionsScene);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform _trans in _roomListContent)
            Destroy(_trans.gameObject);

        foreach (RoomInfo _room in roomList)
        {
            if (_room.RemovedFromList)
                continue;
            Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(_room);
        }

       
      
    }

   
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnedted because: "+cause);
        PhotonNetwork.LoadLevel("NetworkedSelectionScene");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //GameObject _selector = PhotonNetwork.Instantiate(this._colorSelector.name,
        //                                                       _colorSelectorPanel.transform.position,
        //                                                       Quaternion.identity,
        //                                                       0);

        //    _selector.transform.parent = _colorSelectorPanel.transform;

        if (PhotonNetwork.CurrentRoom.PlayerCount >= _numberOfMinimumPlayersNeeded &&
           PhotonNetwork.IsMasterClient)
            _startGameButton.gameObject.SetActive(true);//only be interactable if the player is the host
        else
            _startGameButton.gameObject.SetActive(false);//only be interactable if the player is the host


        Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
       // Networked_PlayerManager.Instance.NetworkedDataConfig.Add(new NetworkedPlayerDataConfiguration(newPlayer));


        //if (!PhotonNetwork.LocalPlayer.IsLocal)
        //    _selector.GetComponent<Networked_ColorSelector>().GetEventSystem().SetActive(false);
        //  Instantiate(_colorSelector, _colorSelectorPanel);

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            SetRoomVisibilityAndJoining(false);
    }

    void SetRoomVisibilityAndJoining(bool state)
    {
        PhotonNetwork.CurrentRoom.IsOpen = state; 
        PhotonNetwork.CurrentRoom.IsVisible = state;
    }
}
