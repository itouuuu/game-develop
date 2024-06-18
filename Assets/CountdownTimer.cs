using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    //試合時間
    public static float Countdown_InGameTime = 10;
    //試合前の待機時間
    public static float Countdown_BeforeGameTime = 3;
    //試合前の待機時間を表示するテキスト。
    public TextMeshProUGUI Countdown_BeforeGameTimeText;
    //試合時間を表示するテキスト。
    public TextMeshProUGUI Countdown_InGameTimeText;
    void Start()
    {
        Countdown_InGameTimeText.enabled = false ;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    //試合開始前のカウントダウンを行うコルーチン。
    public IEnumerator BeforeGameCountDownTime()
	{
        while(true){
            //残り時間が無くなればゲーム終了
            if(Countdown_BeforeGameTime  <= 0.0f){
                Countdown_BeforeGameTime = 0.0f;
                Countdown_BeforeGameTimeText.enabled = false;
                //SetGameState_Ingame();
        		yield break;
            }else{
                Countdown_BeforeGameTime -= Time.deltaTime;
            }
            //1フレーム待機
            yield return null;
        }
    }

    //試合時間のカウントダウンを行うコルーチン。
    public IEnumerator InGameCountDownTime()
	{
        Countdown_InGameTimeText.enabled = true;
        //試合時間の表示の初期化。
        Countdown_InGameTimeText.SetText("Time:" + Countdown_InGameTime);
        while(true){
            //残り時間が無くなればゲーム終了
            if(Countdown_InGameTime  <= 0.0f){
                Countdown_InGameTime = 0.0f;
                Countdown_InGameTimeText.SetText("Time:" + Countdown_InGameTime);
                //ゲーム終了の処理を呼び出す
        		yield break;
            }else{
                Countdown_InGameTimeText.SetText("Time:" + Countdown_InGameTime);
                Countdown_InGameTime -= Time.deltaTime;
            }
            //1フレーム待機
            yield return null;
        }
    }
}
