using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExhibitionController : MonoBehaviour {

    /// <summary>
    /// モデルの配置等を行う
    /// </summary>

    private List<GameObject> _modelList;

    public List<GameObject> ModelList
    {
        set
        {
            _modelList = value;
        }
    }

    private float _radius;

    public float Radius
    {
        set
        {
            _radius = value;
        }
    }

    private bool _isRotate;

    public bool IsRotate
    {
        set
        {
            _isRotate = value;
        }
    }

    private float _speed;

    public float Speed
    {
        set
        {
            _speed = value;
        }
    }

	public void Initialize()
    {
        if(_modelList.Count == 0)
        {
            return;
        }

        var exhibitions = GenerateModel();
        CircleDeploy(exhibitions);
    }

    void Update()
    {
        if (_isRotate)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * _speed, 0), Space.World);
        }
    }

    /// <summary>
    /// モデルリストからオブジェクト生成
    /// </summary>
    List<GameObject> GenerateModel()
    {
        List<GameObject> lists = new List<GameObject>();
        foreach(var model in _modelList)
        {
            var generatedModel = Instantiate(model, transform.position, model.transform.rotation) as GameObject;
            generatedModel.transform.parent = gameObject.transform;
            lists.Add(generatedModel);
        }
        return lists;
    }

    /// <summary>
    /// 等間隔、円形に配置
    /// </summary>
    void CircleDeploy(List<GameObject> exhibitionLists)
    {
        float angleDiff = 360f / exhibitionLists.Count;

        for(int i=0; i<_modelList.Count; i++)
        {
            Vector3 modelPosition = transform.position;
            float angle = (90 - angleDiff * i) * Mathf.Deg2Rad;
            modelPosition.x += _radius * Mathf.Cos(angle);
            modelPosition.z += _radius * Mathf.Sin(angle);
            exhibitionLists[i].transform.position = modelPosition;
        }
    }
}
