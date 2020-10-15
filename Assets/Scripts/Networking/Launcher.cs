using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _roomNameInputField;
    [SerializeField] TMP_Text _errorText, _roomName;
    [SerializeField] Transform _roomListContent, _playerListContent;
    [SerializeField] GameObject _roomListItemPrefab, _playerListItemPrefab;
    [SerializeField] Button _startGameButton;

    public static Launcher Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Master");
       // CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.LoadingScreen);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.MultiplayerOptionsScene);
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
    }
    // Update is called once per frame
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomNameInputField.text))
            return;


        PhotonNetwork.CreateRoom(_roomNameInputField.text);
       
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.LoadingScreen);
        //PhotonNetwork.CreateRoom("");  //for random room names
    }
    //works for both oncreate a room and onjoined room
    public override void OnJoinedRoom()
    {

        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.RoomMenu);
        _roomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Player _player in players)
            Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(_player);

        _startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);//only be interactable if the player is the host
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);//only be interactable if the player is the host
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
        PhotonNetwork.JoinRoom(info.Name);
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.LoadingScreen);

       
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("NetworkedSampleScene");
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
            Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(_room);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);

    }
}
