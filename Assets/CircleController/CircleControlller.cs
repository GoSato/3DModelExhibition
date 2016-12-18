using UnityEngine;
using System.Collections;

//CircularDispositionを継承したクラス。主にコチラは他のオブジェクトを中心に円形配置も可能だが（親子関係配置　ただのInstantiate）
//このスクリプト内にオブジェクトのステータス（オブジェクト、中心角の角度、位置情報）を保持しており、それらを元に操作をする。
//Classをオブジェクトとして扱うことを意識する

//生成する円形配置オブジェクトのステータスを保持する(配列で操作するのが望ましい)
//status一つに付き、一個の円形配置されたGameObjectが対応し、変数の一つにその参照先も保持している
struct status {
	public float radian;		//そのオブジェクトのラジアンを代入
	public GameObject _object;	//生成したオブジェクトの参照を持つ
	//持っている角度を渡す
	public float ToAngle() {
		return radian * Mathf.Rad2Deg;
	}
}

public class CircleControlller : MonoBehaviour {
	
	private status[] circle;	//円形配置のオブジェクト情報を持つステータス
	private Vector3 position;	//中心座標
	private Transform parent;	//このオブジェクトのTransformのキャッシュ

	public int amount;			//オブジェクトの生成数
	public float size;			//円形配置の半径
	public GameObject product;	//生成するオブジェクト
	//z軸を半径かy軸を半経にして円形配置を決定する
	public enum direction
	{
		Horizontal,	//水平に生成
		Vertical	//縦方向に生成
	}

	public direction selectDirection;
	//trueの時自動的に再計算が行われる
	public bool autoRecalculation = false;
	public bool isChild;			//trueならこの円形配置オブジェクトは子である。

	void Start () {
		parent = this.gameObject.transform;
		position = this.gameObject.transform.position;
	}

	void Update () {
		//Vector3は構造体のため同期処理
		position = this.transform.position;

		if(autoRecalculation){
			RadianRecalculation();
		}
	}

	/*円形配置オブジェクトを生成する関数群/////////////////////////////////////////////////////////////*/
	//円形にオブジェクトを配置する。中心はこのコンポーネントを持つインスタンス（GameObject）とする
	//distance(距離)number(数)_object(生成するオブジェクト)でオブジェクトを生成する
	public void CircleObjectGenerate (float distance, int number, GameObject _object) {
		//引数を元にこのコンポーネントのフィールドを設定
		size = distance;
		amount = number;
		product = _object;
		position = this.gameObject.transform.position;

		//null判定
		if(circle != null){
			Debug.LogWarning("Has been generated object!!");
		}else if(amount < 0){
			Debug.LogWarning("Can't generated object!!");
		}else{
			
			//ステータス変数を作成
			circle = new status[amount];
			//位置座標を入手
			Vector3[] array = GetPosition(position);
			//オブジェクトの生成とこのオブジェクトを子供化、ステータスに位置座標と生成物の参照先を持たせる
			for(int i=0; i<amount; i++){
				circle[i]._object = Instantiate(product, array[i], Quaternion.identity)as GameObject;
				circle[i]._object.transform.position = array[i];
				circle[i]._object.transform.parent = this.gameObject.transform;
			}
			//これは子オブジェクトであるか？
			isChild = true;
		}
		RadianRecalculation();
	}

	//引数なしで生成される
	public void CircleObjectGenerate() {
		//null判定
		if(size == null || amount == null || product == null){
			Debug.LogWarning("Status was set null!!");
		}else if(circle != null){
			Debug.LogWarning("Has been generated object!!");
		}else if(amount < 0){
			Debug.LogWarning("Can't generated object!!");
		}else{
			//ステータス変数を作成
			circle = new status[amount];
			//位置座標を入手
			Vector3[] array = GetPosition(position);
			//オブジェクトの生成とこのオブジェクトを子供化、ステータスに位置座標と生成物の参照先を持たせる
			for(int i=0; i<amount; i++){
				circle[i]._object = Instantiate(product, array[i], Quaternion.identity) as GameObject;
				circle[i]._object.transform.position = array[i];
				circle[i]._object.transform.parent = this.gameObject.transform;
			}
			//これは子オブジェクトであるか？
			isChild = true;
		}
		RadianRecalculation();
	}

	//GameObjectなしで生成される
	public void CircleObjectGenerate (float distance, int number){
		//null判定
		if(size == null || amount == null){
			Debug.LogWarning("Status was set null!!",gameObject);
		}else if(circle != null){
			Debug.LogWarning("Has been generated object!!");
		}else if(amount < 0){
			Debug.LogWarning("Can't generated object!!");
		}else{
			//ステータス変数を作成
			circle = new status[amount];
			//位置座標を入手
			Vector3[] array = GetPosition(position);
			//オブジェクトの生成とこのオブジェクトを子供化、ステータスに位置座標と生成物の参照先を持たせる
			for(int i=0; i<amount; i++){
				circle[i]._object = Instantiate(product, array[i], Quaternion.identity)as GameObject;
				circle[i]._object.transform.position = array[i];
				circle[i]._object.transform.parent = parent;
			}
			//これは子オブジェクトであるか？
			isChild = true;
		}
		RadianRecalculation();
	}

	//円形配置オブジェクトをこのオブジェクトを親としないで、生成する
	public void CircleObjectGenerateNotChild (float distance, int number, GameObject _object) {
		//引数を元にこのコンポーネントのフィールドを設定
		size = distance;
		amount = number;
		product = _object;
		position = this.gameObject.transform.position;
		
		//null判定
		if(circle != null){
			Debug.LogWarning("Has been generated object!!");
		}else{
			
			//ステータス変数を作成
			circle = new status[amount];
			//位置座標を入手
			Vector3[] array = GetPosition(position);
			//オブジェクトの生成とこのオブジェクトをステータスに位置座標と生成物の参照先を持たせる
			for(int i=0; i<amount; i++){
				circle[i]._object = Instantiate(product, array[i], Quaternion.identity)as GameObject;
				circle[i]._object.transform.position = array[i];
			}
			//これは子オブジェクトであるか？
			isChild = false;
		}
	}

	//引数なしで生成する（親子なし）
	public void CircleObjectGenerateNotChild() {
		//null判定
		if(size == null || amount == null || product == null){
			Debug.LogWarning("Status was set null!!" ,gameObject);
		}else if(circle != null){
			Debug.LogWarning("Has been generated object!!");
		}else if(amount < 0){
			Debug.LogWarning("Can't generated object!!");
		}else{
			//ステータス変数を作成
			circle = new status[amount];
			//位置座標を入手
			Vector3[] array = GetPosition(position);
			//オブジェクトの生成とこのオブジェクトを子供化、ステータスに位置座標と生成物の参照先を持たせる
			for(int i=0; i<amount; i++){
				circle[i]._object = Instantiate(product, array[i], Quaternion.identity) as GameObject;
				circle[i]._object.transform.position = array[i];
				circle[i]._object.transform.parent = null;
			}
			//これは子オブジェクトであるか？
			isChild = false;
		}
	}
	
	//GameObjectの指定なしで生成する(親子関係なし)
	public void CircleObjectGenerateNotChild (float distance, int number){
		//null判定
		if(size == null || amount == null){
			Debug.LogWarning("Status was set null!!",gameObject);
		}else if(circle != null){
			Debug.LogWarning("Has been generated object!!");
		}else if(amount < 0){
			Debug.LogWarning("Can't generated object!!");
		}else{
			//ステータス変数を作成
			circle = new status[amount];
			//位置座標を入手
			Vector3[] array = GetPosition(position);
			//オブジェクトの生成とこのオブジェクトを子供化、ステータスに位置座標と生成物の参照先を持たせる
			for(int i=0; i<amount; i++){
				circle[i]._object = Instantiate(product, array[i], Quaternion.identity)as GameObject;
				circle[i]._object.transform.position = array[i];
				circle[i]._object.transform.parent = null;
			}
			//これは子オブジェクトであるか？
			isChild = false;
		}
	}

	//以下CircleObjectを操作するための関数群。動作させるために最低でもcircle変数に何らかの値が代入されている必要がある

	//Bigger・Smaller・Rotate・Move関数は毎フレーム実行する際はTime.deltatimeを乗算すること
	//CircleObjectの半径を拡大しサイズを大きくする。関数によって大きくするか小さくするかを分け引数に速度（一回の処理で大きくなる量を設定する）
	public void CircleObjectGetBigger(float speed){
		size += speed;
		RadianRecalculation();
	}

	public void CircleObjectGetSmaller (float speed) {
		size -= speed;
		RadianRecalculation();
	}

	//CircleObjectを回転させる。速度（一回の処理で回る量）の正負で回転する向きを変える
	public void CircleObjectRotate (float speed){
		float a;
		a = Mathf.PI/180 * speed;
		for (int i=0; i<circle.Length; i++){
			circle[i].radian += a;
			circle[i].radian = JudgeRadian(circle[i].radian);
		}
		RadianRecalculation();
	}

	//CircleObjectの中心（インスタンス）と、インスタンスの持つCircleたちを移動する、引数に移動する量と方向を持ったVector3を設定する
	public void CircleObjectMove (Vector3 i){
		this.transform.position += i;
		RadianRecalculation();
	}

	//CircleObjectを指定した座標に移動する（このインスタンスが持つcircleたちも移動される）
	public void CircleObjectPosition(Vector3 i){
		this.transform.position = i;
		RadianRecalculation();
	}

	/*以下CircleObjectを操作するための補助的な関数**************************************************************************************************************/
	//主に、円形配置操作を行うときの方向を入手出来る関数。実質的な移動処理は別で実装してもらう
	//使いどころとしては、これまでの円形配置は直にTransformをいじっているため、他のコンポーネントで円形操作するときに地味に重要
	//RigidbodyやCharacterControllerを使った円形配置操作にどうぞ
	//CircleObjectを回転させる際の方向ベクトルを入手するための関数
	//引数は度数法における加算された角度の方向を返す
	public Vector3[] GetCircleRotate (float speed){
		if(circle.Length != amount){
			RadianRecalculation();
		}
		float a;
		a = Mathf.PI/180 * speed;
		
		//キャッシュ
		Vector3[] before;
		before = new Vector3[amount];
		//合成後
		Vector3[] after;
		after = new Vector3[amount];
		//その差分
		Vector3[] vector;
		vector = new Vector3[amount];
		
		//角度を合成
		for(int i=0; i<circle.Length; i++){
			before[i] = circle[i]._object.transform.position;
			circle[i].radian += a;
			circle[i].radian = JudgeRadian(circle[i].radian);
		}
		//再計算
		after = RadianRecalcationNotChangePosition();
		
		for (int i=0; i<circle.Length; i++){
			vector[i] = after[i] - before[i];
		}
		return vector;
	}

	//コチラはBiggerの亜種版。サイズを縁の半径を大きくする時にその方向を入手する
	public Vector3[] GetCircleChangeSize (float speed){

		if(circle.Length != amount){
			RadianRecalculation();
		}

		size += speed;
		//キャッシュ
		Vector3[] before;
		before = new Vector3[amount];
		//合成後
		Vector3[] after;
		after = new Vector3[amount];
		//その方向(差分)
		Vector3[] vector;
		vector = new Vector3[amount];

		for(int i=0; i<circle.Length; i++){
			before[i] = circle[i]._object.transform.position;
		}
		after = RadianRecalcationNotChangePosition();

		for(int i=0; i<circle.Length; i++){
			vector[i] = after[i] - before[i];
		}

		return vector;
	}

	/*以下CircleObjectを操作する上での必要な補助関数**********************************************************************************************************/
	//CircleObjectたちを変数circleが持つラジアンを確認して、位置を再計算する
	//amountの値を更新した際はオブジェクトは再生成される
	public void RadianRecalculation() {
		//中心を再代入
		position = this.transform.position;

		if(circle != null){
			if(amount < 0){
				Debug.LogWarning("Can't generated object!!");
			}else{
				if(circle.Length != amount){
					CircleObjectRegeneration();
				}else{
					//ラジアン再確認し、各フィールド値を用いて新しい位置座標をステータスに代入
					for (int i=0; i<circle.Length; i++){
						float newX;
						float newY;
						float newZ;
						
						//選択によって分ける
						switch(selectDirection){
						case direction.Horizontal:
							newX = (Mathf.Cos(circle[i].radian) * size + position.x);
							newZ = (Mathf.Sin(circle[i].radian) * size + position.z);
							circle[i]._object.transform.position = new Vector3(newX, position.y, newZ);
							break;
							
						case direction.Vertical:
							newX = (Mathf.Cos(circle[i].radian) * size + position.x);
							newY = (Mathf.Sin(circle[i].radian) * size + position.y);
							circle[i]._object.transform.position = new Vector3(newX, newY, position.z);
							break;
						}
					}
				}
			}
		}else{
			Debug.LogWarning("Not generated object!!");
		}
	}

	//ラジアンを元に再計算された位置座標配列を戻り値とする関数
	//円形操作オブジェクトに位置座標を代入はしないので、ただの計算式ですな
	private Vector3[] RadianRecalcationNotChangePosition(){

		Vector3[] array;
		array = new Vector3[amount];

		for(int i=0; i<amount; i++){
			float newX;
			float newY;
			float newZ;

			switch(selectDirection){
			case direction.Horizontal:
				newX = (Mathf.Cos(circle[i].radian) * size + position.x);
				newZ = (Mathf.Sin(circle[i].radian) * size + position.z);
				array[i] = new Vector3(newX, position.y, newZ);
				break;

			case direction.Vertical:
				newX = (Mathf.Cos(circle[i].radian) * size + position.x);
				newY = (Mathf.Sin(circle[i].radian) * size + position.y);
				array[i] = new Vector3(newX, newY, position.z);
				break;
			}
		}
		return array;
	}

	//productの参照先を更新した際にCircleObjectたちを生成し直します。よって必然的にステータスもフィールドに合わせて更新されます
	public void CircleObjectRegeneration(){
		CircleObjectDelete();
		if(isChild){
			CircleObjectGenerate();
		}else{
			CircleObjectGenerateNotChild();
		}
	}

	//以下CircleObjectたちのステータスを取得あるいはセットする
	//CircleObjectたちの参照先を渡す
	public GameObject[] GetCircleObjects(){
		GameObject[] array;
		array = new GameObject[circle.Length];

		for(int i=0; i<circle.Length; i++){
			array[i] = circle[i]._object;
		}

		return array;
	}

	//CircleObjectたちのラジアン配列を渡す
	public float[] GetRadian() {
		float[] array;
		array = new float[circle.Length];

		for (int i=0; i<circle.Length; i++){
			array[i] = circle[i].radian;
		}

		return array;
	}

	//Circle配列の長さを返す
	public int CircleAmount(){
		return circle.Length;
	}

	//CircleObjectは生成済みか？真ならtrueを返す
	public bool CircleObjectWasGenerated(){
		bool a;
		if(circle != null){
			a = true;
		}else{
			a = false;
		}
		return a;
	}

	//CircleObjectたちのラジアン配列をセットする（この処理の後に上記の再計算処理を行うことを推奨する）
	//judgeフラグがtrueの時ラジアンは0<=θ<2πの範囲になって代入される
	public void SetRadian(float[] f, bool judge) {
		float[] array;
		array = f;
		for(int i=0; i<circle.Length; i++){
			if(judge){
				circle[i].radian = JudgeRadian(array[i]);
			}else{
				circle[i].radian = array[i];
			}
		}
	}

	//サークルオブジェクトを削除し、保持するcircle変数を消去する
	public void CircleObjectDelete() {
		if(size == null || amount == null || product == null){
			Debug.LogWarning("Status was set null!!" ,gameObject);
		}

		for(int i=0; i<circle.Length; i++){
			Destroy(circle[i]._object.gameObject);
		}
		circle = null;
	}

	//このクラス内で使用、Vector3で円形に配置された位置座標を返す
	//center（中心座標）を引数とする
	//この関数内で、コンポーネント内のラジアンステータスを設定する
	private Vector3[] GetPosition (Vector3 center){
		Vector3[] array;	//戻り値として渡す変数を用意
		array = new Vector3[amount];

		for (int i=0; i<array.Length; i++){
			float _centerRadian;//中心角
			_centerRadian = Mathf.PI * 2 / array.Length * i;	//中心角を求める
			//中心角をステータス変数内に代入
			circle[i].radian = JudgeRadian(_centerRadian);

			float x;
			float y;
			float z;

			//縦横の方向を代入し、ステータスによって変更する
			switch(selectDirection){
			case direction.Horizontal:
				//中心座標を求めるための三角関数演算を開始
				x = Mathf.Cos(_centerRadian);	//x座標
				z = Mathf.Sin(_centerRadian);	//y座標
				array[i] = (new Vector3(center.x+x, center.y, center.z+z) * size);
				break;
			case direction.Vertical:
				x = Mathf.Cos(_centerRadian);
				y = Mathf.Sin(_centerRadian);
				array[i] = (new Vector3(center.x+x, center.y+y, center.z) * size);
				break;
			}
		}
		return array;
	}

	//ラジアンを代入するときに、ラジアンが正常な範囲内か判定をする
	private float JudgeRadian (float i){
		//ラジアンを0<=θ<2πの範囲で作成
		while(i< 0f || i >= 2*Mathf.PI){
			while(i<0f){
				//ラジアンが0より小さい時に2nπを合成し0以上にする
				i+= 2*Mathf.PI;
			}
			
			while(i>= 2*Mathf.PI){
				//ラジアンが2nπ以上の時、減算をし範囲内に収める
				i -= 2*Mathf.PI;
			}
		}
		//判定終了したものを返す
		return i;
	}
}