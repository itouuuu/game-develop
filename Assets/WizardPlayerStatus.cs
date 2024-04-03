using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WizardPlayerStatus : MonoBehaviourPunCallbacks
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
    string playerName = "Test";
    //プレイヤーのライフ
    private int hitPoint;
    //プレイヤーの移動速度
    private float moveWizardSpeed = 4.0f;
    //魔法弾の最大存在数
    private int maxMagicAttack = 1;
    //魔法弾の現在存在数
    private int countMagicAttack = 0;
    //魔法弾の速度
    private float magicAttackSpeed = 8.0f;
    //魔法弾の反射回数
    private int magicAttackReflectNum = 2;
    //罠の最大設置数
    private int maxMagicTrap = 1;
    //罠の現在設置数
    private int countMagicTrap = 0;
    //罠の爆発半径
    private float magicTrapExplosionRadius = 2;
    //罠の探知半径
    private float magicTrapDetectionRadius = 2;
    //罠の設置者(id?)
    private string magicTrapID = "";

    //プレイヤー名
    public void SetPlayerName(string newPlayerName){
        this.playerName = newPlayerName;
    }
    public string GetPlayerName(){
        return playerName;
    }

    //ヒットポイント
    public void SetHitPoint(int newHitPoint){
        this.hitPoint = newHitPoint;
    }
    public int GetHitPoint(){
        return hitPoint;
    }

    //プレイヤーの移動速度
    public void SetMoveWizardSpeed(int newMoveWizardSpeed){
        this.moveWizardSpeed = newMoveWizardSpeed;
    }
    public float GetMoveWizardSpeed(){
        return moveWizardSpeed;
    }

    //魔法弾
    public void SetMaxMagicAttack(int newMaxMagicAttack){
        this.maxMagicAttack = newMaxMagicAttack;
    }
    public int GetMaxMagicAttack(){
        return maxMagicAttack;
    }

    public void SetCountMagicAttack(int newCountMagicAttack){
        this.countMagicAttack = newCountMagicAttack;
    }
    public int GetCountMagicAttack(){
        return countMagicAttack;
    }

    public void SetMagicAttackSpeed(int newMagicAttackSpeed){
        this.magicAttackSpeed = newMagicAttackSpeed;
    }
    public float GetMagicAttackSpeed(){
        return magicAttackSpeed;
    }

    public void SetMagicAttackReflectNum(int newMagicAttackReflectNum){
        this.magicAttackReflectNum = newMagicAttackReflectNum;
    }
    public int GetMagicAttackReflectNum(){
        return magicAttackReflectNum;
    }

    //罠
    public void SetMaxMagicTrap(int newMaxMagicTrap){
        this.maxMagicTrap = newMaxMagicTrap;
    }
    public int GetMaxMagicTrap(){
        return maxMagicTrap;
    }

    public void SetCountMagicTrap(int newCountMagicTrap){
        this.countMagicTrap = newCountMagicTrap;
    }
    public int GetCountMagicTrap(){
        return countMagicTrap;
    }

    public void SetMagicTrapExplosionRadius(float newMagicTrapExplosionRadius){
        this.magicTrapExplosionRadius = newMagicTrapExplosionRadius;
    }
    public float GetMagicTrapExplosionRadius(){
        return magicTrapExplosionRadius;
    }

    public void SetMagicTrapDetectionRadius(float newMagicTrapDetectionRadius){
        this.magicTrapDetectionRadius = newMagicTrapDetectionRadius;
    }
    public float GetMagicTrapDetectionRadius(){
        return magicTrapDetectionRadius;
        
    }


}
