using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    //試合時間
    public static float CountdownGameTime = 100;
    //
    public TextMeshProUGUI CountdownGameTimeText;
    void Start()
    {
         CountdownGameTimeText.SetText("Time:" + CountdownGameTime);
    }

    // Update is called once per frame
    void Update()
    {
        //残り時間が無くなればゲーム終了
        if(CountdownGameTime  <= 0.0f){
            CountdownGameTime = 0.0f;
            CountdownGameTimeText.SetText("Time:" + CountdownGameTime);
            //ゲーム終了の処理を呼び出す
        }else{
        CountdownGameTimeText.SetText("Time:" + CountdownGameTime);
        CountdownGameTime -= Time.deltaTime;
        }
        
    }
}
