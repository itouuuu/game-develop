using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MagicTrap : WizardPlayerStatus
{
    //罠の設置者ID
    //private string magicTrapID;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageObject" ) {
            // 衝突したら即爆発する
            ExplosionMagicTrap();
        }
        
    }

    //爆発時に呼ばれる関数
    public void ExplosionMagicTrap(){
        //爆発のパーティクルなどを呼び出す。
        GameObject IMagicExplosion = PhotonNetwork.Instantiate("Prefabs/MagicExplosion", this.transform.position, Quaternion.identity);
        //罠の削除。
        DeleteMagicTrap();
    }

    private void DeleteMagicTrap(){
        Destroy(this.gameObject);
    }
}
