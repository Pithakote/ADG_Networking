using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float speed = 5;
    int turningSpeed = 5000;
    //   [SerializeField]
    //  int _playerIndex = 0;
    public Vector2 MovementInput { get; set; }//property value set in PlayerController
                                              //   public int PlayerIndex { get { return _playerIndex; } }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(MovementInput.x, MovementInput.y)
                                        * speed
                                        * Time.deltaTime);
      //  RotateGameObject(toVector2(MovementInput), turningSpeed, 1);
        //  transform.rotation = Quaternion.LookRotation(transform.forward,new Vector2(MovementInput.x,
        //                                                                          MovementInput.y));
        // transform.LookAt(toVector2(transform.forward) + MovementInput);
       // Vector3 lookVector = new Vector3(MovementInput.x, MovementInput.y, turningSpeed);
     //   if (lookVector.x != 0 && lookVector.y != 0)
       //     transform.rotation = Quaternion.LookRotation(lookVector, Vector3.forward);

    }
    private void RotateGameObject(Vector3 target, float RotationSpeed, float offset)
    {
        //https://www.youtube.com/watch?v=mKLp-2iseDc
        //get the direction of the other object from current object
        Vector3 dir = target - transform.position;
        //get the angle from current direction facing to desired target
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //set the angle into a quaternion + sprite offset depending on initial sprite facing direction
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        //Roatate current game object to face the target using a slerp function which adds some smoothing to the move
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }
     public Vector2 toVector2(Vector3 vec3)
    {
       return new Vector2(vec3.x,vec3.y);
    }
}
