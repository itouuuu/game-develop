/*
    ゲーム全体の進行を管理するスクリプト。
    例として試合が今どのような状況か(試合開始前、試合中、試合終了など)

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameProgressionManager : MonoBehaviourPunCallbacks
{
    public GameObject countdownTimerObject;
    public CountdownTimer countdownTimer;
    
    //試合の進行状況。
    public enum GAMESTATE{
        BeforeGame,//試合前。
        InGame,//試合中。
        AfterGame//試合終了後。
    }

    //試合の進行状況を示す変数。
    public static GAMESTATE gameState = GAMESTATE.BeforeGame;

    

    // Start is called before the first frame update
    void Start()
    {
        countdownTimer = countdownTimerObject.GetComponent<CountdownTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.L)){
            SetGameState_Ingame();
        }
    }

    //gameStateをBeforeGameに切り替える。
    public void SetGameState_Beforegame(){
        //既にBeforeGameであれば切り替えない。
        if(gameState != GAMESTATE.BeforeGame){
            gameState = GAMESTATE.BeforeGame;
            StartCoroutine(countdownTimer.BeforeGameCountDownTime());
        }
    }


    //gameStateをInGameに切り替える。
    public static void SetGameState_Ingame(){
        //既にInGameであれば切り替えない。
        if(gameState != GAMESTATE.InGame){
            gameState = GAMESTATE.InGame;
            //StartCoroutine(countdownTimer.InGameCountDownTime());
        }
    }

    //gameStateをAfterGameに切り替える。
    public void SetGameState_Aftergame(){
        //既にInGameであれば切り替えない。
        if(gameState != GAMESTATE.AfterGame){
            gameState = GAMESTATE.AfterGame;
        }
    }
}
