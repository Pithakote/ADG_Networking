using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;

public class Networked_ColorSelector : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] TMP_Text _playerNameDisplayText;
    [SerializeField] Button[] _buttonsInPanel;
    [SerializeField] Networked_ColorSelector[] _playerInputInSelectionPanel;
    [SerializeField] GameObject _localEventSystem;
    public static GameObject LocalSelectorInstance;
    [SerializeField] string _playerName;
    List<NetworkedPlayerDataConfiguration> _networkedPlayerDataConfig;
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
        ////  _playerInputInSelectionPanel = transform.parent.GetComponentsInChildren<Networked_ColorSelector>();
        //if (photonView.IsMine)// || PhotonNetwork.LocalPlayer.IsLocal)// || _playerId == PhotonNetwork.LocalPlayer.ActorNumber)
        //{
        //    _localEventSystem.SetActive(true);
        //}
        //else
        //{
        //    _localEventSystem.SetActive(false);
        //}

    }

    public void NetworkedSetColor(int _colorNumber)
    {
        Networked_RoomManager.Instance.NetworkedDataConfig[PhotonNetwork.LocalPlayer.ActorNumber - 1].NetworkedPlayerSpriteColor = _colorNumber;
    }

    [PunRPC]
    void EventHandlerStatus()
    {
        _playerNameDisplayText.text = _playerName;

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//if the client who owns this variable is doing this action, the value of the variable is sent across the network
        {
            stream.SendNext(_playerName);
        }
        else
        {
            this._playerName = (string)stream.ReceiveNext();
        }
    }
}
