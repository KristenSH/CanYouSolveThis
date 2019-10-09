using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    // Opening directions
    public enum OpenDirection { x, y, z };

    public OpenDirection direction = OpenDirection.y;
    public float openDistance = 3f;
    public float openSpeed = 2.0f;
    public Transform doorBody;

    protected bool open;
    public Joybutton joybutton;

    Vector3 defaultDoorPosition;

    // Start is called before the first frame update
    void Start()
    {
        if(doorBody)
        {
            defaultDoorPosition = doorBody.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Open();  
    }

    void Open()
    {
        if (!doorBody)
            return;

        if (direction == OpenDirection.x)
        {
            doorBody.localPosition = new Vector3(Mathf.Lerp(doorBody.localPosition.x, defaultDoorPosition.x +
                (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody.localPosition.y, doorBody.localPosition.z);
        }

        else if (direction == OpenDirection.y)
        {
            doorBody.localPosition = new Vector3(Mathf.Lerp(doorBody.localPosition.x, defaultDoorPosition.x +
                (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody.localPosition.y, doorBody.localPosition.z);
        }

        else if (direction == OpenDirection.z)
        {
            doorBody.localPosition = new Vector3(Mathf.Lerp(doorBody.localPosition.x, defaultDoorPosition.x +
                (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody.localPosition.y, doorBody.localPosition.z);
        }
    }
}
