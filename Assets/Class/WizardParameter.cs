using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class WizardParameter : MonoBehaviourPunCallbacks
{
    //プレイヤーネーム
    string playerName;
    //戦車のヒットポイント
    private int hitPoint;
    //戦車の移動速度
    public float tankSpeed = 3;
    //砲弾のプレハブ
    private GameObject shell;
    //地雷のプレハブ
    private GameObject landmine;
    //地雷の設置数
    private int landmineNum = 1;

    //プレイヤーのカメラ
    private Camera mainCamera;
    //ステージの床
    private GameObject land;
    //プレイヤーカラー
    private Material[] playerColor = new Material[4];


    private void Awake()
    {
        //砲弾のプレハブ
        shell = (GameObject)Resources.Load ("Prefabs/Shell");
        //地雷のプレハブ
        landmine = (GameObject)Resources.Load ("Prefabs/Landmine");
        //地面オブジェクトの取得(もっといい方法がある？)
        land = GameObject.Find("Land");
        //メインカメラの取得
        mainCamera = Camera.main;
        //オブジェクトの色を用意したMaterialの色に変更する
        playerColor[0] = (Material)Resources.Load ("redMaterial");
        GetComponent<Renderer>().material.color = playerColor[0].color;

        Debug.Log("wizadparametar awake");
    }
    
    //戦車の移動
    //返り値が移動する方向のベクトル
    public Vector3 MoveTank(){
        //自身のオブジェクトのみ移動させる
        if(photonView.IsMine == true){
            Vector3 inputMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //斜めの距離が長くなるのを防ぐため正規化しておく
	        return inputMove.normalized * tankSpeed;
        }
        return Vector3.zero;
    }


    //砲弾の発射
    //現時点での進捗　押した位置の座標を取得できるようになりました
    public void ShotShell(Vector3 playerPosition){
        if (Input.GetMouseButtonDown(0))
        {
            //スクリーン座標のクリック位置取得
            Vector3 position = Input.mousePosition;
            position.z = 10.0f;
            //スクリーン座標からワールド座標へ変換
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(position);


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
            //マウスで押した点へ座標を変換
            var shellTargetPosition = x0 + ((h - Vector3.Dot(n, x0)) / (Vector3.Dot(n, m))) * m;

            
            //プレハブを指定位置(マウス方向の少し前方)に生成
            GameObject Ishell = PhotonNetwork.Instantiate("Prefabs/Shell",  playerPosition + shellTargetPosition.normalized, Quaternion.identity);
            //shellTargetPositionを引数としてshellの中のスクリプトの関数を呼び出したい
            Ishell.GetComponent<Shell>().SetImpactPosition(shellTargetPosition);
        }
    }

    //地雷の設置
    public void SetLandmine(Vector3 playerPosition){
        if (Input.GetMouseButtonDown(1))
        {
            //プレハブを指定位置に生成
            Instantiate(landmine, playerPosition, Quaternion.identity);
        }
    }



    
}
