using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class DestroyMesh : MonoBehaviour
{
    public void DestroyLabels(GameObject eyeball)
    {
        GameObject labels = GameObject.Find("Labels");
        
        if (labels != null)
            Destroy(labels);
    }
}
