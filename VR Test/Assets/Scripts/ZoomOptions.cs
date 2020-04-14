using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOptions : MonoBehaviour
{

    public GameObject EyeBall = GameObject.Find("Eyeball");


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
        EyeBall.transform.localScale -= new Vector3(1, 1, 1);
    }

    // Function to zoom out of the eye based on the slider
    public void ZoomOut()
    {
        EyeBall.transform.localScale += new Vector3(1, 1, 1);
    }
}
