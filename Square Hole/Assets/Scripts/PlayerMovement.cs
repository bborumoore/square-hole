using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float inputForce = 500f;
    public bool moveRight = false;
    public bool moveLeft = false;

    // Using FixedUpdate because it plays better with Unity Physics Engine
    void FixedUpdate()
    {
        // Adding a consistent forward force to move player
        rb.AddForce(0, 0 , forwardForce * Time.deltaTime);

        if( Input.GetKey("d")) {
            rb.AddForce( inputForce * Time.deltaTime, 0, 0);
        }

         if( Input.GetKey("a")) {
            rb.AddForce( -inputForce * Time.deltaTime, 0, 0);
        }

    }
}
