using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{ 
    protected bool open;
    bool enter = false;
    float openTime = 0;
    float defaultRotationAngle;
    float currentRotationAngle;

    public float doorOpenAngle = 90.0f;
    public float openSpeed = 2.0f;

    public Joybutton joybutton;

    // Start is called before the first frame update
    void Start()
    {
        joybutton = FindObjectOfType<Joybutton>();

        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Open();
    }

    void Open()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
            Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? doorOpenAngle : 0), openTime),
            transform.localEulerAngles.z);

        if (!open && (joybutton.Pressed || Input.GetButton("Fire1")))
        {
            open = true;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }

        if (open && !(joybutton.Pressed || Input.GetButton("Fire1")))
        {
            open = false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }
}
