using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuAdjust : MonoBehaviour
{
    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Landscape)
            this.GetComponent<GridLayoutGroup>().constraintCount = 4;
        else
            this.GetComponent<GridLayoutGroup>().constraintCount = 2;
    }
}
