using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class Networked_PlayerManager : PlayerBase, IPunObservable
{
    [SerializeField] bool _isActivated = false;
 
    [SerializeField] PlayerColorAndShape _playerShapesAndColor;
   // [SerializeField] int _playerNumber = 3;
    [SerializeField] TMP_Text _playerInfo;

    private ExitGames.Client.Photon.Hashtable _myCustomProperty = new ExitGames.Client.Photon.Hashtable();
    Player _player;

  
    
     string name;

    
   [SerializeField] float offset;


    protected override void Awake()
    {
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

        if (photonView.IsMine)
        {
            InitializePlayer();
        }
    }
  

  

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer == PhotonNetwork.LocalPlayer && photonView.IsMine)
        {
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
            _myCustomProperty.Remove("PlayerIndexNumber");
        }
    }
    
    protected override void InitializePlayer()
    {

        if (photonView.IsMine ||
           !PhotonNetwork.IsConnected ||
           PhotonNetwork.LocalPlayer.IsLocal
           )
        {

            PlayerInput.onActionTriggered += _playerInputHandler.Input_onActionTriggered;
        }
        int _playerNumber ;       
        _playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        int shapeID = _playerNumber - 1;

      //  PlayerInput.onActionTriggered += base.Input_onActionTriggered;

        photonView.RPC("SetupCharacter", RpcTarget.All, shapeID);
        
   

       
        
        

    }
    // Update is called once per frame
    void Update()
    {

       _playerInfo.text = "Name is: " + name + " Health Amount: " + PlayerHealth.ToString();

    }

   
  
  
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//if the client who owns this variable is doing this action, the value of the variable is sent across the network
        {
            stream.SendNext(_playerInputHandler.rb.rotation);
            stream.SendNext(_isActivated);
            stream.SendNext(name);
        
        }
        else if (stream.IsReading)//else be ready to receive the action
        {
            this.networkedRotation = (float)stream.ReceiveNext();
            this._isActivated = (bool)stream.ReceiveNext();
            this.name = (string)stream.ReceiveNext();
          
        }
    }


    [PunRPC]
    void SetupCharacter(int shapeID)
    {
        name = PhotonNetwork.LocalPlayer.NickName;
        GetComponent<SpriteRenderer>().sprite = _playerShapesAndColor._playerShape[shapeID];
       

    }

    public override void InitializePlayer(PlayerDataConfiguration pc)
    {
        throw new System.NotImplementedException();
    }
}
