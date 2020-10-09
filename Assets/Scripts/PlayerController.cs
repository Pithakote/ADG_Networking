using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class PlayerController : MonoBehaviour
{
    PlayerMovement _playerMovement;

    private PlayerDataConfiguration _playerConfig;

    
    SpriteRenderer _playerRenderer;

    PlayerControls _controls;

    private void Awake()
    {
        _playerRenderer = GetComponent<SpriteRenderer>();
         _playerMovement = GetComponent<PlayerMovement>();
     

        _controls = new PlayerControls(); 
    }

    public void InitializePlayer(PlayerDataConfiguration pc)
    {
        _playerConfig = pc;
        _playerRenderer.sprite = pc.PlayerShape;
        _playerRenderer.color = pc.PlayerSpriteColor;

        _playerConfig.Input.onActionTriggered += Input_onActionTriggered;
       // BoxCollider2D _boxCollider = new BoxCollider2D();
        gameObject.AddComponent(typeof(PolygonCollider2D));
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
