using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
public class PlayerController : MonoBehaviour, ITakeDamage
{
    PlayerMovement _playerMovement;
    PlayerShooting _playerShooting;
    private PlayerDataConfiguration _playerConfig;

    
    SpriteRenderer _playerRenderer;

    PlayerControls _controls;
    public int PlayerHealth = 10;
    public int PlayerTakeDamageAmount = 2;
    PlayerShooting _playerShootingComponent;

    private void Awake()
    {
        _playerRenderer = GetComponent<SpriteRenderer>();
         _playerMovement = GetComponent<PlayerMovement>();
        _playerShooting = GetComponent<PlayerShooting>();

        _playerShooting._spriteRendererComponent = _playerRenderer;
        _controls = new PlayerControls();

        _playerShootingComponent = GetComponent<PlayerShooting>();

    }

    public void InitializePlayer(PlayerDataConfiguration pc)
    {
        _playerConfig = pc;
        _playerRenderer.sprite = pc.PlayerShape;
        _playerRenderer.color = pc.PlayerSpriteColor;

        _playerConfig.Input.onActionTriggered += Input_onActionTriggered;
       // BoxCollider2D _boxCollider = new BoxCollider2D();
       
        //gameObject.AddComponent(typeof(PolygonCollider2D));

       
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == _controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.Fire.name)
        {
            OnShoot(obj);
          //  OnChangeColor(obj);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (_playerMovement == null)
            return;

        _playerMovement.MovementInput = ctx.ReadValue<Vector2>();

        //expand on this and make the gameobject rotate towards to input axis
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        _playerShootingComponent._isActivated = ctx.ReadValueAsButton();
    }
    public void OnChangeColor(InputAction.CallbackContext ctx)
    {
        _playerShooting._isActivated = ctx.ReadValueAsButton();
    }

    public void ReduceHealth()
    {
        PlayerHealth -= PlayerTakeDamageAmount;
    }
}
