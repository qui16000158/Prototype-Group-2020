using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This will flip the normals of a mesh
// and then add a mesh collider
public class FlipNormals : MonoBehaviour
{
    [SerializeField]
    MeshFilter mf; // The object's mesh filter
    Mesh mesh; // The object's mesh

    // Start is called before the first frame update
    void Start()
    {
        mesh = mf.mesh; // Grab the mesh from the mesh filter

        int[] triangles = new int[mesh.triangles.Length]; // Create an array to hold new triangle indices
        // Jump through the array in threes, because triangles have 3 points
        for(int i = 0;i<triangles.Length;i+=3){
            // Flip the 1st, and third point of each triangle
            triangles[i] = mesh.triangles[i+2];
            triangles[i+2] = mesh.triangles[i];

            // Maintail 2nd point's index
            triangles[i+1] = mesh.triangles[i+1];
        }

        mesh.triangles = triangles; // Override triangles in the mesh

        gameObject.AddComponent<MeshCollider>(); // Add a mesh collider
    }
}
