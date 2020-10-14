using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _roomNameInputField;
    [SerializeField] TMP_Text _errorText, _roomName;
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
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.MultiplayerOptionsScene);
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

    public override void OnLeftRoom()
    {
        CanvasManager.Instance.SwitchCanvas(CanvasTypesInsideScenes.MultiplayerOptionsScene);
    }
}
