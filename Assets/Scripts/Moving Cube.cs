using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Create the variable - add the serialize field so that we can edit it within unity
    [SerializeField] float speed = 5;

    // Create a new velocity value
    Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        if (transform.position.x > 10)
        {
            velocity = new Vector3(-speed, 0, 0);
        }
        else if (transform.position.x < -10)
        {
            velocity = new Vector3(speed, 0, 0);
        }


    }


}
