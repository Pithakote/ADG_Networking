using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _text;
    Player player;
    int _playerNumber;
    public void SetUp(Player _player)
    {
        player = _player;
        _text.text = _player.NickName;
        _playerNumber = _player.ActorNumber;


        if(PhotonNetwork.LocalPlayer.IsLocal)
        Debug.Log("The player index is: "+_playerNumber);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
            Destroy(gameObject);
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
