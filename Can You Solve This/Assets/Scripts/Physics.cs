using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    public float moveSpeed;
    private float maxSpeed = 5.0f;

    public GameObject deathParticles;

    private Rigidbody rBody;
    private Vector3 input;

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
    }

    void Controls()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (rBody.velocity.magnitude < maxSpeed)
        {
            rBody.AddForce(input * moveSpeed);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            transform.position = spawn;
        }
    }
}
