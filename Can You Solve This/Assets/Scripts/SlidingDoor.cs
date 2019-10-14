using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    // Place this on the button object.

    public GameObject button;
    public GameObject leftDoor;
    public GameObject rightDoor;

    public bool isPressed = false;

    Animator leftAnim;
    Animator rightAnim;

    void Start()
    {
        leftAnim = leftDoor.GetComponent<Animator>();
        rightAnim = rightDoor.GetComponent<Animator>();
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
