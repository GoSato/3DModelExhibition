using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExhibitionManager : MonoBehaviour {

    /// <summary>
    /// モデルの管理を行う
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

    void Start () {
        var exhibitionLocator = SetExhibitionLocator();
        exhibitionLocator.Initialize();
	}
	
	void Update () {
	
	}

    ExhibitionController SetExhibitionLocator()
    {
        GameObject locator = new GameObject("ExhibitionLocator");
        locator.transform.position = _centerObj.transform.position;
        var locatorComponent = locator.AddComponent<ExhibitionController>();
        locatorComponent.ModelList = _modelList;
        return locatorComponent;
    }
}
