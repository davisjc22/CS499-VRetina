using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MainMenu : MonoBehaviour
{
    private GameObject EyeBall;
    private Vector3 EyeOrigin;
    private Vector3 CameraOrigin;
    private float distanceBetween;

    void Start()
    {
        StartCoroutine(LoadDevice(""));
    }

    // Update is called once per frame
    void Update()
    {
        if (!EyeBall)
        {
            EyeBall = GameObject.Find("Eyeball");
            EyeOrigin = EyeBall.transform.position;
            CameraOrigin = GameObject.FindWithTag("MainCamera").transform.position;
            distanceBetween = Vector3.Distance(CameraOrigin, EyeOrigin);
        }
    }

    //Loads the VR mode of the app
    public void LoadVR()
    {
        SceneManager.LoadScene("VRDemo");
    }

    //Loads the AR mode of the app
    public void LoadAR()
    {
        SceneManager.LoadScene("ARDemo");
    }

    // Loads the main menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void toggleMenuAnimation(Animator anim)
    {
        bool isOpen = anim.GetBool("Open");
        anim.SetBool("Open", !isOpen);
    }

    IEnumerator LoadDevice(string newDevice)
    {
        if (String.Compare(XRSettings.loadedDeviceName, newDevice, true) != 0)
        {
            XRSettings.LoadDeviceByName(newDevice);
            yield return null;
            XRSettings.enabled = true;
        }
    }

    // Function to zoom in to the eye based on the slider
    public void ZoomIn()
    {
        //GameObject EyeBall = GameObject.Find("Eyeball");
        Debug.Log("Zoom In Button Hit");
        EyeBall.transform.localScale -= new Vector3(1, 1, 1);
    }

    // Function to zoom out of the eye based on the slider
    public void ZoomOut()
    {
        //GameObject EyeBall = GameObject.Find("Eyeball");
        Debug.Log("Zoom Out Button Hit");
        EyeBall.transform.localScale += new Vector3(1, 1, 1);
    }

    public void updateZoom()
    {
        //Slider from 0 to 1. 0 is the eyeball's origin, 1 is the cameras position
        //slider value * distance between camera and eyeball origin = new position

        float zoomVal = GameObject.Find("Zoom Slider").GetComponent<Slider>().value;
        Debug.Log("Zoom Slider Hit");
        Vector3 changeVal = new Vector3(0, 0, -distanceBetween*zoomVal) + EyeOrigin;
        EyeBall.transform.position = changeVal;
    }
}
