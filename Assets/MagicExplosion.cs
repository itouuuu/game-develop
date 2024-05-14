using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MagicExplosion : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        //InitializeMagicExplosionParameters();
        StartCoroutine(ExplodeMagicTrap());
    }

    //生成時に呼び出され初期化を行う
    public void SetMagicExplosionRadius(float magicTrapExplosionRadius){
        this.transform.localScale = new Vector3(magicTrapExplosionRadius, magicTrapExplosionRadius, magicTrapExplosionRadius);
    }

    //爆発を少し残しその後破棄する関数
    private IEnumerator ExplodeMagicTrap()
	{
        
        //少し待機する。
        yield return new WaitForSeconds(1.0f);
        DeleteMagicExplosion();
	}


    private void DeleteMagicExplosion(){
        Debug.Log("爆発は消えました");
        Destroy(this.gameObject);
    }
}
