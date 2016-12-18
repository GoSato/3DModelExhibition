using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExhibitionManager : MonoBehaviour {

    /// <summary>
    /// モデルの管理、ExhibitionControllerの生成を行う
    /// </summary>

    [Header("モデルリスト")]
    [SerializeField]
    private List<GameObject> _modelList;

    public List<GameObject> ModelList
    {
        get
        {
            return _modelList;
        }
    }

    [Header("中心のオブジェクト")]
    [SerializeField]
    private GameObject _centerObj;

    [Header("配置する円の半径")]
    [SerializeField]
    private float _radius;

    [Header("円の回転")]
    [SerializeField]
    private bool _isRotate;

    [Header("円の回転するスピード")]
    [SerializeField]
    private float _speed;

    void Start () {
        var exhibitionLocator = SetExhibitionLocator();
        exhibitionLocator.Initialize();
	}

    /// <summary>
    /// ExhibitionControllerの生成
    /// </summary>
    ExhibitionController SetExhibitionLocator()
    {
        GameObject locator = new GameObject("ExhibitionLocator");
        locator.transform.position = _centerObj.transform.position;
        var locatorComponent = locator.AddComponent<ExhibitionController>();
        locatorComponent.ModelList = _modelList;
        locatorComponent.Radius = _radius;
        locatorComponent.IsRotate = _isRotate;
        locatorComponent.Speed = _speed;
        return locatorComponent;
    }
}
