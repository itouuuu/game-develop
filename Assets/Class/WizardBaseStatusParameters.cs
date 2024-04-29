/*
すべてのプレイヤーの基礎ステータスとなる値などが書いてある。
これらの値は関数GetOO,によって取得できる。
このファイル内の値は変更できないようにする。
ギアによる能力の変化はここではなくWizardParameterの方で行う。
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WizardBaseStatusParameters : MonoBehaviourPunCallbacks
{
    /*
    Standby:ゲーム開始前
    Normal:通常の状態
    Invisible:透明ギア装備時の状態
    Damaged:被弾状態
    GameOver:撃破された状態
    */
    public enum CHARACTERSTATE{
        Standby,
        Normal,
        Invisible,
        Damaged,
        GameOver
    }
    //プレイヤーネーム
    private string playerName = "Test";
    //プレイヤーのライフ
    private int baseStatus_maxHitPoint = 3;
    //プレイヤーの移動速度
    private float baseStatus_moveWizardSpeed = 4.0f;
    //魔法弾の最大存在数
    private int baseStatus_maxMagicAttack = 2;
    //魔法弾の速度
    private float baseStatus_magicAttackSpeed = 8.0f;
    //魔法弾の反射回数
    private int baseStatus_magicAttackReflectNum = 1;
    //罠の最大設置数
    private int baseStatus_maxMagicTrap = 1;
    //罠の爆発半径
    private float baseStatus_magicTrapExplosionRadius = 2;
    //罠の探知半径
    private float baseStatus_magicTrapDetectionRadius = 2;
    //罠の設置者(id?)
    private string magicTrapID = "";


    //プレイヤー関連
    //プレイヤー名
    public void SetPlayerName(string newPlayerName){
        this.playerName = newPlayerName;
    }
    public string GetPlayerName(){
        return playerName;
    }

    //ヒットポイント
    public int GetMaxHitPoint(){
        return baseStatus_maxHitPoint;
    }

    //プレイヤーの移動速度
    public float GetMoveWizardSpeed(){
        return baseStatus_moveWizardSpeed;
    }

    //魔法弾関連
    //最大魔法弾数
    public int GetMaxMagicAttack(){
        return baseStatus_maxMagicAttack;
    }


    //魔法弾の速度
    public float GetMagicAttackSpeed(){
        return baseStatus_magicAttackSpeed;
    }

    //魔法弾の最大反射回数
    public int GetMagicAttackReflectNum(){
        return baseStatus_magicAttackReflectNum;
    }

    //罠関連
    //罠の最大設置数
    public int GetMaxMagicTrap(){
        return baseStatus_maxMagicTrap;
    }

    //罠の爆発距離半径
    public float GetMagicTrapExplosionRadius(){
        return baseStatus_magicTrapExplosionRadius;
    }

    //罠の探知距離半径
    public float GetMagicTrapDetectionRadius(){
        return baseStatus_magicTrapDetectionRadius;
        
    }


}
