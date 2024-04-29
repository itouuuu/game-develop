/*
弾を任意の場所に飛ばすた目の処理もここに書く
魔法弾や罠の生成などの関数はここに記載する
ギアによるステータス変更を行った値を保存する


*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public abstract class WizardParameter : WizardBaseStatusParameters
{
    public CHARACTERSTATE characterState = CHARACTERSTATE.Normal;
    //プレイヤーのカメラ
    private Camera mainCamera;
    //ステージの床
    private GameObject land;
    //canvasを
    private GameObject canvas;
    //ライフの表示
    private GameObject[] hitPointMarkArray = new GameObject[5];
    //現在の残りライフ
    private int currentHitPoint = 0;
    //プレイヤーカラー
    private Material[] playerColor = new Material[8];
    //被弾時の無敵時間
    private static float hitInvincibleTime = 3.0f;
    //無敵時間の点滅間隔
    private static float transparentTime = 0.1f;
    //プレイヤーのマテリアル
    private Renderer playerMaterial;

    //プレイヤーのライフなどのステータスパラメータ変数。
        //プレイヤーの最大ライフ
        private int maxHitPoint = 0;
        //プレイヤーの移動速度
        private float moveWizardSpeed = 0.0f;
        //魔法弾の最大存在数
        private int maxMagicAttack = 0;
        //魔法弾の現在存在数
        private int countMagicAttack = 0;
        //魔法弾の速度
        private float magicAttackSpeed = 0.0f;
        //魔法弾の反射回数
        private int magicAttackReflectNum = 0;
        //罠の最大設置数
        private int maxMagicTrap = 0;
        //罠の現在設置数
        private int countMagicTrap = 0;
        //罠の爆発半径
        private float magicTrapExplosionRadius = 0.0f;
        //罠の探知半径
        private float magicTrapDetectionRadius = 0.0f;
    


    private void Awake()
    {
        //地面オブジェクトの取得(もっといい方法がある？)
        land = GameObject.Find("Land");
        //メインカメラの取得
        mainCamera = Camera.main;
        //オブジェクトの色を用意したMaterialの色に変更する
        playerColor[0] = (Material)Resources.Load ("redMaterial");

        //プレイヤーマテリアルの取得
        playerMaterial = gameObject.GetComponent<Renderer>();
        //色の設定
        playerMaterial.material.color = playerColor[0].color;

        canvas = GameObject.Find("Canvas");
        //HPの初期化。
        InitializeHitpoint();
        InitializeWizardParameter();
    }
    
    //キャラクターの移動
    //返り値が移動する方向のベクトル
    public Vector3 MovePlayerWizard(){
            Vector3 inputMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //斜めの距離が長くなるのを防ぐため正規化しておく
	        return inputMove.normalized * GetMoveWizardSpeed();
    }


    //魔法弾の発射
    public void ShotMagicAttack(Vector3 playerPosition){
        if (Input.GetMouseButtonDown(0))
        {
            
            //変換後のマウス座標を取得
            Vector3 mouseClickPosition = MouseCursortoPlanePosition();
            //プレハブを指定位置(自分の座標+マウス方向の少し前方)に生成
            GameObject Ishell = PhotonNetwork.Instantiate("Prefabs/MagicAttack",  playerPosition + (mouseClickPosition - playerPosition).normalized/1.0f, Quaternion.identity);
            //shellTargetPositionを引数としてshellの中のスクリプトの関数を呼び出す。
            Ishell.GetComponent<Shell>().InitializeMagicAttackParameters(magicAttackSpeed,magicAttackReflectNum);
            Ishell.GetComponent<Shell>().SetImpactPosition(mouseClickPosition);
        }
    }

    //罠の設置
    public void SetMagicTrap(Vector3 playerPosition){
        if (Input.GetMouseButtonDown(1))
        {
            //プレハブを指定位置に生成
            GameObject IMagicTrap = PhotonNetwork.Instantiate("Prefabs/MagicTrap", playerPosition, Quaternion.identity);
            //IMagicTrap.GetComponent<MagicTrap>().InitializeMagicTrapParameters(GetPlayerName(),GetMagicTrapExplosionRadius(),GetMagicTrapDetectionRadius());
        }
    }

    //マウスカーソルの座標を平面上の座標へ変換
    public Vector3 MouseCursortoPlanePosition()
    {
        //スクリーン座標のクリック位置取得
            Vector3 mouseCursor = Input.mousePosition;
            mouseCursor.z = 10.0f;
            //スクリーン座標からワールド座標へ変換
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(mouseCursor);

            //地面の上方向のベクトル
            var n = land.transform.up;
            //地面上の任意の点
            var x = land.transform.position + new Vector3(0,1.0f,0);
            //カメラの位置
            var x0 = mainCamera.transform.position;
            //カメラからクリックした点を通るベクトル
            var m = targetWorldPosition  - mainCamera.transform.position;
            //二つのベクトルの内積
            var h = Vector3.Dot(n, x);
            //平面上へ座標を変換
            return x0 + ((h - Vector3.Dot(n, x0)) / (Vector3.Dot(n, m))) * m;
    }

    public void InitializeHitpoint(){
        for(currentHitPoint = 0;currentHitPoint < GetMaxHitPoint();currentHitPoint++){
            //UIを生成
            hitPointMarkArray[currentHitPoint] = PhotonNetwork.Instantiate("Prefabs/HitPointHeart",  new Vector3(-900 ,500 - (100 * currentHitPoint),0), Quaternion.identity);
            hitPointMarkArray[currentHitPoint].transform.SetParent(canvas.transform, false);
        }
            
    }

    public void UpdateHitPoint(){
        //ライフの減少。
        currentHitPoint--;
        //UIのライフを一つ削除する。
        if(currentHitPoint >= 0){
            Destroy(hitPointMarkArray[currentHitPoint]);
        }

        return;    
    }

    

    //ダメージを受けた際に実行される関数
    public void OnPlayerDameged(){
        //被弾時の無敵時間だったら実行しない
        if(characterState == CHARACTERSTATE.Damaged){
            return ;
        }
        //ライフを減らす処理
        UpdateHitPoint();

        //ライフが0になったらゲームオーバー。
        if(currentHitPoint == 0){
            characterState = CHARACTERSTATE.GameOver;
            return ;
        }
        //被弾状態の開始
        StartCoroutine(BlinkPlayer());
    }

    //一定間隔で点滅を行うコルーチン
    private IEnumerator BlinkPlayer()
	{
		characterState = CHARACTERSTATE.Damaged;
        bool isTransparent = false;
        //無敵時間の残り時間と点滅間隔の時間。
		float blinkedTotalTime = hitInvincibleTime;
		float blinkedTime = transparentTime;

		while (blinkedTotalTime > 0) {
			blinkedTotalTime -= Time.deltaTime;
			blinkedTime -= Time.deltaTime;

			if (blinkedTime < 0) {
				blinkedTime = transparentTime;
                //点滅の切り替え。
                isTransparent = !isTransparent;
                SwitchBlinkMaterial(!isTransparent);

			}
            //1フレーム待機
            yield return null;
		}
        //ここが点滅の終了時の処理。
        characterState = CHARACTERSTATE.Normal;

        //最後には必ずRendererを有効にする(消えっぱなしになるのを防ぐ)。
        SwitchBlinkMaterial(true);
		yield break;
	}

    //点滅時のマテリアルの切り替えを行う(trueならalpha=1,falseならalpha=0)
    private void SwitchBlinkMaterial(bool isTransparent)
	{
        //trueなら透明(alpha=0)にする。
        if(isTransparent == true && playerMaterial.sharedMaterial.color.a == 0){
        //メモリリーク対策にsharedMaterialを使う。
            playerMaterial.sharedMaterial.color += new Color(0, 0, 0, 1.0f);
        }
        else if(isTransparent == false && playerMaterial.sharedMaterial.color.a == 1.0f){
            playerMaterial.sharedMaterial.color -= new Color(0, 0, 0, 1.0f);
        }
        return ;
	}

    
        //ギアによるステータスの加算乗算するための値を格納する変数。
        //基礎ステータスにaddは加算、multiplyは乗算する。
        
            //プレイヤーライフ増加ギアパワー。
            private int addGearPower_maxHitPoint = 0;
            //プレイヤーの移動速度増加ギアパワー。
            private float multiplyGearPower_moveWizardSpeed = 1.0f;
            //魔法弾の最大存在数増加ギアパワー。
            private int addGearPower_maxMagicAttack = 0;
            //魔法弾の速度増加ギアパワー。
            private float multiplyGearPower_magicAttackSpeed = 1.0f;
            //魔法弾の反射回数増加ギアパワー。
            private int addGearPower_magicAttackReflectNum = 1;
            //罠の最大設置数増加ギアパワー。
            private int addGearPower_maxMagicTrap = 0;
            //罠の爆発半径拡大ギアパワー。
            private float multiplyGearPower_magicTrapExplosionRadius = 1.0f;
            //罠の探知半径拡大ギアパワー。
            private float multiplyGearPower_magicTrapDetectionRadius = 1.0f;
        
    //ギアパワーを含めたパラメータの初期化。
    private void  InitializeWizardParameter()
    {
        //プレイヤーのライフ
        maxHitPoint = GetMaxHitPoint() + addGearPower_maxHitPoint;
        //プレイヤーの移動速度
        moveWizardSpeed = GetMoveWizardSpeed() * multiplyGearPower_moveWizardSpeed;
        //魔法弾の最大存在数
        maxMagicAttack = GetMaxMagicAttack() + addGearPower_maxMagicAttack;
        //魔法弾の現在存在数
        countMagicAttack = 0;
        //魔法弾の速度
        magicAttackSpeed = GetMagicAttackSpeed() * multiplyGearPower_magicAttackSpeed;
        //魔法弾の反射回数
        magicAttackReflectNum = GetMagicAttackReflectNum() + addGearPower_magicAttackReflectNum;
        //罠の最大設置数
        maxMagicTrap = GetMaxMagicTrap() + addGearPower_maxMagicTrap;
        //罠の現在設置数
        countMagicTrap = 0;
        //罠の爆発半径
        magicTrapExplosionRadius = GetMagicTrapExplosionRadius() * multiplyGearPower_magicTrapExplosionRadius;
        //罠の探知半径
        magicTrapDetectionRadius = GetMagicTrapDetectionRadius() * multiplyGearPower_magicTrapDetectionRadius;
    }
    
    
    

    
}
