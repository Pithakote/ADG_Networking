﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;


public class OfflinePlayer : PlayerBase
{
   
    private PlayerDataConfiguration _playerConfig;

    
   
   // PlayerControls _controls; 
  
    protected override void Awake()
    {
        
        PhotonNetwork.OfflineMode = true;

        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
    }

    public override void InitializePlayer(PlayerDataConfiguration pc)
    {
        _playerConfig = pc;
        _playerRenderer.sprite = pc.PlayerShape;
        _playerRenderer.color = pc.PlayerSpriteColor;

        _playerConfig.Input.onActionTriggered += _playerInputHandler.Input_onActionTriggered;
     
       
    }

    
    

    protected override void InitializePlayer()
    {
        throw new System.NotImplementedException();
    }
}