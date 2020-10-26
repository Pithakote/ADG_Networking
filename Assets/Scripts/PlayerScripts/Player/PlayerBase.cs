using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
public abstract class PlayerBase : MonoBehaviourPunCallbacks, ITakeDamage
{

    [SerializeField] protected Image _healthBar;


    public static GameObject LocalPlayerInstance;


    
    
    
    
    protected SpriteRenderer _playerRenderer;
    public PlayerInput PlayerInput { get; set; }


    [SerializeField] protected float PlayerHealth;
    [SerializeField] protected float PlayerMaxHealth;
    protected int PlayerTakeDamageAmount;
    public float networkedRotation { get; set; }

    protected PlayerInputHandler _playerInputHandler;
    

    // Start is called before the first frame update

    protected virtual void Awake()
    {

        _playerRenderer = GetComponent<SpriteRenderer>();

       


        _playerInputHandler = GetComponent<PlayerInputHandler>();



    }
    protected virtual void Start()
    {
        PlayerTakeDamageAmount = 2;
        if (PlayerMaxHealth == 0)
        PlayerMaxHealth = 50;
        // PlayerHealth = PlayerMaxHealth;

        PlayerHealth = PlayerMaxHealth;
    }

   


    protected abstract void InitializePlayer();

    public abstract void InitializePlayer(PlayerDataConfiguration pc);
   
    public virtual void ReduceHealth()
    {
        if (PlayerHealth > 0)
        {
            PlayerHealth -= PlayerTakeDamageAmount;
          
            _healthBar.fillAmount = (PlayerHealth / PlayerMaxHealth);

            Debug.Log("Health decreasing");

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

   
}
