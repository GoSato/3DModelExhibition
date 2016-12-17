using UnityEngine;
using System.Collections;

public class Exhibition : MonoBehaviour {

    [SerializeField]
    private float _speed = 1;

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * _speed, 0),Space.World);
    }
}
