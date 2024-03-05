using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class WizardParameter : WizardPlayerStatus
{
    public CHARACTERSTATE characterState = CHARACTERSTATE.Normal;
    //プレイヤーのカメラ
    private Camera mainCamera;
    //ステージの床
    private GameObject land;
    //プレイヤーカラー
    private Material[] playerColor = new Material[8];
    //被弾時の無敵時間
    private static float hitInvincibleTime = 3.0f;
    //無敵時間の点滅間隔
    private static float transparentTime = 0.1f;
    private Renderer playerMaterial;


    private void Awake()
    {
        //地面オブジェクトの取得(もっといい方法がある？)
        land = GameObject.Find("Land");
        //メインカメラの取得
        mainCamera = Camera.main;
        //オブジェクトの色を用意したMaterialの色に変更する
        playerColor[0] = (Material)Resources.Load ("redMaterial");
        playerMaterial = gameObject.GetComponent<Renderer>();
        playerMaterial.material.color = playerColor[0].color;

        Debug.Log("wizadparametar awake");
    }
    
    //キャラクターの移動
    //返り値が移動する方向のベクトル
    public Vector3 MoveTank(){
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
            Ishell.GetComponent<Shell>().SetInitialMagicAttackParameters(GetMagicAttackSpeed(),GetMagicAttackReflectNum());
            Ishell.GetComponent<Shell>().SetImpactPosition(mouseClickPosition);
        }
    }

    //罠の設置
    public void SetMagicTrap(Vector3 playerPosition){
        if (Input.GetMouseButtonDown(1))
        {
            //プレハブを指定位置に生成
            PhotonNetwork.Instantiate("Prefabs/MagicTrap", playerPosition, Quaternion.identity);
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

    //被弾時の点滅を実行する
    public void OnPlayerDameged(){
        //被弾時の無敵時間だったら実行しない
        if(characterState == CHARACTERSTATE.Damaged){
            return ;
        }
        //ライフを減らす処理
        SetHitPoint(GetHitPoint() - 1);
        if(GetHitPoint() == 0){
            characterState = CHARACTERSTATE.GameOver;
            return ;
        }
        //被弾状態の開始
        StartCoroutine(BlinkPlayer());
    }

    IEnumerator BlinkPlayer()
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

    //点滅時のマテリアルの切り替えを行う
    void SwitchBlinkMaterial(bool isTransparent)
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


    
}
