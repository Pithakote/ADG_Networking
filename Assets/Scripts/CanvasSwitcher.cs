using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviourPunCallbacks
{
    public CanvasTypesInsideScenes _desiredCanvasType;
    CanvasManager _canvasManager;
    Button menuButton;
    // Start is called before the first frame update
    void Start()
    {
      //  menuButton = GetComponent<Button>();
     //   menuButton.onClick.AddListener(OnButtonClick);
        _canvasManager = CanvasManager.Instance;
    }

    public void OnButtonClick()
    {
        _canvasManager.SwitchCanvas(_desiredCanvasType);

    }
    public void LeaveGame()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
          //  _canvasManager.SwitchCanvas(_desiredCanvasType);
          //  PhotonNetwork.Disconnect();
            //   ChangeScene();
        }
        else
        {
            Debug.Log("Offline Mode changing scene");
            ChangeScene();
        }
            // _canvasManager.SwitchCanvas(_desiredCanvasType);
        

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnedted because: " + cause);
        PhotonNetwork.LoadLevel("NetworkedSelectionScene");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Online Mode changing scene");
      //  PhotonNetwork.Disconnect();
        ChangeScene();
    }

    void ChangeScene()
    {
        //SceneManager.LoadScene("NetworkedSelectionScene");
        PhotonNetwork.LoadLevel("NetworkedSelectionScene");
    }

}
