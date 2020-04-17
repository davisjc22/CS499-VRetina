using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function to zoom in to the eye based on the slider
    public void ZoomIn()
    {
        GameObject EyeBall = GameObject.Find("Eyeball");
        Debug.Log("Zoom In Button Hit");
        EyeBall.transform.localScale -= new Vector3(1, 1, 1);
    }

    // Function to zoom out of the eye based on the slider
    public void ZoomOut()
    {
        GameObject EyeBall = GameObject.Find("Eyeball");
        Debug.Log("Zoom Out Button Hit");
        EyeBall.transform.localScale += new Vector3(1, 1, 1);
    }
}
