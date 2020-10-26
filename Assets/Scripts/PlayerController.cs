using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
public class PlayerController : PlayerBase, ITakeDamage
{
   
    private PlayerDataConfiguration _playerConfig;

    
   
   // PlayerControls _controls; 
  
    private void Awake()
    {
        PhotonNetwork.OfflineMode = true;
        

     
    }

    
    public override void InitializePlayer(PlayerDataConfiguration pc)
    {
        _playerConfig = pc;
        GetComponent<SpriteRenderer>().sprite = pc.PlayerShape;
        GetComponent<SpriteRenderer>().color = pc.PlayerSpriteColor;

        _playerConfig.Input.onActionTriggered += base.Input_onActionTriggered;
     
       
    }

    
    

    protected override void InitializePlayer()
    {
        throw new System.NotImplementedException();
    }
}
