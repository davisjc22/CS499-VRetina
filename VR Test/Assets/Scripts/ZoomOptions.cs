using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomOptions : MonoBehaviour
{
    private GameObject EyeBall;
    private Vector3 EyeOrigin;
    private Vector3 CameraOrigin;
    private float distanceBetween;

    // Start is called before the first frame update
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
            distanceBetween = Vector3.Distance(CameraOrigin, EyeOrigin);
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
        //EyeBall.transform.localPosition

    }
}
