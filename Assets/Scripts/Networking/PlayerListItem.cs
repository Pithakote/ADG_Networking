﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _text;
    [SerializeField] GameObject _colorSelectorLocal, _colorSelector;
    GameObject _colorSelectorHolder;
    Player player;
    int _playerNumber;
    Color _playerColor;
    private void Start()
    {
      //  _colorSelectorHolder = GameObject.Find("ColorSelectorPanel");
    }
    public void SetUp(Player _player)
    {
        player = _player;
        _text.text = _player.NickName;
        _playerNumber = _player.ActorNumber;


       // GameObject _selector = PhotonNetwork.Instantiate(this._colorSelector.name,
         //                                               Vector2.zero,
           //                                                 Quaternion.identity
             //                                           );

        //_colorSelectorLocal = _colorSelector;
        //_colorSelectorLocal.GetComponent<Networked_ColorSelector>().SetupNetworkedPanel(_player.NickName, _playerNumber);
        if (PhotonNetwork.LocalPlayer.IsLocal)
            Debug.Log("The player index is: " + _playerNumber);
        //else
          //  _colorSelectorLocal.GetComponent<Networked_ColorSelector>().GetEventSystem().SetActive(false);
    }

    public void SetupColor(Color selectedColor)
    {
        _playerColor = selectedColor;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(_colorSelectorLocal);
            Destroy(gameObject);
            
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(_colorSelectorLocal);
        Destroy(gameObject);

        
    }
}
