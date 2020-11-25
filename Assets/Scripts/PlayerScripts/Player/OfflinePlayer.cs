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
    //PlayerDataConfiguration is a custom class made for handling Player data for Offline mode

    private PlayerDataConfiguration _playerConfig;







    //this function is called when the offline local coop level is loaded after selecting the player data

    public override void InitializePlayer(PlayerDataConfiguration pc)
    {
        _playerConfig = pc;
        _playerRenderer.sprite = pc.PlayerShape;
        _playerRenderer.color = pc.PlayerSpriteColor;

        //subscribing the Input actions from the _PlayerInputHandler to the PlayerInput component
        //PlayerInput component is required to use Unity's Input System

        _playerConfig.Input.onActionTriggered += _playerInputHandler.Input_onActionTriggered;

    }



    //not implemented as it was made in the base class for Online player

    protected override void InitializePlayer(int _playerColorNumber)
    {
        throw new System.NotImplementedException();
    }
    //inherited form base class and added the offline functionality
    public override void ReduceHealth(int PlayerTakeDamageAmount)
    {
        base.ReduceHealth(PlayerTakeDamageAmount);
        if (PlayerHealth > 0)
        {
            HealthReduced = true;
            PlayerHealth -= PlayerTakeDamageAmount;


            Debug.Log("Health decreasing");


        }
        else
            gameObject.SetActive(false);

    }

    protected override void Update()
    {
        throw new System.NotImplementedException();
    }
}
