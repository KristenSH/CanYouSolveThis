using System.Collections;
using System.Collections.Generic;
using GoogleARCore;

using UnityEngine;
using UnityEngine.EventSystems;

public class ARController : MonoBehaviour
{
    public Camera FirstPersonCamera;

    public GameObject GameObjectVerticalPlanePrefab;

    public GameObject GameObjectHorizontalPlanePrefab;

    public GameObject GameObjectPointPrefab;

    private const float k_PrefabRotation = 180.0f;

    private bool m_IsQuitting = false;


    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateApplicationLifecycle();

        Touch();
    }


    void Touch()
    {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                GameObject prefab;
                if (hit.Trackable is FeaturePoint)
                {
                    prefab = GameObjectPointPrefab;
                }
                else if (hit.Trackable is DetectedPlane)
                {
                    DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                    if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
                    {
                        prefab = GameObjectVerticalPlanePrefab;
                    }
                    else
                    {
                        prefab = GameObjectHorizontalPlanePrefab;
                    }
                }
                else
                {
                    prefab = GameObjectHorizontalPlanePrefab;
                }

                var gameObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                gameObject.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);

                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                gameObject.transform.parent = anchor.transform;


            }
        }
    }
    private void UpdateApplicationLifecycle()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (m_IsQuitting)
        {
            return;
        }

        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            ShowAndroidToastMessage(
                "ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }

    private void DoQuit()
    {
        Application.Quit();
    }

    
    private void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
