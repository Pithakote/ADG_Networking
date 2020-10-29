using Photon.Pun;
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
        
       
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
     //   if(GetComponent<Rigidbody2D>())
    
    }

    public override void InitializePlayer(PlayerDataConfiguration pc)
    {
        _playerConfig = pc;
        _playerRenderer.sprite = pc.PlayerShape;
        _playerRenderer.color = pc.PlayerSpriteColor;


        _playerConfig.Input.onActionTriggered += _playerInputHandler.Input_onActionTriggered;

    }




    protected override void InitializePlayer(int _playerColorNumber)
    {
        throw new System.NotImplementedException();
    }

    public override void ReduceHealth()
    {
        if (PlayerHealth > 0)
        {
            HealthReduced = true;
            PlayerHealth -= PlayerTakeDamageAmount;


            Debug.Log("Health decreasing");


        }
        else
            gameObject.SetActive(false);

    }
}
