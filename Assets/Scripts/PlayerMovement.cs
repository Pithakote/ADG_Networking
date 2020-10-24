using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    float speed = 5;
    int turningSpeed = 5000;
    //   [SerializeField]
    //  int _playerIndex = 0;
    public Vector2 MovementInput { get; set; }//property value set in PlayerController
                                              //   public int PlayerIndex { get { return _playerIndex; } }

    public Vector3 _mousePos;
    [SerializeField] float offset;
    Rigidbody2D rb;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //transform.Translate(new Vector2(MovementInput.x, MovementInput.y)
        //                                * speed
        //                                * Time.deltaTime);

        rb.MovePosition(rb.position + MovementInput * speed * Time.deltaTime);
      

        
       //  get playercontroller
          MouseRotation();

       // GetComponent<Transform>().Rotate(Vector3.back * _mousePos.x  * speed);
      
    }

    private void MouseRotation()
    {
        //var lookDir = _mousePos - transform.position;
        var lookDir = _mousePos ;
        //  var lookDir = new Vector2(_mousePos.x, _mousePos.y) - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offset;
        //-offset don't need to calculate with offser because forward vector is the right i.e the read arrow 
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      //  GetComponent<Transform>().Rotate(Vector3.back * _mousePos.x * speed);
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
