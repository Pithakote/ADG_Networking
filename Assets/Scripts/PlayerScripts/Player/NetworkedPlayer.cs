using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class NetworkedPlayer : PlayerBase, IPunObservable
{
   
    [SerializeField] bool _isActivated = false;
 
    [SerializeField] PlayerColorAndShape _playerShapesAndColor;
    [SerializeField] TMP_Text _playerInfo;

    private ExitGames.Client.Photon.Hashtable _myCustomProperty = new ExitGames.Client.Photon.Hashtable();
    Player _player;

  
    
     string name;

    NetworkedPlayerDataConfiguration _npc;
    
   [SerializeField] float offset;

    protected override void Awake()
    {
        PhotonNetwork.SendRate = 40;
        PhotonNetwork.SerializationRate = 15;
        base.Awake();
        transform.parent = GameObject.Find("Newtowked_GameManager").transform;
        //_playerShooting = GetComponent<NetworkedPlayerShooting>();


        if (photonView.IsMine )
        {
            PlayerBase.LocalPlayerInstance = this.gameObject;
        }

        if (photonView.IsMine && PlayerInput == null)
        {
            PlayerInput = GetComponent<PlayerInput>();

        }
        else if (!photonView.IsMine && GetComponent<PlayerInput>())
            GetComponent<PlayerInput>().enabled = false;
        //   _playerMovement = GetComponent<PlayerMovement>();

        //  InitialisePlayer(PhotonNetwork.LocalPlayer);
    }
  
    

    protected override void Start()
    {
        base.Start();
        _playerInfo.text = name + " Health Amount: " + PlayerHealth.ToString();



    }

    protected override void Update()
    {
        _playerInfo.text = name + " Health Amount: " + PlayerHealth.ToString();


    }



    //PhotonNetwork function for handling when PlayerLeaves the room
    //need to inherit from MonoBehaviourPunCallbacks to use this
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer == PhotonNetwork.LocalPlayer && photonView.IsMine)
        {
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
            _myCustomProperty.Remove("PlayerIndexNumber");
        }
    }

    //NetworkedPlayerDataConfiguration is a class responsible for handling Networked Player data
    //after selecting the color
    public void InitialisePlayer(NetworkedPlayerDataConfiguration npc)
    {
        if (photonView.IsMine)
        {
           
            _npc = npc;
            InitializePlayer(Networked_PlayerManager.Instance.ColorNumber);

        }
    }
    protected override void InitializePlayer(int _playerColorNumber)
    {

        if (photonView.IsMine ||
           !PhotonNetwork.IsConnected ||
           PhotonNetwork.LocalPlayer.IsLocal
           )
        {
            //subscribing the Input actions from the _PlayerInputHandler to the PlayerInput component
            PlayerInput.onActionTriggered += _playerInputHandler.Input_onActionTriggered;
        }

        //------------this section is required to assign the correct shape and color------------
        //------------in offline mode, a player manager was used to do so, tried to do the same for networked play using Custom-------
        //------------properties but ended up using serializable object to self contain the player data -------------------
        #region assign the correct shape and color
        int _playerNumber ;       
        _playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        int shapeID = _playerNumber - 1;

        //RPC is used to notify the change of one player to all other clients currently connected
        //so SetupCharacter is a rpc function that is being called here to notify everyone of the shape and color change
        photonView.RPC("SetupCharacter", RpcTarget .All, shapeID, _playerColorNumber);
        #endregion
        //-----------------------------------------------------------------------

    }

    public override void ReduceHealth(int PlayerTakeDamageAmount)
    {
        base.ReduceHealth(PlayerTakeDamageAmount);

        //RPC is used to notify the change of one player to all other clients currently connected
        //so HealthDeduction is a RPC function that is being called here to notify everyone
        //that this client is taking damage

        photonView.RPC("HealthDeduction", RpcTarget.All, PlayerTakeDamageAmount);

    }

    //RPC is used to notify the change of one player to all other clients currently connected
    //functions are marked as a RPC function by using [PunRPC] in Photon
    [PunRPC]
    void SetupCharacter(int shapeID, int _playerColorNumber)
    {
        name = PhotonNetwork.LocalPlayer.NickName;
        _playerRenderer.sprite = _playerShapesAndColor._playerShape[shapeID];
        _playerRenderer.color = _playerShapesAndColor._playerColor[_playerColorNumber];


    }
    [PunRPC]
    void HealthDeduction(int PlayerTakeDamageAmount)
    {
        //if the client is alive or has health available
        if (PlayerHealth > 0)
        {
            HealthReduced = true;
            PlayerHealth -= PlayerTakeDamageAmount;
            _healthBar.fillAmount = (PlayerHealth / PlayerMaxHealth);


            Debug.Log("Health decreasing");


        }
        //if the client is dead
        else
        {
            //-----------To make sure nothing will be there to affect other clients once
            //-----------this client is "dead"
            _isPlayerAlive = false;
            _healthBar.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<PlayerMovement>()._isPlayerAlive = _isPlayerAlive;
            GetComponent<PlayerShooting>().enabled = _isPlayerAlive;
            GetComponent<Rigidbody2D>().isKinematic = true;
            
            _playerRenderer.sprite = _deadIcon;

            if (Networked_GameManager.Instance)
            {
                Networked_GameManager.Instance.NumberOfDeadPlayers++;
                if (photonView.IsMine)
                    Networked_GameManager.Instance.IsLocalClientDead = !_isPlayerAlive;
            }
            //----------------------------------------------------------------
        }
    }



    //-------------photon PUN2 uses this function to observe variables using PhotonView.
    //-------------this helps to synchronise the observed variables in all instances
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//if the client who owns this variable is doing this action, the value of the variable is sent across the network
        {
            stream.SendNext(_playerInputHandler.rb.rotation);
            stream.SendNext(_isActivated);
            stream.SendNext(name);
            stream.SendNext(_isPlayerAlive);
            stream.SendNext(PlayerHealth);
        }
        else if (stream.IsReading)//else be ready to receive the action
        {
            this.networkedRotation = (float)stream.ReceiveNext();
            this._isActivated = (bool)stream.ReceiveNext();
            this.name = (string)stream.ReceiveNext();
            this._isPlayerAlive = (bool)stream.ReceiveNext();
            this.PlayerHealth = (float)stream.ReceiveNext();
          
        }
    }

    //-------------------------------------------------------------------------------------------



    public override void InitializePlayer(PlayerDataConfiguration pc)
    {
        throw new System.NotImplementedException();
    }
}
