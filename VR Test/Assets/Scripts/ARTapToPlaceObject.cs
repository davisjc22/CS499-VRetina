using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;

    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRaycast;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private bool eyeballPlaced = false;
    private GameObject EyeBall;
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {
        if (!eyeballPlaced) //only use the indicator if there is no eyeball in the scene yet
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();

            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
            }
        }
        //Once the eyeball is in the scene, it can be manipulated
        else
        {
            if (Input.GetMouseButton(0))//if the screen is touched or mouse is held down, the eyeball will rotate
            {
                //GameObject EyeBall = GameObject.Find("Eyeball"); //Instantiate the eyeball once it is placed in AR instead of searching for it
                EyeBall.transform.Rotate(0, 1, 0);
            }
        }

    }

    private void PlaceObject()
    {
        float heightFromFloor = .25f; //how high the eyeball should be off of the surface
        Vector3 newSpot = new Vector3(placementPose.position.x, heightFromFloor, placementPose.position.z); //retrieve the coordinates of the placement plane and add the height to it
        EyeBall = Instantiate(objectToPlace, newSpot, placementPose.rotation); //create the eyeball
        EyeBall.name = "EyeBall";

        eyeballPlaced = true; //the eyeball has been placed
        placementIndicator.SetActive(false); //turn off the placement indicator square since the eyeball has been placed
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
            placementIndicator.SetActive(false);
    }

    private void UpdatePlacementPose() //Moves the placement plane around
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        //arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);
        arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }
    }
}
