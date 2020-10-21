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
    public SpriteRenderer SpriteRendererComponent { get; set; }
    [SerializeField] bool _isActivated = false;

    public PlayerInput PlayerInput { get; set; }
    private void Awake()
    {
        if (photonView.IsMine)
        { 
           Networked_PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
         _controls = new PlayerControls();

        SpriteRendererComponent = gameObject.GetComponent<SpriteRenderer>();
        if (PlayerInput == null)
            PlayerInput = GetComponent<PlayerInput>();
    }

    public void InitialisePlayer(Sprite _networkPlayerShape)
    {
        SpriteRendererComponent.sprite = _networkPlayerShape;
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {

            PlayerInput.onActionTriggered += Input_onActionTriggered;
        }

        if (_isActivated)
            SpriteRendererComponent.color = Color.red;
        else
            SpriteRendererComponent.color = Color.gray;
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
         //   stream.SendNext(_spriteRendererComponent.color);
        }
        else if (stream.IsReading)//else be ready to receive the action
        {
            this._isActivated = (bool)stream.ReceiveNext();
          //  _spriteRendererComponent = (SpriteRenderer)stream.ReceiveNext();
        }
    }
}
