using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Needs this to add the input functionality
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // Reference the rigid body  - to prevent wacky collision
    Rigidbody rb;

    // Get some variables up and running
    Vector3 direction;
    [SerializeField] float multiplier = 1;
    bool onGround = true;





    // Start is called before the first frame update
    void Start()
    {
        // set the starting velocity to 0
        direction = Vector3.zero;    // zero = {0,0,0}

        // Links rb to the body of the plater
        rb = gameObject.GetComponent<Rigidbody>();
    }




    // Update is called once per frame
    void Update()
    {
        // Change our position based on our direction
        // transform.position += direction * multiplier * Time.deltaTime;

        // We can also add a force to move the player
        rb.AddForce(direction * multiplier);

        // And we can check the magnitude of this and normalize it - normal vector - just has a mangitude of 1
        /*
        if (rb.velocity.magnitude < 10)
        {
            // Vector3 test = new Vector3(123, 4397, 92342);
            // test.normalized = {1, 2, 3};

            rb.AddForce(direction * multiplier);
        }
        */
        
    }




    // When we move...
    void OnMove(InputValue value)
    {
        // Get the 2D vector input {x, y}
        Vector2 input = value.Get<Vector2>();

        float movementX = input.x;
        float movementZ = input.y;  // NOTE: the different directions - how unity treats up and left and right

        // Move us based on how we input - directly updates the position
        // transform.position += new Vector3(movementX, 0, movementZ);

        // Instead - update the velocity
        // velocity = new Vector3(movementX, 0, movementZ);

        // For better movement, apply the movement to the rigid body itself - accounts for collisions better
        // rb.velocity = new Vector3(movementX, 0, movementZ);

        direction = new Vector3(movementX, 0, movementZ);

        // And log the stuff so that we can see it
        Debug.Log(input);
    }



    // Jump - just a button push
    void OnJump(InputValue value)
    {
        // If we are off the ground, don't jump
        if (!onGround)
        {
            // break; - this is causing an error?
        }


        // Update our vertial position
        // transform.position += new Vector3(0, 1, 0);

        // Good way - add a vertical force to jump
        Vector3 jumpForce = new Vector3(0, 200, 0);
        rb.AddForce(jumpForce);

        // And throw a little message in there so that we can 
        Debug.Log("Jumping");
    }



    // Check if we are on the ground
    private void OnCollusionEnter(Collision collision)
    {
        // Another way to do this
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint c = collision.contacts[i];
            float ratioInDirection = Vector3.Dot(Vector3.up, c.normal);

            if (ratioInDirection > 0.8)
            {
                onGround = true;
            }
        }


        /*
        // Only if we are touching the floor
        if(collision.gameObject.CompareTag("Floor"))
        {
            onGround = true;
        }
        */
    }

    // Check if we are off the ground
    private void onCollisionExit(Collision collision)
    {

        // Another way to do this
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint c = collision.contacts[i];
            float ratioInDirection = Vector3.Dot(Vector3.up, c.normal);

            if (ratioInDirection > 0.8)
            {
                onGround = false;
            }
        }


        // Only if we are not touching the floor
        /*
        if (collision.gameObject.CompareTag("Floor"))
        {
            onGround = false;
        }
        */
    }


}
