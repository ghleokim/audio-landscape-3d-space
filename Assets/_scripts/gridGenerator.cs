using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]

public class gridGenerator : MonoBehaviour {

    private int xSize = 10, ySize = 10;
    public float coef = 1.0f;
    public float desiredLength = 100.0f;
    private float a;



    // Use this for initialization
    void Start () {
        Awake();
        
    }

    private void Awake()
    {
        StartCoroutine(Generate());
    }


    // Update is called once per frame
    void Update () {
        /*
       
        a += Time.deltaTime;
        h = 5 * Mathf.Sin(0.25f * Mathf.PI * a);
        //positionUpdate(h);

        Mesh mesh2 = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices2 = mesh.vertices;
        Vector3[] normals = mesh.normals;

        for (var i = 0; i < vertices2.Length; i++)
        {
            vertices2[i] += normals[i] * Mathf.Sin(Time.time);
        }

        mesh2.vertices = vertices2;*/

    }


   
    private Vector3[] vertices;
    private Mesh mesh;
    private float h;
    private float scale;

    private IEnumerator Generate()
    {
        //WaitForSeconds wait = new WaitForSeconds(0.05f);    //delay to show rendering

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();    //getcomponent mesh
        mesh.name = "procedural grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);


        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                //put different height functions here

                h = Mathf.Sin(Mathf.Sqrt(x*x+y*y))/Mathf.Sqrt(x*x+y*y);

                //h = Mathf.Sin(0.5f * x * Mathf.PI);                   //decide height
                vertices[i] = new Vector3(x, h, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i] = tangent;
            }
        }


        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.tangents = tangents;

        MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        mc.convex = true;
        mc.inflateMesh = true;

        scale = desiredLength / xSize;

        Transform tr = gameObject.GetComponent<Transform>();
        tr.position = new Vector3(-1.0f * desiredLength / 2.0f, 0.0f, -1.0f * desiredLength / 2.0f);
        tr.localScale = new Vector3(desiredLength / scale, 1, desiredLength / scale);


        //position: [-desiredLength/2 0 -desiredlength/2]
        //scale:    [desiredLength/scale 1 desiredLength/scale]
        //





        yield return 0;
    }

    /*private void positionUpdate(float nh)
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();    //getcomponent mesh
        mesh.name = "procedural grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];


        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, nh, y);
               
            }
        }
    }*/

    private void OnDrawGizmos() {
        if (vertices == null) return;
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++){
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

}
