using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetOfPointsConverter : MonoBehaviour {

    [Header("点に使うモデル")]
    [SerializeField]
    private GameObject _pointsModel;

    [Header("一つ一つの点のサイズ")]
    [SerializeField]
    private Vector3 _pointsScale;

    private List<Vector3> _vertices;

    public List<Vector3> Vertices
    {
        get
        {
            return _vertices;
        }
    }

    private Vector3 _scale;

    private Vector3 _generatePos;

    private Quaternion _rotation;

	void Start () {
        _vertices = GetVertices();
        _scale = GetScale();
        _generatePos = GetPosition();
        _rotation = GetRotation();
        GeneratePoints(_vertices, _generatePos, _rotation, _scale);
	}
	
	void Update () {
	
	}

    /// <summary>
    /// Meshの頂点を取得
    /// </summary>
   
    List<Vector3> GetVertices()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        List<Vector3> vertices = new List<Vector3>();
        vertices.AddRange(mf.mesh.vertices);

        gameObject.GetComponent<MeshRenderer>().enabled = false;

        return vertices;
    }

    Vector3 GetScale()
    {
        return gameObject.transform.localScale;
    }

    Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    Quaternion GetRotation()
    {
        return gameObject.transform.rotation;
    }

    /// <summary>
    /// 点集合生成
    /// </summary>

    void GeneratePoints(List<Vector3> vertices, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        var points = new GameObject("Points");

        foreach(Vector3 verticePoint in vertices)
        {
            var point = Instantiate(_pointsModel, Vector3.zero, Quaternion.identity) as GameObject;
            point.transform.localScale = _pointsScale;
            point.transform.position = verticePoint;
            point.transform.parent = points.transform;
        }

        points.transform.position = pos;
        points.transform.rotation = rot;
        points.transform.localScale = scale;
    }
}
