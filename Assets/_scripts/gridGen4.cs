using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshColliderCookingOptions))]

public class gridGen4 : MonoBehaviour
{

    private int xSize = 10, ySize = 10;
    public float coef = 100.0f;
    public float desiredLength = 100.0f;
    public int planeNum = 4;
    private float a;

    // Use this for initialization
    void Start()
    {
        Awake();

    }

    private void Awake()
    {
        StartCoroutine(Generate());
    }


    // Update is called once per frame
    void Update()
    {


        a += Time.deltaTime;
        h = 5 * Mathf.Sin(0.25f * Mathf.PI * a);
        //positionUpdate(h);

        Mesh mesh2 = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices2 = mesh.vertices;

        //Vector3[] normals = mesh.normals;

        for (var i = 0; i < vertices2.Length; i++)
        {
            float refX = vertices2[i].x * 0.01f;
            float refY = vertices2[i].z * 0.01f;
            vertices2[i].y += 3 * Mathf.Sin(0.01f * coef * Mathf.PI * Time.time * refX * refY);
        }

        mesh2.vertices = vertices2;
        mesh2.RecalculateNormals();
        mesh2.RecalculateTangents();

        //SharedMesh method?
        DestroyImmediate(this.GetComponent<MeshCollider>());
        MeshCollider CMesh = gameObject.AddComponent<MeshCollider>();

        CMesh.sharedMesh = mesh2;


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

        scale = desiredLength / xSize;

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                //put different height functions here

                //h = contourPlane4(x - xSize/2, y - ySize/2);
                //h = Mathf.Sin(Mathf.Sqrt((x-50.0f) * (x + y * y)) / Mathf.Sqrt(x * x + y * y);
                //h = 0.0f;
                //h = Mathf.Sin(0.5f * x * Mathf.PI);                   //decide height
                h = recallPlane(planeNum, x - xSize / 2, y - ySize / 2);
                vertices[i] = new Vector3(x*scale, h, y*scale);
                uv[i] = new Vector2((float)x / xSize * scale, (float)y / ySize * scale);
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
        mc.convex = false;
        mc.inflateMesh = true;

        Transform tr = gameObject.GetComponent<Transform>();
        tr.position = new Vector3(-1.0f * desiredLength / 2.0f, 0.0f, -1.0f * desiredLength / 2.0f);
        //tr.localScale = new Vector3(desiredLength / scale, 1, desiredLength / scale);


        //position: [-desiredLength/2 0 -desiredlength/2]
        //scale:    [desiredLength/scale 1 desiredLength/scale]
        //





        yield return 0;
    }

    private float contourPlane(float x, float y)
    {
        return Mathf.Sin(Mathf.Sqrt(x * x + y * y) / Mathf.Sqrt(1.0f + x * x + y * y));
    }
    private float contourPlane2(float x, float y)
    {
        return ((1.0f - x) * (1.0f - x)) + 100 * Mathf.Pow((y - x * x), 2);
    }
    private float contourPlane3(float x, float y)
    {
        return Mathf.Sin(0.5f * Mathf.PI * x * y * y);
    }
    private float contourPlane4(float x, float y) //sphere
    {
        return (-Mathf.Sqrt(100f - x * x - y * y) + 10f) * 10.0f;
    }
    private float contourPlane5(float x, float y) //plane
    {
        return 1.0f;
    }


    private float recallPlane(int num, float x, float y){

        int caseNum = num;

        switch (caseNum)
        {
            case 1:
                return contourPlane(x, y);

            case 2:
                return contourPlane2(x, y);

            case 3:
                return contourPlane3(x, y);

            case 4:
                return contourPlane4(x, y);

            case 5:
                return contourPlane5(x, y);
            default:
                break;
        }

        return 0;
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


    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

}
