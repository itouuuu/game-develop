using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MagicTrap : WizardPlayerStatus
{
    //罠の設置者ID
    //private string magicTrapID;
    private SphereCollider sphereCol;


    void Start()
    {
        
        sphereCol = GetComponent<SphereCollider>();
        InitializeMagicTrapParameters();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //罠生成時に呼び出され初期化を行う
    private void InitializeMagicTrapParameters(){
        sphereCol.radius = GetMagicTrapDetectionRadius();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Debug.Log("プレイヤーが探知範囲に入りました");
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "MagicAttack(Clone)" && Vector3.Distance(other.transform.position, this.transform.position) <= 1.0f) {
            // 衝突したら即爆発する
            ExplosionMagicTrap();
        }
    }

    //爆発時に呼ばれる関数
    private void ExplosionMagicTrap(){
        GameObject IMagicExplosion = PhotonNetwork.Instantiate("Prefabs/MagicExplosion", this.transform.position, Quaternion.identity);
        DeleteMagicTrap();
    }

    private void DeleteMagicTrap(){
        Destroy(this.gameObject);
    }
}
