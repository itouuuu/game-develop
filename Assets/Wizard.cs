using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : WizardParameter
{
    private Rigidbody charactorRB;
    void Start()
    {
        charactorRB = this.GetComponent<Rigidbody>();
    }

    void Update()
    {   
        //自身のオブジェクトのみ移動させる
        if(photonView.IsMine == true){
            //魔法弾の発射
            ShotMagicAttack(this.transform.position);
            //罠の設置
            SetMagicTrap(this.transform.position);
            charactorRB.velocity = MoveTank() * Time.deltaTime * 100.0f;
	        //this.transform.position += (moveVelocity * Time.deltaTime);
            //マウスカーソルの方に常に体を向ける
            transform.LookAt(MouseCursortoPlanePosition());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "MagicAttack(Clone)") {
            //魔法弾と衝突したら被弾判定を行う。
             OnPlayerDameged();
        } 
    }
}
