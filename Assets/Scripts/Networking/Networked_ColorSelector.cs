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
using ExitGames.Client.Photon;

public class Networked_ColorSelector : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _playerNameDisplayText;
    [SerializeField] Button[] _buttonsInPanel;
  
    [SerializeField] Networked_ColorSelector[] _playerInputInSelectionPanel;
    [SerializeField] GameObject _localEventSystem, _selectionMenu, _readyMenu, _readyButton;
    public static GameObject LocalSelectorInstance;
    [SerializeField] string _playerName;
    List<NetworkedPlayerDataConfiguration> _networkedPlayerDataConfig;
    [SerializeField] int _localPlayerNumber, _ownerPlayerNumber, _getPlayerNumber;

    
    private void Awake()
    {
        transform.SetParent(GameObject.Find("ColorSelectorPanel").transform);
      //  transform.position = Vector3.zero;
        Networked_PlayerManager.Instance.NetworkedDataConfig.Add(new NetworkedPlayerDataConfiguration(PhotonNetwork.LocalPlayer));
        _playerName = photonView.Owner.NickName;
        _playerNameDisplayText.text = _playerName;

        _buttonsInPanel = GetComponentsInChildren<Button>();

        if (photonView.IsMine)
        {
            LocalSelectorInstance = this.gameObject;
        
        }
        else
        {
            foreach (Button _button in _buttonsInPanel)
                _button.interactable = false;

            _localEventSystem.SetActive(false);
        }

        _localPlayerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        _ownerPlayerNumber = photonView.Owner.ActorNumber;
        _getPlayerNumber = photonView.Owner.GetPlayerNumber();
    
        photonView.RPC("InitialFunctionsToBeShared", RpcTarget.All, _playerName);
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
         Networked_PlayerManager.Instance.ColorNumber = _colorNumber;
        _readyMenu.SetActive(true);
        _selectionMenu.SetActive(false);
    }


    public void IsReadyButton()
    {
        photonView.RPC("IsReady", RpcTarget.All, null) ;
       
    }
    [PunRPC]
    void EventHandlerStatus()
    {
        _playerNameDisplayText.text = _playerName;

    }

   [PunRPC]
     void IsReady()
    {
          Networked_PlayerManager.Instance._isReady = true;
        _readyButton.SetActive(false);
        
    } 

}
