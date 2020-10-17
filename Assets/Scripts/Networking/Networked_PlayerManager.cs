using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Networked_PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] float _speed = 0.03f;
    public static GameObject LocalPlayerInstance;
    public Vector2 MovementInput { get; set; }//property value set in PlayerController
    PlayerControls _controls;

    bool _isActivated = false;
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
        

        if (_isActivated)
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        else
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == _controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.Fire.name)
        {
            OnChangeColor(obj);
        }
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();

    }

    public void OnChangeColor(InputAction.CallbackContext ctx)
    {
        _isActivated = ctx.ReadValueAsButton();
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector2(MovementInput.x, MovementInput.y)
                                       * _speed
                                      * Time.deltaTime);
      
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//if the client who owns this variable is doing this action, the value of the variable is sent across the network
        {
            stream.SendNext(_isActivated);
        }
        else//else be ready to receive the action
            this._isActivated = (bool)stream.ReceiveNext();
    }
}
