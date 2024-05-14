using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MagicTrap : MonoBehaviourPunCallbacks
{
    //罠の設置者ID。
    //private string magicTrapID;
    private float magicTrapExplosionRadius = 0.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageObject" ) {
            //衝突したら即爆発する。
            ExplosionMagicTrap();
        }
        
    }

    //爆発半径を初期化する関数(WizardParameterから呼び出される)。
    public void InitializeMagicTrapExplosionRadius(float setMagicTrapExplosionRadius){
        magicTrapExplosionRadius = setMagicTrapExplosionRadius;
    }

    //爆発時に呼ばれる関数
    public void ExplosionMagicTrap(){
        //爆発のパーティクルなどを呼び出す。
        GameObject IMagicExplosion = PhotonNetwork.Instantiate("Prefabs/MagicExplosion", this.transform.position, Quaternion.identity);
        //爆発半径を実際の当たり判定にセット。
        IMagicExplosion.GetComponent<MagicExplosion>().SetMagicExplosionRadius(magicTrapExplosionRadius);
        //罠の削除。
        DeleteMagicTrap();
    }

    private void DeleteMagicTrap(){
        Destroy(this.gameObject);
        Debug.Log("罠は壊れました");
    }
}
