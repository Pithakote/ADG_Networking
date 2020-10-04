using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class PlayerController : MonoBehaviour
{
    PlayerMovement _playerMovement;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        PlayerMovement[] _playerMovementIntances = FindObjectsOfType<PlayerMovement>();
        int _index = _playerInput.playerIndex; //automatically gets the input from playerInput

        for (int i = 0; i < _playerMovementIntances.Length; i++)
          _playerMovement =  _playerMovementIntances.FirstOrDefault(pm => pm.PlayerIndex == _index);
            //if the index in the player is the same as the index given by the _index(playerInput.index)
    }
  

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (_playerMovement == null)
            return;

        _playerMovement.MovementInput = ctx.ReadValue<Vector2>();
    }
}
