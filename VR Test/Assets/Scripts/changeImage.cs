using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class changeImage : MonoBehaviour
{
    public Texture2D texture;
    public List<Label> labels;

    public void updateImage()
    {
        GameObject view = GameObject.Find("Scroll View");
        if (view != null)
        {
            view.gameObject.SetActive(false);
        }

        GameObject eyeball = GameObject.Find("Eyeball");
        eyeball.GetComponent<Renderer>().material.mainTexture = texture;

        this.GetComponent<DestroyMesh>().DestroyLabels(eyeball);
        this.GetComponent<CreateMesh>().CreateLabels(labels, eyeball);
    }
}
