using UnityEngine;
using System.Collections;


/*
 * このクラスは、円形に指定したオブジェクトを指定した数だけ生成できる。ただし生成物を操作することは出来ないため、こいつらを継承した子クラスを作る
 * */

public class Circulardisposition : MonoBehaviour {

	public GameObject productObject;

	//指定したGameObjectの子として、円形配列で生成する。ただし、戻り値にGameObject[]が来ることに注意
	//第一引数：親となるGameObject	のTransform　第二引数：中心からの距離　第三引数：生成するオブジェクトの数　第四引数：生成するオブジェクト
	public GameObject[] ParentCircleObjectProduct(Transform parent, float distance, int number, GameObject product){
		Vector3[] position;	//円形配置するオブジェクトたちの位置座標配列
		GameObject[] productObjects;
		productObjects = new GameObject[number];

		position = GetPosition(parent.transform.position, distance, number);
		//オブジェクトを生成し、parentオブジェクトの子とする。
		for(int i=0; i<number; i++){
			productObjects[i] = Instantiate(product, position[i], Quaternion.identity) as GameObject;
			productObjects[i].gameObject.transform.parent = parent;
		}

		return productObjects;
	}
	//上の関数の親を指定しない版。この場合、このGameObjectが親となる。
	public GameObject[] ParentCircleObjectProduct(float distance, int number, GameObject product){
		Vector3[] position;			//円形配置するオブジェクトたちの位置座標配列
		GameObject[] productObjects;//円形配置のオブジェクト配列
		productObjects = new GameObject[number];
		
		position = GetPosition(this.transform.position, distance, number);
		//オブジェクトを生成し、parentオブジェクトの子とする。
		for(int i=0; i<number; i++){
			productObjects[i] = Instantiate(product, position[i], Quaternion.identity) as GameObject;
			productObjects[i].gameObject.transform.parent = this.gameObject.transform;
		}
		return productObjects;
	}

	//下の関数のオーバーロード。GameObjectの指定をしなくても生成できる。（ただし、生成されるのはこの関数に代入されたGameObject）
	public void CircleObjectProduct (Vector3 center, float distance, int number) {
		Vector3[] position;
		position = GetPosition(center, distance, number);
		
		for (int i=0; i<number; i++){
			GameObject.Instantiate(productObject, position[i], Quaternion.identity);
		}
	}


	//円形に配置されたオブジェクトをGameObject配列で返す
	//第一引数：中心座標　第二引数：中心からの距離　第三引数：生成するオブジェクトの数　第四引数：生成するオブジェクト
	public GameObject[] CircleObjectProduct (Vector3 center, float distance, int number, GameObject product) {

		GameObject[] productObjects;				//生成されたオブジェクトの参照先
		productObjects = new GameObject[number];	//指定した数だけの配列を作成
		Vector3[] position;							//オブジェクトの座標はコチラに総括して入れられる

		//関数実行：円形座標が代入される
		position = GetPosition(center, distance, number);

		//ゲームオブジェクトを円形配置で、生成
		for (int i=0; i<number; i++){
			//ゲームオブジェクトを生成して、配列に代入
			productObjects[i] = GameObject.Instantiate(product, position[i], Quaternion.identity) as GameObject;
		}
		return productObjects;
	}


	//位置座標をVector3の配列で返す
	//第一引数：中心座標 第二引数：中心からの距離 第三引数：生成するオブジェクトの数
	private Vector3[] GetPosition (Vector3 center, float distance, int number) {

		Vector3[] array;			//位置座標を代入する配列を宣言
		array = new Vector3[number];//配列を生成

		//Vector3の配列に円形配置の順に
		for(int i=0; i<array.Length; i++){
			float x;	//x座標
			float z;	//z座標
			float sita;	//中心角
			sita = Mathf.PI/array.Length*i*2;			//中心角ラジアンを求める
			x = Mathf.Cos(sita) * distance + center.x;	//x座標を求める
			z = Mathf.Sin(sita) * distance + center.z;	//y座標を求める
			array[i] = new Vector3(x, center.y, z);		//オブジェクトの座標
		}
		return array;
	}
}
