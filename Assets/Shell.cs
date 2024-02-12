using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private CharacterController playerController;
    //着弾地点
    public Vector3 impactPosition = Vector3.zero;
    //反射回数
    private int  reflectNum = 0;
    //砲弾の速度
    private float shellSpeed = 1.0f;
    //正規化した砲弾が進む方向
    private Vector3 normShellVelocity;
    void Start()
    {
        playerController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        playerController.Move(normShellVelocity * shellSpeed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
            Debug.Log(other.gameObject.name +"enter");
        if (other.gameObject.name != "Shell(Clone)") {
            // 衝突した相手オブジェクトを削除する
            //Destroy(other.gameObject);
            //Destroy(this.gameObject);
        }   
    }

    void OnTriggerExit(Collider other)
    {
            Debug.Log(other.gameObject.name +"exit");
        if (other.gameObject.name != "Shell(Clone)") {
            // 衝突した相手オブジェクトを削除する
            //Destroy(other.gameObject);
            //Destroy(this.gameObject);
        }   
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
    void Reflect(){
        if(reflectNum > 0){
            //砲弾の終了
        }
        reflectNum--;

    }

    //弾を消す際に呼ばれる関数
    void Destro(){
        Destroy(this.gameObject);
    }
}
