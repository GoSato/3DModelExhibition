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

	public void Initialize()
    {
        if(_modelList.Count == 0)
        {
            return;
        }
            
        foreach(var model in _modelList)
        {
            Debug.Log(model.gameObject.name);
        }
    }
}
