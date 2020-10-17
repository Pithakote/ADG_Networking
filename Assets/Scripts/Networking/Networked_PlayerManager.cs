using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Networked_PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] float _speed = 0.03f;
    public static GameObject LocalPlayerInstance;
    public Vector2 MovementInput { get; set; }//property value set in PlayerController
    PlayerControls _controls;
    private void Awake()
    {
        if (photonView.IsMine)
        { 
           Networked_PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
         _controls = new PlayerControls(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        this.gameObject.GetComponent<PlayerInput>().onActionTriggered += Input_onActionTriggered;

        //this.gameObject.transform.position = new Vector3( 
        //                                                this.gameObject.transform.position.x + Input.GetAxis("Horizontal")*_speed,
        //                                                this.gameObject.transform.position.y + Input.GetAxis("Vertical")*_speed,
        //                                                this.gameObject.transform.position.z
        //    );
      // transform.position = new Vector2(MovementInput.x * _speed * Time.deltaTime, MovementInput.y * _speed * Time.deltaTime);

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
        MovementInput = ctx.ReadValue<Vector2>();

    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector2(MovementInput.x, MovementInput.y)
                                       * _speed
                                      * Time.deltaTime);
      
    }
}
