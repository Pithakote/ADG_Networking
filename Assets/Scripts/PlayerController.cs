using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
    public float PlayerHealth;
    public float PlayerMaxHealth;
    public int PlayerTakeDamageAmount;
    PlayerShooting _playerShootingComponent;



    [SerializeField] Image _healthBar;
    float _newHealthValue;
    private void Awake()
    {
        _playerRenderer = GetComponent<SpriteRenderer>();
         _playerMovement = GetComponent<PlayerMovement>();
        _playerShooting = GetComponent<PlayerShooting>();

        _playerShooting._spriteRendererComponent = _playerRenderer;
        _controls = new PlayerControls();

        _playerShootingComponent = GetComponent<PlayerShooting>();

    }

    void Start()
    {
        PlayerHealth = PlayerMaxHealth;

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
        if (obj.action.name == _controls.PlayerMovement.AimingMouse.name)
        {
            
            OnRotateMouse(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.AimingController.name)
        {
            
            OnRotateController(obj);
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
    public void OnRotateMouse(InputAction.CallbackContext ctx)
    {
        // _mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
      
      // if(ctx.control.device)
        _playerMovement._mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
       //else if(_controls.devices is Gamepad)
       
      //  _playerMovement._mousePos = ctx.ReadValue<Vector2>();
      // _playerMovement._mousePos = ctx.ReadValue<Axis>();

    } public void OnRotateController(InputAction.CallbackContext ctx)
    {
        // _mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
      
      // if(ctx.control.device)
       // _playerMovement._mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
       //else if(_controls.devices is Gamepad)
       
        _playerMovement._mousePos = ctx.ReadValue<Vector2>();
      // _playerMovement._mousePos = ctx.ReadValue<Axis>();

    }
    public void ReduceHealth()
    {
        if (PlayerHealth > 0)
        {
            PlayerHealth -= PlayerTakeDamageAmount;
            _healthBar.fillAmount = (PlayerHealth / PlayerMaxHealth);

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
