using System.Collections;
using System.Collections.Generic;
using GoogleARCore;

using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour
{
    private List<DetectedPlane> m_NewTrackedPlanes = new List<DetectedPlane>();

    public GameObject GridPrefab;

    public GameObject Level;

    public GameObject ARCamera;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detection();

        Touch();
    }
    

    void Detection()
    {
        // Check ARCore session status
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // The following function will filll the list with the planes that ARCore detected in the current frame
        Session.GetTrackables<DetectedPlane>(m_NewTrackedPlanes, TrackableQueryFilter.New);

        for (int i = 0; i < m_NewTrackedPlanes.Count; ++i)
        {
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);

            grid.GetComponent<GridVisualiser>().Initialize(m_NewTrackedPlanes[i]);
        }
    }

    void Touch()
    {
        Touch touch;

        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHit hit;
        if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            Level.SetActive(true);

            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            Level.transform.position = hit.Pose.position;
            Level.transform.rotation = hit.Pose.rotation;

            Vector3 cameraPosition = ARCamera.transform.position;

            cameraPosition.y = hit.Pose.position.y;

            Level.transform.LookAt(cameraPosition, Level.transform.up);

            Level.transform.parent = anchor.transform;
        }
    }
}
