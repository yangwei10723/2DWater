                      using UnityEngine;
using System.Collections.Generic;


public class X2DWater : MonoBehaviour
{
    public MeshFilter mf;
    public int segmentCount;
    public float A = 2;
    public float W = 2;
    public float speed = 0.1f;
    public float xCoef = 30;
    public float fi = 0.1f;
    private Mesh _mesh;

    float screenWidth;
    float cameraSize;
    private void Awake()
    {
        mf.mesh = _mesh = new Mesh();
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        cameraSize = Camera.main.orthographicSize;
    }

    private void Update()
    {
        float interval = screenWidth / segmentCount;
        float uvInterval = 1f / segmentCount;

        List<Vector3> vertList = new List<Vector3>();
        List<Vector2> uvList = new List<Vector2>();
        List<int> triangleList = new List<int>();
        for (int i = 0; i < segmentCount; i++)
        {            
            float point1X = interval * i;
            float point2X = interval * (i + 1);

            float point1Y = transform.position.y + A * Mathf.Cos(W * (Time.time * speed - point1X * xCoef) + fi);
            float point2Y = transform.position.y + A * Mathf.Cos(W * (Time.time * speed - point2X * xCoef) + fi);

            point1X -= screenWidth * 0.5f;
            point2X -= screenWidth * 0.5f;

            Vector3 point1 = new Vector3(point1X, point1Y, 0);
            Vector3 point2 = new Vector3(point2X, point2Y, 0);
            Vector3 point3 = new Vector3(point2X, -cameraSize, 0);
            Vector3 point4 = new Vector3(point1X, -cameraSize, 0);
            vertList.Add(point1);
            vertList.Add(point2);
            vertList.Add(point3);
            vertList.Add(point4);
            uvList.Add(new Vector2(i * uvInterval, 1));
            uvList.Add(new Vector2((i + 1) * uvInterval, 1));
            uvList.Add(new Vector2((i + 1) * uvInterval, 0));
            uvList.Add(new Vector2(i * uvInterval, 0));

            int startIndex = i * 4;
            triangleList.Add(startIndex);
            triangleList.Add(startIndex + 1);
            triangleList.Add(startIndex + 3);
            triangleList.Add(startIndex + 1);
            triangleList.Add(startIndex + 2);
            triangleList.Add(startIndex + 3);
        }

        _mesh.Clear();
        _mesh.SetVertices(vertList);
        _mesh.SetTriangles(triangleList, 0);
        _mesh.SetUVs(0, uvList);
    }
}
