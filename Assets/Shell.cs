using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    //着弾地点
    public Vector3 impactPosition = Vector3.zero;
    //反射回数
    private int  magicAttackReflectNum = 0;
    //砲弾の速度
    private float magicAttackSpeed = 0.0f;
    //正規化した砲弾が進む方向
    private Vector3 normShellVelocity;
    void Start()
    {
    }

    void Update()
    {
        this.transform.position += (normShellVelocity * magicAttackSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "MagicAttack(Clone)") {
            // 衝突したら弾を削除する。
            DestroyMagicAttack();
        }
        else if (other.gameObject.name == "PlayerWizard(Clone)") {
            // 衝突したら弾を削除する。
            DestroyMagicAttack();
        }
        else if (other.gameObject.name == "Wall") {
            //反射を行う処理。
            Reflect(other);
        }
    }

    //魔法弾生成時に呼び出され速度と反射回数が与えられる
    public void SetInitialMagicAttackParameters(float speed,int reflectNum){
        magicAttackSpeed = speed;
        magicAttackReflectNum = reflectNum;
    }


    //着弾地点のセットとその方向へのベクトルの計算
    public void SetImpactPosition(Vector3 setPosition){
        impactPosition = setPosition;
        //砲弾の進む方向を正規化し計算する(移動速度に影響)
        normShellVelocity = (impactPosition - this.transform.position).normalized;
        //砲弾の向きを変える
        transform.LookAt(impactPosition);
    }

    //壁に反射した際に呼ばれる関数
    void Reflect(Collision wall){
        if(magicAttackReflectNum <= 0){
            //砲弾の終了
            DestroyMagicAttack();
        }
        magicAttackReflectNum--;

        // Triggerで接触したオブジェクトは
        // 全てボールとみなすことにする
        var rb = this.GetComponent<Rigidbody>();
        if (rb == null) return;
        
        //魔法弾の入射ベクトル（速度）
        var inDirection = normShellVelocity;
        //壁の法線ベクトル
        var inNormal = wall.contacts[0].normal;
        //魔法弾の反射ベクトル（速度）
        var result = Vector3.Reflect(inDirection, inNormal);
        //着弾地点の設定
        SetImpactPosition(result + this.transform.position);

    }

    //弾を消す際に呼ばれる関数
    void DestroyMagicAttack(){
        //あとで消えるときの音とかを入れる
        Destroy(this.gameObject);
    }
}
