using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;
using Photon.Pun.UtilityScripts;

public class Networked_ColorSelector : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] TMP_Text _playerNameDisplayText;
    [SerializeField] Button[] _buttonsInPanel;
    [SerializeField] Networked_ColorSelector[] _playerInputInSelectionPanel;
    [SerializeField] GameObject _localEventSystem;
    public static GameObject LocalSelectorInstance;
    [SerializeField] string _playerName;
    List<NetworkedPlayerDataConfiguration> _networkedPlayerDataConfig;
    [SerializeField] int _localPlayerNumber, _ownerPlayerNumber, _getPlayerNumber;
    private void Awake()
    {
        transform.SetParent(GameObject.Find("ColorSelectorPanel").transform);
      //  transform.position = Vector3.zero;
        Networked_RoomManager.Instance.NetworkedDataConfig.Add(new NetworkedPlayerDataConfiguration(PhotonNetwork.LocalPlayer));
        _playerName = photonView.Owner.NickName;
        _playerNameDisplayText.text = _playerName;

        if (photonView.IsMine)
        {
            LocalSelectorInstance = this.gameObject;
        }
        else
        {
            _localEventSystem.SetActive(false);
        }

        _localPlayerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        _ownerPlayerNumber = photonView.Owner.ActorNumber;
        _getPlayerNumber = photonView.Owner.GetPlayerNumber();
     //   photonView.RPC("EventHandlerStatus", RpcTarget.Others, null );

        //foreach (PlayerInput _inputComponent in _playerInputInSelectionPanel)
        //{

        //GetComponent<PlayerInput>().enabled = false;
        // }

        // photonView.RPC("EventHandlerStatus", RpcTarget.All, null);
        photonView.RPC("InitialFunctionsToBeShared", RpcTarget.Others, _playerName);
    }

    [PunRPC]
    void InitialFunctionsToBeShared(string _pName)
    {
        _playerNameDisplayText.text = _pName;
    }
    public GameObject GetEventSystem()
    {
        return _localEventSystem;
    }
    public void SetupNetworkedPanel(string _playerName, int _playerId)
    {
     

        _buttonsInPanel = transform.GetComponentsInChildren<Button>();
        

    }

    public void NetworkedSetColor(int _colorNumber)
    {
        //  Networked_RoomManager.Instance.NetworkedDataConfig[photonView.Controller.ActorNumber - 1].NetworkedPlayerSpriteColor = _colorNumber;
        Networked_RoomManager.Instance.ColorNumber = _colorNumber;
        //  photonView.RPC("SyncList", RpcTarget.All, _colorNumber);
    }

    [PunRPC]
    void EventHandlerStatus()
    {
        _playerNameDisplayText.text = _playerName;

    }

    [PunRPC]
    void SyncList(int _colorNumber)
    {
        Networked_RoomManager.Instance.NetworkedDataConfig[photonView.Controller.ActorNumber - 1].NetworkedPlayerSpriteColor = _colorNumber;

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//if the client who o\wns this variable is doing this action, the value of the variable is sent across the network
        {
            stream.SendNext(_playerName);
        }
        else
        {
            this._playerName = (string)stream.ReceiveNext();
        }
    }
}
