using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MagicExplosion : MagicTrap
{

    // Start is called before the first frame update
    void Start()
    {
        //InitializeMagicExplosionParameters();
        StartCoroutine(BlinkMagicTrap());
    }

    //生成時に呼び出され初期化を行う
    public void SetMagicExplosionRadius(float magicTrapExplosionRadius){
        this.transform.localScale = new Vector3(magicTrapExplosionRadius, magicTrapExplosionRadius, magicTrapExplosionRadius);
    }

    //一定間隔で点滅を行うコルーチン
    private IEnumerator BlinkMagicTrap()
	{
        //少し待機する。
        yield return new WaitForSeconds(1.0f);
        DeleteMagicExplosion();
		yield break;
	}


    private void DeleteMagicExplosion(){
        Destroy(this.gameObject);
    }
}
