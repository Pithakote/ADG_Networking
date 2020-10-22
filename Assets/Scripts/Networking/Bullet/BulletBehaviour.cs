using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    float _moveSpeed = 30f;
    float lifefTime;
    float maxLifeTime = 2f;


    private void OnEnable()
    {
        lifefTime = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _moveSpeed * Time.deltaTime);
        lifefTime += Time.deltaTime;
        if (lifefTime > maxLifeTime)
        {
            Destroy(gameObject);
        }
    }
}
