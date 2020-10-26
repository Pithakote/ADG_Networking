using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;


public abstract class PlayerBase : MonoBehaviourPunCallbacks, ITakeDamage
{

    [SerializeField] protected Image _healthBar;


    public static GameObject LocalPlayerInstance;


    protected PlayerMovement _playerMovement;
    protected PlayerShooting _playerShooting;
    protected PlayerControls _controls;
    protected Rigidbody2D rb;
    protected SpriteRenderer _playerRenderer;
    public PlayerInput PlayerInput { get; set; }


    protected float PlayerHealth;
    [SerializeField] protected float PlayerMaxHealth;
    protected int PlayerTakeDamageAmount;
    public float networkedRotation { get; set; }


    bool rightStickIsUsed;

    // Start is called before the first frame update

    protected virtual void Awake()
    {

        _playerRenderer = GetComponent<SpriteRenderer>();
        
        _playerMovement = GetComponent<PlayerMovement>();
        if (_playerMovement == null)
            gameObject.AddComponent<PlayerMovement>();
        _playerShooting = GetComponent<PlayerShooting>();
        rb = GetComponent<Rigidbody2D>();
        


       

        _controls = new PlayerControls();
    }
    protected virtual void Start()
    {
        if(PlayerMaxHealth == 0)
        PlayerMaxHealth = 50;
        // PlayerHealth = PlayerMaxHealth;

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected abstract void InitializePlayer();

    public abstract void InitializePlayer(PlayerDataConfiguration pc);
   
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

    protected void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == _controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.Fire.name)
        {
            //  OnChangeColor(obj);

            OnShoot(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.AimingMouse.name)
        {

            OnRotateMouse(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.FireWhileAimingMobile.name)
        {


            OnShootWithJoystick(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.AimingController.name)
        {

            OnRotateController(obj);


          



        }


    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _playerMovement.MovementInput = ctx.ReadValue<Vector2>();

    }
    public void OnShootWithJoystick(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();

        if (value != Vector2.zero)
            rightStickIsUsed = true;
        else
        {
            rightStickIsUsed = false;
        }
        _playerShooting._isActivated = rightStickIsUsed;

    }
    public void OnShoot(InputAction.CallbackContext ctx)
    {


        _playerShooting._isActivated = ctx.ReadValueAsButton();
    }

   

    public void OnRotateMouse(InputAction.CallbackContext ctx)
    {
        _playerMovement._mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue() -
                                                                   new Vector2(rb.position.x,
                                                                                rb.position.y));
      
    }
    public void OnRotateController(InputAction.CallbackContext ctx)
    {
        _playerMovement._mousePos = ctx.ReadValue<Vector2>();
    
    }
}
