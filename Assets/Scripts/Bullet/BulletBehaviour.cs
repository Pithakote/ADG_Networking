using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviourPun
{
    float _moveSpeed = 30f;
    float lifefTime;
    float maxLifeTime = 2f;
    int PlayerTakeDamageAmount = 2;
    private void OnEnable()
    {
        lifefTime = 0f;
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
       //moves the bullet in the up direction
        transform.Translate(Vector2.up * _moveSpeed * Time.deltaTime);
        
        //lifetime changes according to how much time has passed 
        lifefTime += Time.deltaTime;
        
        //destroy the bullet if it doesn't hit anything after maxLifeTime
        if (lifefTime > maxLifeTime)
        {


            //RPC calls are made so that all other clients sync what has happened in this client.
            //in this case, Destroy is the RPC function as we want other clients to know that the bullet
            //has been destroyed
            
            
            photonView.RPC("Destroy", RpcTarget.All, null);

        }
    }

   


    //  [PunRPC]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //the bullet checks if the object it collided with has implemented
        //ITakeDamage interface
        ITakeDamage _damageTakingObject = collision.gameObject.GetComponent<ITakeDamage>() as ITakeDamage;


        //if the object has implemented ITakeDamage, 
        //then it stored it in the variable  _damageTakingObject and calls the ReduceHealth function

        if (PhotonNetwork.OfflineMode == false)//for online modes
        {
            if (_damageTakingObject != null && photonView.IsMine)//null check for _damageTakingObject and checks if is local player 
            {
                _damageTakingObject.ReduceHealth(PlayerTakeDamageAmount);

                //RPC calls are made so that all other clients sync what has happened in this client.
                //in this case, Destroy is the RPC function as we want other clients to know that the bullet
                //has been destroyed
                //this rpc method is being called here because when the bullet hits another player,
                //it needs to be destroyed and needs to be synced
                photonView.RPC("Destroy", RpcTarget.All, null);

            }
        }
        else//for offline modes
        {
            if (_damageTakingObject != null )
            {
                _damageTakingObject.ReduceHealth(PlayerTakeDamageAmount);

              
                Destroy(this.gameObject);

            }
        }
    }
    //RPC function to be used with RPC calls
    [PunRPC]
    void Destroy()
    {
        Destroy(this.gameObject);
    }
    //originally made for getting the bulletback to pool but we're not using it
    void GoToPool()
    {
        ObjectPool.Instance.ReturnToPool(this.gameObject);

    }
}
