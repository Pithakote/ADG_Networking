using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class PlayerController : MonoBehaviour
{
    PlayerMovement _playerMovement;

    private PlayerConfiguration _playerConfig;

    [SerializeField]
    SpriteRenderer _playerColor;

    PlayerControls _controls;

    private void Awake()
    {
       
         _playerMovement = GetComponent<PlayerMovement>();
     

        _controls = new PlayerControls(); 
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        _playerConfig = pc;
        _playerColor.color = pc.PlayerSpriteColor;
        _playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == _controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (_playerMovement == null)
            return;

        _playerMovement.MovementInput = ctx.ReadValue<Vector2>();

        //expand on this and make the gameobject rotate towards to input axis
    }
}
