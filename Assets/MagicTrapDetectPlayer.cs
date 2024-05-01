using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTrapDetectPlayer : MagicTrap
{
    //プレイヤーを探知するためのコライダー。
    private CapsuleCollider capsuleCol;
    
    //親になっている罠のマテリアル。
    private Material magicTrapMaterial;
    //親になっている罠のゲームオブジェクト。
    [SerializeField] private GameObject magicTrapObject;
    //爆発前の点滅に使用するマテリアル。
    [SerializeField] Material[] magicTrapMaterialArray = new Material[2];

    private float magicTrapDetectionRadius = 0;
    void Start()
    {
        //マテリアルの取得
        magicTrapMaterial = magicTrapObject.GetComponent<MeshRenderer>().material;
        capsuleCol = GetComponent<CapsuleCollider>();
        
        capsuleCol.radius = magicTrapDetectionRadius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Debug.Log("プレイヤーが探知範囲に入りました");
            //点滅し色変化し数秒後に爆発する処理。開始。
            StartCoroutine(BlinkMagicTrap());
            //一度爆破待機状態になったらコライダーは消す。
            capsuleCol.enabled = false;
        }
        
    }

    //罠生成時に呼び出され初期化を行う
    public void SetMagicTrapParameters(float setMagicTrapDetectionRadius){
        //コライダーの半径を探知半径にする。
        magicTrapDetectionRadius = setMagicTrapDetectionRadius;
    }

    //一定間隔で点滅を行うコルーチン
    public IEnumerator BlinkMagicTrap()
	{
        //点滅の残り時間。
		float blinkedTotalTime = 5.0f;
        //点滅間隔。
		float blinkedTime = 0.2f;

        //点滅時間が残っている間は続ける。
		while (blinkedTotalTime > 0) {
            //時間の経過。
			blinkedTotalTime -= Time.deltaTime;
			blinkedTime -= Time.deltaTime;

            //点滅の周期が来たら次の色へ変化させる。
			if (blinkedTime < 0) {
				blinkedTime = 0.2f;
                magicTrapMaterial = SwitchBlinkMateria(magicTrapMaterial);
                magicTrapObject.GetComponent<MeshRenderer>().material = magicTrapMaterial;
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

    
}
