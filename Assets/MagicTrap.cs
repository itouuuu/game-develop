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
    //プレイヤーのマテリアル
    private Material magicTrapMaterial;
    [SerializeField] Material[] magicTrapMaterialArray = new Material[2];


    void Start()
    {
        
        sphereCol = GetComponent<SphereCollider>();
        //プレイヤーマテリアルの取得
        magicTrapMaterial = gameObject.GetComponent<MeshRenderer>().material;
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
            //点滅で色変化し数秒後に爆発する処理。
            //点滅の開始。
            StartCoroutine(BlinkMagicTrap());
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "MagicAttack(Clone)" && Vector3.Distance(other.transform.position, this.transform.position) <= 1.0f) {
            // 衝突したら即爆発する
            ExplosionMagicTrap();
        }
    }

    //一定間隔で点滅を行うコルーチン
    private IEnumerator BlinkMagicTrap()
	{
        //点滅の残り時間。
		float blinkedTotalTime = 3.0f;
        //点滅間隔。
		float blinkedTime = 0.2f;

		while (blinkedTotalTime > 0) {
			blinkedTotalTime -= Time.deltaTime;
			blinkedTime -= Time.deltaTime;

			if (blinkedTime < 0) {
				blinkedTime = 0.2f;
                magicTrapMaterial = SwitchBlinkMateria(magicTrapMaterial);
                this.gameObject.GetComponent<MeshRenderer>().material = magicTrapMaterial;
			}
            //1フレーム待機
            yield return null;
		}
        //点滅終了後は爆発する。
        ExplosionMagicTrap();
		yield break;
	}

    //点滅時のマテリアルの切り替えを行う(trueならalpha=1,falseならalpha=0)
    private Material SwitchBlinkMateria(Material switchMaterial)
	{
        //
        if(switchMaterial == magicTrapMaterialArray[0]){
        //
            return magicTrapMaterialArray[1];
        }
        else {
            return magicTrapMaterialArray[0];
        }
	}



    //爆発時に呼ばれる関数
    private void ExplosionMagicTrap(){
        //爆発させる。
        GameObject IMagicExplosion = PhotonNetwork.Instantiate("Prefabs/MagicExplosion", this.transform.position, Quaternion.identity);
        //罠の削除。
        DeleteMagicTrap();
    }

    private void DeleteMagicTrap(){
        Destroy(this.gameObject);
    }
}
