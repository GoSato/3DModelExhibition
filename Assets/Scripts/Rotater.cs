using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

    [SerializeField]
    private float _speed;

    private void Update()
    {
        gameObject.transform.Rotate(0, _speed * Time.deltaTime, 0);
    }
}
