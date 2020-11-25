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

        //Input controls asset is used to assign the action names
        //so an object of Type PlayerControls is required to read the action names
        _controls = new PlayerControls();
        _playerMovement = GetComponent<PlayerMovement>();
        if (_playerMovement == null)
            gameObject.AddComponent<PlayerMovement>();

        _playerShooting = GetComponent<PlayerShooting>();

        if(GetComponent<Rigidbody2D>())
        rb = GetComponent<Rigidbody2D>();

    }

    //this function is subsctibed to the PlayerInput's "onActionTriggered" event in the player classes.
    // that event detects the inputs and triggers this action 
    public void Input_onActionTriggered(CallbackContext obj)
    {
        //Input actions in the Input controls asset in the project has actions. This function
        //controls what code to run when an action is triggered by comparing the triggered actiom with the available actions
        //in the Input control component


        //compares the triggered input actions with the actions availabe in the input Control asset
        //and uses code assign to them

        //action for movement
        if (obj.action.name == _controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }

        //action for shooting
        if (obj.action.name == _controls.PlayerMovement.Fire.name)
        {
            

            OnShoot(obj);
        }

        //action for aiming and rotatiing with mouse
        if (obj.action.name == _controls.PlayerMovement.AimingMouse.name)
        {

            OnRotateMouse(obj);
        }

        //action for aiming and rotatiing with mobile joystick
        if (obj.action.name == _controls.PlayerMovement.FireWhileAimingMobile.name)
        {


            OnShootWithJoystick(obj);
        }

        //action for aiming and rotatiing with right stick in a gamepad
        if (obj.action.name == _controls.PlayerMovement.AimingController.name)
        {

            OnRotateController(obj);
        }


    }

    public void OnMove(InputAction.CallbackContext ctx)
    {

        //reads the vector direction in 2D and sends the value to be used in PlayerMovement class
        _playerMovement.MovementInput = ctx.ReadValue<Vector2>();

    }
    public void OnShootWithJoystick(InputAction.CallbackContext ctx)
    {
        //function to shoot and rotate at the same time

        //
        //reads the direction in 2D and sends the value to be used in PlayerMovement class
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
        //ctx.readvaueasbutton returns true of the button is pressed. wheter the button
        //should be held or tapped is set in teh input controls asset
        //depending on the value, the bool of PlayerShooting class is triggered

        _playerShooting._isActivated = ctx.ReadValueAsButton();
    }



    public void OnRotateMouse(InputAction.CallbackContext ctx)
    {
        //we dont want this code to work in the main menu 
        //just a cheker
        if (SceneManager.GetActiveScene().name == "MainMenu")
            return;

        //returned the position of the mouse  
        //by performing vector substraction to find the desired direction which is
        //sent to the PlayerMovmeent class.
        _playerMovement._mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue() -
                                                                   new Vector2(rb.position.x,
                                                                                rb.position.y));

    }
    public void OnRotateController(InputAction.CallbackContext ctx)
    {
        //if a controller is being used, it just reads the vector direction 
        //and sends to PlayerMovment class to find the desired direction
        _playerMovement._mousePos = ctx.ReadValue<Vector2>();

    }
}
