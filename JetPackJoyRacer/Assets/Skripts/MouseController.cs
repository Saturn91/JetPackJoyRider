using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

    public float jetpackForce = 75.0f;
    public float forwardMovementSpeed = 3.0f;

    private Rigidbody2D rigidbody_2D;

    // Use this for initialization
    void Start () {
        rigidbody_2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");

        if (jetpackActive)
        {
            rigidbody_2D.AddForce(new Vector2(0, jetpackForce));
        }

        Vector2 newVelocity = rigidbody_2D.velocity;
        newVelocity.x = forwardMovementSpeed;
        rigidbody_2D.velocity = newVelocity;
    }
}
