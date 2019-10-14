using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    private float maxSpeed = 5.0f;

    public float jumpSpeed = 5.0f;
    public bool isGrounded;

    public GameObject deathParticles;

    private Rigidbody rBody;

    //Inputs
    public Joystick joystick;
    public Joybutton joybutton;

    private float lvAxis;
    private float lhAxis;

    private Vector3 spawn;

    // Start is called before the first frame update
    void Start()
    {

        spawn = transform.position;

        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Controls();

        Movement();

        Jump();

        fallingDeath();
    }

    void Controls()
    {
        lhAxis = joystick.Horizontal;
        lvAxis = joystick.Vertical;
    }

    void Movement()
    {
        Vector3 move = new Vector3(lhAxis, 0.0f, lvAxis);

        if (rBody.velocity.magnitude < maxSpeed)
        {
            rBody.AddForce(move * moveSpeed);
        }
    }

    void fallingDeath()
    {
        if (transform.position.y < -2)
        {
            Death();
        }
    }

    void Jump()
    {
        if (joybutton.Pressed && isGrounded)
        {
            rBody.AddForce(new Vector3(0, 2, 0) * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }

        if (other.transform.CompareTag("Enemy"))
        {
            Death();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Goal")
        {
            GameManager.CompleteLevel();
        }
    }

    void Death()
    {
        Instantiate(deathParticles, transform.position, Quaternion.Euler(270, 0, 0));
        transform.position = spawn;
    }
}
