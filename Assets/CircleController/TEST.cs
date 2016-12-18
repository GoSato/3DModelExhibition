using UnityEngine;
using System.Collections;

//CharcterControllerを使って円形配列の回転を行う
public class TEST : MonoBehaviour
{

    public CircleControlller circleController;  //circlecontroller
    public float speed;                 //回転速度
    public GameObject[] circleObjects;          //円形配置オブジェクトの参照先代入
    public Vector3[] moveDirection;             //円形配置オブジェクトの移動する方向を持つ

    void Start()
    {
        //circlecontrollerを代入
        circleController = this.gameObject.GetComponent<CircleControlller>();
        //circleオブジェクトを生成する
        circleController.CircleObjectGenerate();
        //各パラメーターを設定
        circleController.amount = 10;
        circleController.size = 20f;
        speed = 100f;
        circleController.autoRecalculation = false;
    }
}