using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private float maxSpeed = 5.0f;

    public GameObject deathParticles;

    private Rigidbody rBody;
    //private Vector3 input;

    //Inputs
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

        fallingDeath();
    }

    void Controls()
    {
        lhAxis = Input.GetAxisRaw("Horizontal");

        lvAxis = Input.GetAxisRaw("Vertical");

        //input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //if (rBody.velocity.magnitude < maxSpeed)
        //{
        //    rBody.AddRelativeForce(input * moveSpeed);
        //}
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

    void OnCollisionEnter(Collision other)
    {
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
