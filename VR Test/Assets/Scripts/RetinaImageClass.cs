using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class RetinaImage
{
    public int id;
    public string name;
    public int xSize;
    public int ySize;
    public string filetype;
    public bool official;
    public string uploaded;
    public string image;
    public List<Label> labels;

    public RetinaImage()
    {
        id = -1;
        name = "";
        xSize = 0;
        ySize = 0;
        filetype = "";
        official = false;
        uploaded = "";
        image = "";
    }
}

[Serializable]
public class Label
{
    public int labelId;
    public string labelName;
    public List<Mesh> meshes;
}

[Serializable]
public class Mesh
{
    public int meshId;
    public List<Vertex> vertices;
    public List<Triangle> triangles;

    public Mesh(Mesh inMesh)
    {
        Debug.Log(inMesh);
        Debug.Log(inMesh.vertices[0].x);
        Debug.Log(inMesh.meshId);
        meshId = inMesh.meshId;
    }
}

[Serializable]
public class Vertex
{
    public int vertexNum;
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class Triangle
{
    public int triangleNum;
    public int a;
    public int b;
    public int c;
}
