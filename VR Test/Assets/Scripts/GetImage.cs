/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetImage : MonoBehaviour
{
    private Renderer rend;
    private Mesh mesh;
    public RetinaImage retinaImage;
    private bool notLoaded;
    public int imageId = 8;
    public Vector3 scale = new Vector3(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        retinaImage = new RetinaImage();
        notLoaded = true;
        StartCoroutine(LoadImage(imageId));
    }

    // Update is called once per frame
    void Update()
    {
        if (notLoaded && retinaImage.id != -1)
        {
            Debug.Log("Applied Image.");
            notLoaded = false;
            ApplyImage(rend);
            CreateLabels();
        }
    }

    public IEnumerator RequestImage(int id)
    {
        WWWForm form = new WWWForm();

        form.AddField("id", id);
        form.AddField("clientType", "app");

        using (UnityWebRequest www = UnityWebRequest.Post("http://ec2-13-58-160-235.us-east-2.compute.amazonaws.com/request.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Webserver request complete");

                string phrase = www.downloadHandler.text;

                retinaImage = JsonUtility.FromJson<RetinaImage>(phrase);

                Debug.Log("Retina image object updated.");
            }
        }
    }

    public void CreateLabels()
    {
        GameObject labels = new GameObject("Labels");
        labels.transform.SetParent(this.transform);
        labels.transform.localPosition = new Vector3(0, 0, 0);
        foreach (Label label in retinaImage.labels)
        {
            GameObject componentLabel = new GameObject(label.labelId.ToString());
            Text labelName = componentLabel.AddComponent<Text>();
            labelName.text = label.labelName;
            componentLabel.GetComponent<Text>().enabled = false;

            foreach (Mesh mesh in label.meshes)
            {
                GameObject componentMesh = new GameObject(mesh.meshId.ToString());
                MeshFilter mf = componentMesh.AddComponent<MeshFilter>();
                MeshCollider mc = componentMesh.AddComponent<MeshCollider>();

                UnityEngine.Mesh genMesh = new UnityEngine.Mesh();

                Vector3[] newVertices = new Vector3[mesh.vertices.Count];
                int[] newTriangles = new int[mesh.triangles.Count * 3];

                foreach (Vertex vertex in mesh.vertices)
                {
                    newVertices[vertex.vertexNum] = new Vector3(vertex.x, vertex.y, vertex.z);
                }

                foreach (Triangle triangle in mesh.triangles)
                {
                    newTriangles[triangle.triangleNum * 3] = triangle.a;
                    newTriangles[(triangle.triangleNum * 3) + 1] = triangle.b;
                    newTriangles[(triangle.triangleNum * 3) + 2] = triangle.c;
                }
                genMesh.vertices = newVertices;
                genMesh.triangles = newTriangles;

                mf.mesh = genMesh;
                mc.sharedMesh = genMesh;
                componentMesh.transform.SetParent(componentLabel.transform);
                componentMesh.transform.localPosition = new Vector3(0, 0, 0);
            }
            componentLabel.transform.SetParent(labels.transform);
            componentLabel.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void ApplyImage(Renderer rend)
    {
        byte[] imageBytes = Convert.FromBase64String(retinaImage.image);
        Texture2D tex = new Texture2D(retinaImage.xSize, retinaImage.ySize);
        tex.LoadImage(imageBytes);
        rend.material.SetTexture("_MainTex", tex);
        Debug.Log("Updated Texture on model.");
    }

    public void SaveToFile()
    {
        string destinationFolder = Application.persistentDataPath + "/retinaImages/";
        string destination = destinationFolder + retinaImage.id.ToString() + ".dat";
        FileStream file;

        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, retinaImage);
        file.Close();
    }

    public IEnumerator LoadImage(int id)
    {
        string destinationFolder = Application.persistentDataPath + "/retinaImages/";
        string destination = destinationFolder + id.ToString() + ".dat";
        Debug.Log(destination);
        FileStream file;

        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        if (File.Exists(destination))
        {
            Debug.Log("Loaded from file");
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            RetinaImage tempRetinaImage = (RetinaImage)bf.Deserialize(file);
            retinaImage.id = tempRetinaImage.id;
            retinaImage.name = tempRetinaImage.name;
            retinaImage.xSize = tempRetinaImage.xSize;
            retinaImage.ySize = tempRetinaImage.ySize;
            retinaImage.filetype = tempRetinaImage.filetype;
            retinaImage.official = tempRetinaImage.official;
            retinaImage.uploaded = tempRetinaImage.uploaded;
            retinaImage.image = tempRetinaImage.image;
            retinaImage.labels = tempRetinaImage.labels;
            file.Close();
        }
        else
        {
            Debug.Log("Downloading from webserver");
            StartCoroutine(RequestImage(id));
            while (retinaImage.id == -1)
                yield return new WaitForSeconds(0.1f);
            SaveToFile();
            Debug.Log("Saving to File");
        }
    }
};
*/