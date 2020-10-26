using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerControls _controls;
     PlayerMovement _playerMovement;
    bool rightStickIsUsed;
     PlayerShooting _playerShooting;

    public Rigidbody2D rb;
    private void Awake()
    {
        _controls = new PlayerControls();
        _playerMovement = GetComponent<PlayerMovement>();
        if (_playerMovement == null)
            gameObject.AddComponent<PlayerMovement>();
        _playerShooting = GetComponent<PlayerShooting>();

        if(GetComponent<Rigidbody2D>())
        rb = GetComponent<Rigidbody2D>();

    }
    public void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == _controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.Fire.name)
        {
            

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
        if (SceneManager.GetActiveScene().name == "PlayerSetup")
            return;

        _playerMovement._mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue() -
                                                                   new Vector2(rb.position.x,
                                                                                rb.position.y));

    }
    public void OnRotateController(InputAction.CallbackContext ctx)
    {
        _playerMovement._mousePos = ctx.ReadValue<Vector2>();

    }
}
