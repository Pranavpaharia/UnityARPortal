using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore;

public class PortalConroller : MonoBehaviour
{
    // Start is called before the first frame update
    List<DetectedPlane> m_NewdetectedPlanes = new List<DetectedPlane>();
    public GameObject codebox;
    GameObject uObject;
    bool bFramed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;
        if (Session.Status != SessionStatus.Tracking)
            return;

        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if(Frame.Raycast(touch.position.x,touch.position.y,raycastFilter, out hit))
        {
            if((hit.Trackable is DetectedPlane) && Vector3.Dot(Camera.main.transform.position - hit.Pose.position,hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                //GameObject prefab;
                if(hit.Trackable is FeaturePoint)
                {

                }
                else
                {

                }

                // = Instantiate(codebox, hit.Pose.position, hit.Pose.rotation);

                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                //uObject.transform.parent = anchor.transform;
                Vector3 deltaPos = new Vector3(hit.Pose.position.x, hit.Pose.position.y + 0.01f, hit.Pose.position.z);
                if (!bFramed)
                {

                    uObject = Instantiate<GameObject>(codebox,deltaPos , hit.Pose.rotation);
                    //frameObj.transform.Rotate(180,0,0,Space.Self);
                    //frameObj.transform.Rotate(90, 0, 0, Space.Self);
                    uObject.transform.parent = anchor.transform;
                    bFramed = true;
                }
                else
                {
                    uObject.transform.position = deltaPos;
                }
            }
        }
    }
}
