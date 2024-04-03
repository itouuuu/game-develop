using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MagicExplosion : WizardPlayerStatus
{
    


    // Start is called before the first frame update
    void Start()
    {
        InitializeMagicExplosionParameters();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //生成時に呼び出され初期化を行う
    private void InitializeMagicExplosionParameters(){
        float explosionRadius = GetMagicTrapExplosionRadius();
        this.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
    }

    //爆発時に呼ばれる関数
    private void ExplosionMagicTrap(){
        GameObject IMagicExplosion = PhotonNetwork.Instantiate("Prefabs/MagicExplosion", this.transform.position, Quaternion.identity);
        DeleteMagicExplosion();
    }

    private void DeleteMagicExplosion(){
        Destroy(this.gameObject);
    }
}
