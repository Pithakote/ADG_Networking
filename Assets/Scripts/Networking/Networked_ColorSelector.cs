using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;

public class Networked_ColorSelector : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _playerNameDisplayText;
    [SerializeField] Button[] _buttonsInPanel;
    [SerializeField] Networked_ColorSelector[] _playerInputInSelectionPanel;
    [SerializeField] GameObject _localEventSystem;

   List<NetworkedPlayerDataConfiguration> _networkedPlayerDataConfig;
    private void Awake()
    {
        
        //foreach (PlayerInput _inputComponent in _playerInputInSelectionPanel)
        //{

        //GetComponent<PlayerInput>().enabled = false;
        // }

       // photonView.RPC("EventHandlerStatus", RpcTarget.All, null);
    }

    public GameObject GetEventSystem()
    {
        return _localEventSystem;
    }
    public void SetupNetworkedPanel(string _playerName, int _playerId)
    {
        _playerNameDisplayText.text = _playerName;


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
        Networked_RoomManager.Instance.NetworkedDataConfig[PhotonNetwork.LocalPlayer.ActorNumber-1].NetworkedPlayerSpriteColor = _colorNumber;
    }

    [PunRPC]
    void EventHandlerStatus()
    {
       
    }
}
