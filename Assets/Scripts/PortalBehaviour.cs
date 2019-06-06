using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PortalBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject showroom;
    Vector3 position = new Vector3(-13.28023f,-0.28f,1.9355f);
    bool forward, backward, leftside, rightside;
    Text DebugText;
    VideoPlayer vidplayer;
    GameObject MalePrefab;
    GameObject MainCamera;
    bool bMovie;
    AudioSource audioCompDoorC;
    AudioClip laptopSound,DoorCloseSound;
    int voicetype = 1;
    
    void Start()
    {
        //Debug.Log(transform.GetChild(1).transform.GetChild(4).transform.GetChild(3).name);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonitor>().DoorPrefab = transform.GetChild(3).transform.GetChild(0).gameObject;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonitor>().PortalPrefab = this.gameObject;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonitor>().SetPortalPrefab();

        vidplayer = transform.GetChild(1).transform.GetChild(4).transform.GetChild(3).GetComponent<VideoPlayer>();
        MalePrefab = transform.GetChild(1).transform.GetChild(7).gameObject;
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
        audioCompDoorC = transform.GetChild(0).GetComponent<AudioSource>();
        Debug.Log(audioCompDoorC);
        laptopSound = Resources.Load<AudioClip>("Voice/blip2");
        DoorCloseSound = Resources.Load<AudioClip>("Voice/doorclose");

        if(Application.loadedLevelName == "TestPortal")
        { 
            DebugText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Text>();
            //DebugText.text = "InportalScript";
        }
        //PositionText = GameObject.FindGameObjectWithTag("Position").GetComponent<Text>();
        //if(PositionText!= null)
        //PositionText.text = "Got Reference";
        //showroom = Resources.Load<GameObject>("Showroom") as GameObject;
        // position = transform.position + position;
        // Quaternion rot = transform.rotation * Quaternion.Euler(0,86.5f,0);
        //Quaternion rot = Quaternion.Euler(0, 86.5f, 0);
        //Instantiate<GameObject>(showroom,position,rot);

    }

    void PlayVoiceOver1()
    {
        AudioClip clip2 = Resources.Load<AudioClip>("Voice/1");
        MalePrefab.GetComponent<AudioSource>().clip = clip2;
        MalePrefab.GetComponent<AudioSource>().Play();
    }

    public void DoorOpenSound()
    {
        MalePrefab.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Voice/dooropen");
        MalePrefab.GetComponent<AudioSource>().Play();
    }


    public void Play1stAnimVoiceOver()
    {
        MalePrefab.GetComponent<Animation>().Play();

        if (!MalePrefab.GetComponent<AudioSource>().isPlaying)
        {
            PlayVoiceOver1();
        }
    }

    void Play2dnAnimVoiceOver()
    {
        
        
    }

    IEnumerator SpeakDialogues()
    {
        AudioClip clip2 = Resources.Load<AudioClip>("Voice/2");
        AudioClip clip3 = Resources.Load<AudioClip>("Voice/3");
        AudioClip clip4 = Resources.Load<AudioClip>("Voice/4");
        MalePrefab.GetComponent<AudioSource>().clip = clip2;
        MalePrefab.GetComponent<AudioSource>().Play();
        MalePrefab.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(clip2.length);
        MalePrefab.GetComponent<AudioSource>().clip = clip3;
        MalePrefab.GetComponent<AudioSource>().Play();
        MalePrefab.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(clip3.length);
        MalePrefab.GetComponent<AudioSource>().clip = clip4;
        MalePrefab.GetComponent<AudioSource>().Play();
        MalePrefab.GetComponent<Animation>().Play();
        //yield return new WaitForSeconds(clip4.length);
    }

    public void SalesManCloseDialogue()
    {
        //if (MalePrefab.GetComponent<AudioSource>().isPlaying)
        //    return;
        StartCoroutine(SpeakDialogues());

        //if(voicetype == 1)
        //{
        //    MalePrefab.GetComponent<Animation>().Play();
        //    MalePrefab.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Voice/2");
        //    MalePrefab.GetComponent<AudioSource>().Play();
        //}
        //else if(voicetype == 2)
        //{
        //    MalePrefab.GetComponent<Animation>().Play();
        //    MalePrefab.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Voice/3");
        //    MalePrefab.GetComponent<AudioSource>().Play();
        //}
        //else if (voicetype == 3)
        //{
        //    MalePrefab.GetComponent<Animation>().Play();
        //    MalePrefab.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Voice/4");
        //    MalePrefab.GetComponent<AudioSource>().Play();
        //}

        //voicetype++;

        //if (voicetype > 3)
        //    voicetype = 1;


    }

    public void DoorCloseSoundPlay()
    {
        if(!audioCompDoorC.isPlaying)
        {
            audioCompDoorC.clip = DoorCloseSound;
            audioCompDoorC.PlayDelayed(0.6f);
        }
    }


    public void ToggleMusicPlayer()
    {
        if(bMovie)
        {
            DebugText.text = "VideoPlay";
            bMovie = !bMovie;
            vidplayer.Pause();
            audioCompDoorC.clip = laptopSound;
            audioCompDoorC.Play();
        }
        else
        {
            DebugText.text = "VideoPause";
            bMovie = !bMovie;
            vidplayer.Play();
            audioCompDoorC.clip = laptopSound;
            audioCompDoorC.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PortalConroller.BportalSpawned)
        { 
            
            MalePrefab.transform.LookAt(MainCamera.transform.GetChild(0));
        }
    }

    public void OnPressRight()
    {
        rightside = !rightside;
    }

    public void OnPressLeft()
    {
        leftside = !leftside;
    }

    public void OnPressUp()
    {
        forward = !forward;
    }

    public void OnPressDown()
    {
        backward = !backward;
    }


}
