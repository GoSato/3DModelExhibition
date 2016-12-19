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

    private struct BaseInfo
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 scale;
    }

    void Start() {
        var vertices = GetVertices();
        var baseInfo = GetBaseInfo();
        GeneratePoints(vertices, baseInfo);
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

    /// <summary>
    /// 点集合の基となるモデルの情報を取得
    /// </summary>
    /// <returns></returns>

    BaseInfo GetBaseInfo()
    {
        BaseInfo info = new BaseInfo();

        info.pos = gameObject.transform.position;
        info.rot = gameObject.transform.rotation;
        info.scale = gameObject.transform.localScale;

        return info;
    }

    /// <summary>
    /// 点集合生成
    /// </summary>

    void GeneratePoints(List<Vector3> vertices, BaseInfo info)
    {
        var points = new GameObject("Points");

        foreach(Vector3 verticePoint in vertices)
        {
            var point = Instantiate(_pointsModel, Vector3.zero, Quaternion.identity) as GameObject;
            point.transform.localScale = _pointsScale;
            point.transform.position = verticePoint;
            point.transform.rotation = _pointsModel.transform.rotation;
            point.transform.parent = points.transform;
        }

        points.transform.position = info.pos;
        points.transform.rotation = info.rot;
        points.transform.localScale = info.scale;
    }
}
