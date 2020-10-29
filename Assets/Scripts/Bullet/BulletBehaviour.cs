using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviourPun
{
    float _moveSpeed = 30f;
    float lifefTime;
    float maxLifeTime = 2f;
  
    private void OnEnable()
    {
        lifefTime = 0f;
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _moveSpeed * Time.deltaTime);
        lifefTime += Time.deltaTime;
        if (lifefTime > maxLifeTime)
        {

            //  GoToPool();
            //   Destroy(gameObject);
            photonView.RPC("Destroy", RpcTarget.All, null);

        }
    }

   


    //  [PunRPC]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ITakeDamage _damageTakingObject = collision.gameObject.GetComponent<ITakeDamage>() as ITakeDamage;
        
        if (_damageTakingObject != null && photonView.IsMine)
        {
               //photonView.RPC()
               _damageTakingObject.ReduceHealth();

            //if (other.name != "Shield_Collider")
            //{
            //       other.SendMessage("TakeDamage", damage);
            //}

            //   Debug.Log("Shot");

            // GoToPool();
            photonView.RPC("Destroy", RpcTarget.All, null);
          
        }
    }
    [PunRPC]
    void Destroy()
    {
        Destroy(this.gameObject);
    }
    void GoToPool()
    {
       // _objectPool.ReturnToPool(this.gameObject);
        ObjectPool.Instance.ReturnToPool(this.gameObject);

      //  Destroy(gameObject);
    }
}
