using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SphereCreator : MonoBehaviour
{
    public GameObject square_template;
    public Material material;

    static int lattitudes = 40;
    static int longitudes = 4 * lattitudes;
    static float angle = 2 * Mathf.PI / longitudes; // unit angle in radians
    static float radius = 1f;

    Vector3 flatToSphere(Vector2 v)
    {
        return new Vector3(radius * Mathf.Cos(v[0] * angle) * Mathf.Cos(v[1] * angle), radius * Mathf.Sin(v[1] * angle), radius * Mathf.Sin(v[0]* angle) * Mathf.Cos(v[1] * angle));
    }

    // Start is called before the first frame update
    void Start()
    {
        int objects_created = 0;
        for(int y = -lattitudes ; y < lattitudes; y++)
        {
            for (int x = 0; x < longitudes; x++)
            {
                Vector2 v00 = new Vector2(x, y);
                Vector2 v10 = new Vector2(x + 1, y);
                Vector2 v01 = new Vector2(x, y + 1);
                Vector2 v11 = new Vector2(x + 1, y + 1);

                Vector3 sv00 = flatToSphere(v00);
                Vector3 sv10 = flatToSphere(v10);
                Vector3 sv01 = flatToSphere(v01);
                Vector3 sv11 = flatToSphere(v11);

                Vector3[] sphereVertices = new Vector3[] { sv00, sv10, sv01, sv11 };

                Mesh mesh = new Mesh();
                mesh.vertices = sphereVertices;
                mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) }; //values here don't matter except for texture purposes
                mesh.triangles = new int[] { 0, 2, 3, 0, 3, 1 };

                Debug.Log("creating object with vertices: " + sphereVertices[0] + ", " + sphereVertices[1] + ", " + sphereVertices[2] + ", " + sphereVertices[3]);
                GameObject gameobject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
                gameobject.transform.localScale = new Vector3(1, 1, 1);
                gameobject.GetComponent<MeshFilter>().mesh = mesh;
                gameobject.GetComponent<MeshRenderer>().material = material;
                gameobject.AddComponent<MeshCollider>();

                objects_created += 1;
            }
        }
        Debug.Log("created " + objects_created + " objects total");
    }

    // Update is called once per frame
    void Update()
    {


    }
}
