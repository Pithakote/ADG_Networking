using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _text;
    [SerializeField] GameObject _colorSelectorLocal;
    GameObject _colorSelectorHolder;
    Player player;
    int _playerNumber;

    private void Start()
    {
      //  _colorSelectorHolder = GameObject.Find("ColorSelectorPanel");
    }
    public void SetUp(Player _player, GameObject _colorSelector)
    {
        player = _player;
        _text.text = _player.NickName;
        _playerNumber = _player.ActorNumber;

        _colorSelectorLocal = _colorSelector;
        if(PhotonNetwork.LocalPlayer.IsLocal)
        Debug.Log("The player index is: "+_playerNumber);
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
