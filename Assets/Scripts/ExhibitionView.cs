using UnityEngine;
using System.Collections;

public class ExhibitionView : MonoBehaviour {

    /// <summary>
    /// 展示物の挙動を制御
    /// </summary>

    [Header("展示物の回転するスピード")]
    [SerializeField]
    private float _speed;

    [Header("配置位置を補正")]
    [SerializeField]
    private Vector3 _offset;

    void Start()
    {
        gameObject.transform.position += _offset;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * _speed, 0),Space.World);
    }
}
