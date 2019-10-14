using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    // Place this on the buttonTrigger object.

    public GameObject buttonTrigger;
    public GameObject buttonSphere;

    public GameObject leftDoor;
    public GameObject rightDoor;

    public bool isPressed = false;

    Animator leftAnim;
    Animator rightAnim;

    void Start()
    {
        leftAnim = leftDoor.GetComponent<Animator>();
        rightAnim = rightDoor.GetComponent<Animator>();

        buttonSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var buttonRenderer = buttonSphere.GetComponent<Renderer>();
        buttonRenderer.material.SetColor("_Color", Color.red);
    }

    void Update()
    {
        ChangeColor(); 
    }

    void ChangeColor()
    {
        if (isPressed == true)
        {
            buttonSphere.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isPressed = true;
            SlideDoors(true);
        }
    }

    void SlideDoors(bool state)
    {
        leftAnim.SetBool("slide", state);
        rightAnim.SetBool("slide", state);
    }
}
