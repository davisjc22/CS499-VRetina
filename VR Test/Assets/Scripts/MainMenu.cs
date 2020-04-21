using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    private GameObject EyeBall;
    private Vector3 EyeOrigin;
    private Vector3 CameraOrigin;
    private float DistanceBetween;
    public bool MenuActive = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!EyeBall)
        {
            EyeBall = GameObject.Find("Eyeball");
            EyeOrigin = EyeBall.transform.position;
            CameraOrigin = GameObject.FindWithTag("MainCamera").transform.position;
            DistanceBetween = Vector3.Distance(CameraOrigin, EyeOrigin);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            LoadStartMenu();
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
        StartCoroutine(LoadDevice(""));
        SceneManager.LoadScene("ARDemo");
    }

    // Loads the main menu scene
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void toggleMenuAnimation(GameObject Menu)
    {
        Animator anim = Menu.GetComponent<Animator>();
        bool isOpen = anim.GetBool("Open");

        if (MenuActive)
        {
            anim.SetBool("Open", !isOpen);
            MenuActive = !MenuActive;
            Menu.SetActive(MenuActive);
        }
        else
        {
            MenuActive = !MenuActive;
            Menu.SetActive(MenuActive);
            anim.SetBool("Open", !isOpen);
        }
        Debug.Log("IsOpen: " + isOpen);
        
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
        EyeBall.transform.position += new Vector3(0, 0, .5f);
    }

    // Function to zoom out of the eye based on the slider
    public void ZoomOut()
    {
        EyeBall.transform.position -= new Vector3(0, 0, .5f);
    }

    public void updateZoom()
    {
        //Slider from 0 to 1. 0 is the eyeball's origin, 1 is the cameras position
        //slider value * distance between camera and eyeball origin = new position

        float zoomVal = GameObject.Find("Zoom Slider").GetComponent<Slider>().value;
        Vector3 changeVal = new Vector3(0, 0, -DistanceBetween*zoomVal) + EyeOrigin;
        EyeBall.transform.position = changeVal;
    }

}
