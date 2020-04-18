using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class CreateMesh : MonoBehaviour
{
    public void CreateLabels(List<Label> labelsIn, GameObject eyeball)
    {
        GameObject labels = new GameObject("Labels");
        labels.transform.SetParent(eyeball.transform);
        labels.transform.localPosition = new Vector3(0, 0, 0);
        foreach (Label label in labelsIn)
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
}
