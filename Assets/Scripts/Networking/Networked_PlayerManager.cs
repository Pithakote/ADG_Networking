using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class Networked_PlayerManager : MonoBehaviourPunCallbacks, IPunObservable, ITakeDamage
{
    [SerializeField] float _speed = 0.03f;
    public static GameObject LocalPlayerInstance;
    public Vector2 MovementInput { get; set; }//property value set in PlayerController
    PlayerControls _controls;
    public SpriteRenderer SpriteRendererComponent { get; set; }
    [SerializeField] bool _isActivated = false;
    [SerializeField] Sprite [] test;
    [SerializeField] Image _healthBar;
    public PlayerInput PlayerInput { get; set; }

    [SerializeField] PlayerColorAndShape _playerShapesAndColor;
   // [SerializeField] int _playerNumber = 3;
    [SerializeField] TMP_Text _playerInfo;

    private ExitGames.Client.Photon.Hashtable _myCustomProperty = new ExitGames.Client.Photon.Hashtable();
    Player _player;

    NetworkedPlayerShooting _playerShootingComponent;

    public float PlayerHealth;
    public float PlayerMaxHealth;
    public int PlayerTakeDamageAmount;
    float _newHealthValue;
    string name;

    Vector3 _mousePos;
    Rigidbody2D rb;
    [SerializeField] float offset;

    bool isUsed;
    float networkedRotation, initialRotation;
    private void Awake()
    {
        if (photonView.IsMine)
        { 
           Networked_PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
         _controls = new PlayerControls();

        SpriteRendererComponent = gameObject.GetComponent<SpriteRenderer>();
        if (photonView.IsMine && PlayerInput == null)
            PlayerInput = GetComponent<PlayerInput>();
        else if (!photonView.IsMine && GetComponent<PlayerInput>())
            GetComponent<PlayerInput>().enabled = false;

        transform.parent = GameObject.Find("Newtowked_GameManager").transform;
        _playerShootingComponent = GetComponent<NetworkedPlayerShooting>();
        rb = GetComponent<Rigidbody2D>();
        //  InitialisePlayer(PhotonNetwork.LocalPlayer);
        //  SetSkin();

    }

    void Start()
    {
        if (photonView.IsMine)
        {
            InitialisePlayer();
        }
     //   PlayerHealth = PlayerMaxHealth;
       // _playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
       //  _playerNumber = _player.ActorNumber;
       //  if (photonView.IsMine)
       //  {

        //     base.photonView.RPC("ChangeSprite", RpcTarget.AllBuffered, null);
        //  }
    }
    public override void OnLeftRoom()
    {
     //   if (photonView.IsMine)
          //  PhotonNetwork.RemoveRPCs(photonView);
    }
    [PunRPC]
    void SetSkin()
    {

        int _playerNumber = 0;
        if (photonView.IsMine)
        {
            //_myCustomProperty["PlayerIndexNumber"] = PhotonNetwork.LocalPlayer.ActorNumber;
            //Debug.Log("GameManager actor number is " + PhotonNetwork.LocalPlayer.ActorNumber);
            //PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperty;
            
            _myCustomProperty.Add("PlayerIndexNumber",PhotonNetwork.LocalPlayer.ActorNumber);
            Debug.Log("GameManager actor number is " + PhotonNetwork.LocalPlayer.ActorNumber);
            PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperty);

            //SpriteRendererComponent.sprite = _playerShapesAndColor._playerShape[PhotonNetwork.LocalPlayer.ActorNumber];
            _playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            SpriteRendererComponent.sprite = _playerShapesAndColor._playerShape[_playerNumber];

          
        }
        else
        {
            _player = photonView.Owner;
            int otherPlayerindex = (int)photonView.Owner.CustomProperties["PlayerIndexNumber"];
            Debug.Log("Other player index is: "+otherPlayerindex);
            SpriteRendererComponent.sprite = _playerShapesAndColor._playerShape[otherPlayerindex];



        }

      //  Debug.Log("The actor number " + _playerNumber);
        //  SpriteRendererComponent.sprite =  (Sprite)thisPlayer.CustomProperties["PlayerShape"];
        _playerInfo.text = _playerNumber.ToString() + PhotonNetwork.LocalPlayer.NickName;
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer == PhotonNetwork.LocalPlayer && photonView.IsMine)
        {
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
            _myCustomProperty.Remove("PlayerIndexNumber");
        }
    }
    
    public void InitialisePlayer()
    {
        //_playerNumbefr = thisPlayer.ActorNumber;

        int _playerNumber ;

       // if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerIndexNumber"))
       //     _playerNumber = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerIndexNumber"];
      //  else
            _playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;


        int shapeID = _playerNumber - 1;
         
        Debug.Log("The actor number " + _playerNumber);
        //  SpriteRendererComponent.sprite =  (Sprite)thisPlayer.CustomProperties["PlayerShape"];
      //  _playerInfo.text = _playerNumber.ToString() + name;
        //SpriteRendererComponent.sprite = _networkPlayerShape;

        photonView.RPC("SetupCharacter", RpcTarget.All, shapeID);


        PlayerHealth = PlayerMaxHealth;
        //  ChangeSprite();

        if (photonView.IsMine || !PhotonNetwork.IsConnected || PhotonNetwork.LocalPlayer.IsLocal)
        {

            PlayerInput.onActionTriggered += Input_onActionTriggered;
        }
        initialRotation = rb.rotation;
        //   _myCustomProperty["PlayerShape"] = System.Convert.ToByte(_networkPlayerShape);
        //    SpriteRendererComponent.sprite = _myCustomProperty["PlayerShape"];

    }
    // Update is called once per frame
    void Update()
    {

       

        if (_isActivated)
        {
           // SpriteRendererComponent.sprite = test[0];
            SpriteRendererComponent.color = Color.red;
        }
        else
        {

         //   SpriteRendererComponent.sprite = test[1];
            SpriteRendererComponent.color = Color.gray;
        }

        _playerInfo.text = "Name is: " + name + " Health Amount: " + PlayerHealth.ToString();

    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == _controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.Fire.name )
         {
            //  OnChangeColor(obj);

            OnShoot(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.FireWhileAimingMobile.name)
        {
           
            
            OnShootWithJoystick(obj);
        }
       
        if (obj.action.name == _controls.PlayerMovement.AimingMouse.name)
        {

            OnRotateMouse(obj);
        }
        if (obj.action.name == _controls.PlayerMovement.AimingController.name)
        {

            OnRotateController(obj);

            
          //  Debug.Log("Vector2 value when shooting is: " + value);

            


        }
       

    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();

    }
    public void OnShootWithJoystick(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        
        if(value != Vector2.zero)
        isUsed = true;
        else
        {
            isUsed = false;
        }
        _playerShootingComponent._isActivated = isUsed;
       
    }
    public void OnShoot(InputAction.CallbackContext ctx)
    {
        

        _playerShootingComponent._isActivated = ctx.ReadValueAsButton();
    }

    public void OnChangeColor(InputAction.CallbackContext ctx)
    {
        _isActivated = ctx.ReadValueAsButton();
    }

    public void OnRotateMouse(InputAction.CallbackContext ctx)
    {
        // _mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

        // if(ctx.control.device)
        _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue() -
                                                                   new Vector2(rb.position.x,
                                                                                rb.position.y));
        //else if(_controls.devices is Gamepad)

        //  _playerMovement._mousePos = ctx.ReadValue<Vector2>();
        // _playerMovement._mousePos = ctx.ReadValue<Axis>();

    }
    public void OnRotateController(InputAction.CallbackContext ctx)
    {
        // _mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

        // if(ctx.control.device)
        // _playerMovement._mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //else if(_controls.devices is Gamepad)

        _mousePos = ctx.ReadValue<Vector2>();
        // _playerMovement._mousePos = ctx.ReadValue<Axis>();

    }
    private void FixedUpdate()
    {
        //transform.Translate(new Vector2(MovementInput.x, MovementInput.y)
        //                               * _speed
        //                              * Time.deltaTime);
        rb.MovePosition(rb.position + MovementInput * _speed * Time.deltaTime);


        MouseRotation();

        if (!photonView.IsMine)
        {
            rb.rotation = networkedRotation + Time.deltaTime * 100.0f;
        }

    }
    private void MouseRotation()
    {
        //var lookDir = _mousePos - transform.position;
        var lookDir = _mousePos;
        //  var lookDir = new Vector2(_mousePos.x, _mousePos.y) - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offset;
        //-offset don't need to calculate with offser because forward vector is the right i.e the read arrow 

       //  transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.rotation = angle;
        //  GetComponent<Transform>().Rotate(Vector3.back * _mousePos.x * speed);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//if the client who owns this variable is doing this action, the value of the variable is sent across the network
        {
            stream.SendNext(rb.rotation);
            stream.SendNext(_isActivated);
            stream.SendNext(name);
          //  stream.SendNext(_healthBar.fillAmount);
        //    stream.SendNext(_playerNumber);
         //   stream.SendNext(_spriteRendererComponent.color);
        }
        else if (stream.IsReading)//else be ready to receive the action
        {
            this.networkedRotation = (float)stream.ReceiveNext();
            this._isActivated = (bool)stream.ReceiveNext();
            this.name = (string)stream.ReceiveNext();
           // this._healthBar.fillAmount = (float)stream.ReceiveNext();
           // this._playerInfo
        //    this._playerNumber = (int)stream.ReceiveNext();
          //  _spriteRendererComponent = (SpriteRenderer)stream.ReceiveNext();
        }
    }

   
    [PunRPC]
    void SetupCharacter(int shapeID)
    {
        name = PhotonNetwork.LocalPlayer.NickName;
        // SpriteRendererComponent.sprite = _playerShapesAndColor._playerShape[_playerNumber-1];
        SpriteRendererComponent.sprite = _playerShapesAndColor._playerShape[shapeID];
      //  _playerInfo.text = "Name is: "+ name +" Health Amount: " + PlayerHealth.ToString();
        //SpriteRendererComponent.sprite = (Sprite)thisPlayer.CustomProperties["PlayerShape"];

    }

   // [PunRPC]
    public void ReduceHealth()
    {
        if (PlayerHealth > 0)
        {
            PlayerHealth -= PlayerTakeDamageAmount;
            //  _newHealthValue = (float)(PlayerHealth / PlayerMaxHealth);
            //   _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _newHealthValue, Time.deltaTime); 

            _healthBar.fillAmount = (PlayerHealth / PlayerMaxHealth);

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
