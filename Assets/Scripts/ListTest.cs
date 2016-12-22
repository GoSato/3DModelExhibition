using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListTest : MonoBehaviour {

	void Start () {
        List<int> list = new List<int>();
        list.Add(5);
        list.Add(3);

        // メモリー確保するのでUpdate内では使わない

        foreach(var num in list)
        {
            Debug.Log(num);
        }

        // 手動でforeachを展開する

        var enumerator = list.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var pair = enumerator.Current;

            Debug.Log(pair);
        }
	}
}
