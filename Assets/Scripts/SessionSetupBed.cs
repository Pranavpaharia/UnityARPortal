using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using GoogleARCore;
using UnityEngine;
using UnityEngine.EventSystems;
public class SessionSetupBed : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera appCamera;
    public GameObject FramePrefab;
    private const float k_modelRotation = 180.0f;
    bool bFramed = false;
    GameObject frameObj;
    void Start()
    {
        //To initialize at the start  
    }

    void ApplicationLifeCycle()
    {
        if(Session.Status == SessionStatus.None)
        {
            Debug.Log("AR Core not initialized");
        }

        if (Session.Status == SessionStatus.FatalError)
        {
            Debug.Log(" AR Core Fatal Error");
        }

        if (Session.Status == SessionStatus.LostTracking)
        {
            Debug.Log(" AR Core lost tracking");
        }

        if (Session.Status.IsError())
        {
            Debug.Log(" AR Core encountered problem in connecting");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ApplicationLifeCycle();

        Touch touch;
        if(Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if(Frame.Raycast(touch.position.x,touch.position.y,raycastFilter,out hit))
        {
            if((hit.Trackable is DetectedPlane) &&  Vector3.Dot(appCamera.transform.position - hit.Pose.position,hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current Detected Plane");
            }

            else
            {
                //GameObject prefab;
                if(hit.Trackable is FeaturePoint)
                {
                    //Instantiate<GameObject>(FramePrefab, hit.Pose.position, hit.Pose.rotation);
                    Debug.Log("Hit on point cloud");
                }
                else
                {
                   // Instantiate<GameObject>(FramePrefab,hit.Pose.position,hit.Pose.rotation);
                    Debug.Log("Hit on Plane");

                }
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                if (!bFramed)
                { 
                    frameObj = Instantiate<GameObject>(FramePrefab, hit.Pose.position, hit.Pose.rotation);
                    //frameObj.transform.Rotate(180,0,0,Space.Self);
                    //frameObj.transform.Rotate(90, 0, 0, Space.Self);
                    frameObj.transform.parent = anchor.transform;
                    bFramed = true;
                }
                else
                {
                    frameObj.transform.position = hit.Pose.position;
                }
            }
        }
    }
}
