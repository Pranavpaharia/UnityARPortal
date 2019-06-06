using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine.UI;

public class PortalConroller : MonoBehaviour
{
    // Start is called before the first frame update
    List<DetectedPlane> m_NewdetectedPlanes = new List<DetectedPlane>();
    public GameObject codebox;
    GameObject uObject;
    bool bPlaced = false;
    public static bool BportalSpawned = false;
    GameObject[] pTrackedPlanes;
    GameObject planeGenerator;
    Material portalMat;
    CameraMonitor camScript;
    List<DetectedPlane> m_Allplanes = new List<DetectedPlane>();
    void Start()
    {
        pTrackedPlanes = new GameObject[30];
        planeGenerator = GameObject.Find("Plane Generator");
        portalMat = Resources.Load<Material>("PlaneGrid1") as Material;
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonitor>();
    }

   

    public void SpawnedPortal(GameObject btn)
    {
        BportalSpawned = !BportalSpawned;

        if(BportalSpawned)
        { 
            btn.GetComponent<Image>().color = Color.green;
            btn.transform.GetChild(0).GetComponent<Text>().text = "PortalFixed";
            btn.GetComponent<AudioSource>().Play();
            //portalMat.SetFloat("_Invert", 0);
            planeGenerator.GetComponent<DetectedPlaneGenerator>().bPlaneVisibility = false;
            pTrackedPlanes = GameObject.FindGameObjectsWithTag("Plane");
            camScript.SetDoorState(CameraMonitor.DoorState.Open);
            foreach(GameObject plane in pTrackedPlanes)
            {
                plane.GetComponent<DetectedPlaneVisualizer>().TogglePlaneVisibility(false);
            }
            
            //CameraMonitor.bDoorAnimToggle = true;
            //pTrackedPlane.GetComponent<Renderer>().material.SetFloat("_Invert", 0);
        }
        else
        {
            btn.GetComponent<Image>().color = Color.white;
            btn.transform.GetChild(0).GetComponent<Text>().text = "PortalNotFixed";
            btn.GetComponent<AudioSource>().Play();
            //portalMat.SetFloat("_Invert", 1);
            //pTrackedPlanes = GameObject.FindGameObjectWithTag("Plane") as GameObject;
            //pTrackedPlanes.GetComponent<DetectedPlaneVisualizer>().enabled = true;
            planeGenerator.GetComponent<DetectedPlaneGenerator>().bPlaneVisibility = true;
            pTrackedPlanes = GameObject.FindGameObjectsWithTag("Plane");
            foreach (GameObject plane in pTrackedPlanes)
            {
                plane.GetComponent<DetectedPlaneVisualizer>().TogglePlaneVisibility(true);
            }
        }
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


        if (BportalSpawned)
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
                if (!bPlaced)
                {

                    uObject = Instantiate<GameObject>(codebox,deltaPos , hit.Pose.rotation);
                    //frameObj.transform.Rotate(180,0,0,Space.Self);
                    uObject.transform.Rotate(0, 90, 0, Space.Self);
                    uObject.transform.parent = anchor.transform;
                    bPlaced = true;
                    
                    
                }
                else
                {
                    uObject.transform.position = deltaPos;
                }
            }
        }
    }
}
