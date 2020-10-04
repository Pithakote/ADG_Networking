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
       // _playerInput = GetComponent<PlayerInput>();
         _playerMovement = GetComponent<PlayerMovement>();
        //  int _index = _playerInput.playerIndex; //automatically gets the input from playerInput

        //    for (int i = 0; i < _playerMovementIntances.Length; i++)
        //     _playerMovement =  _playerMovementIntances.FirstOrDefault(pm => pm.PlayerIndex == _index);
        //if the index in the player is the same as the index given by the _index(playerInput.index)

        _controls = new PlayerControls(); 
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        _playerConfig = pc;
       // _playerColor.color = pc.PlayerSpriteColor.color;
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
    }
}
