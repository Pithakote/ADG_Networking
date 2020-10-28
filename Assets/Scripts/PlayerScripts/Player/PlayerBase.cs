using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(DontGoOffScreen))]
public abstract class PlayerBase : MonoBehaviourPunCallbacks, ITakeDamage
{

    [SerializeField] protected Image _healthBar;
    [SerializeField] protected Sprite _deadIcon;

    public static GameObject LocalPlayerInstance;


    
    public bool HealthReduced { get; set; }
    
    
    protected SpriteRenderer _playerRenderer;
    public PlayerInput PlayerInput { get; set; }


    [SerializeField] protected float PlayerHealth;
    [SerializeField] protected float PlayerMaxHealth;
    protected int PlayerTakeDamageAmount;
    public float networkedRotation { get; set; }

    protected PlayerInputHandler _playerInputHandler;
    


    // Start is called before the first frame update
    protected abstract void InitializePlayer(int _playerColorNumber);

    public abstract void InitializePlayer(PlayerDataConfiguration pc);

    public bool _isPlayerAlive;

    protected virtual void Awake()
    {

        _playerRenderer = GetComponent<SpriteRenderer>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();



    }
    protected virtual void Start()
    {
        _isPlayerAlive = true;
        HealthReduced = false;
        PlayerTakeDamageAmount = 2;
        if (PlayerMaxHealth == 0)
        PlayerMaxHealth = 50;
        // PlayerHealth = PlayerMaxHealth;

        PlayerHealth = PlayerMaxHealth;
    }

   


  
    public virtual void ReduceHealth()
    {
        if (PlayerHealth > 0)
        {
            HealthReduced = true;
            PlayerHealth -= PlayerTakeDamageAmount;
          
            _healthBar.fillAmount = (PlayerHealth / PlayerMaxHealth);

            Debug.Log("Health decreasing");
            

        }
        else
        {
            _isPlayerAlive = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<PlayerMovement>()._isPlayerAlive = _isPlayerAlive;
            GetComponent<PlayerShooting>().enabled = _isPlayerAlive;
            _playerRenderer.sprite = _deadIcon;

            if (Networked_GameManager.Instance )
            {
                Networked_GameManager.Instance.NumberOfDeadPlayers++;
                if(photonView.IsMine)
                Networked_GameManager.Instance.IsLocalClientDead = !_isPlayerAlive;
            }
            //gameObject.SetActive(false);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer == PhotonNetwork.LocalPlayer && photonView.IsMine && PhotonNetwork.OfflineMode == false)
        {
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
         //   _myCustomProperty.Remove("PlayerIndexNumber");
        }
    }
}
