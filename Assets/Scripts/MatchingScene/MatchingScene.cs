using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MatchingScene : MonoBehaviourPunCallbacks
{
    enum OnlineState 
    {
        Offline,
        EnteringMaster,
        InMaster,
        EnteringLobby,
        InLobby,
        EnteringRoom,
        InRoom
    }

    private List<string> _cachedRoomNames=new List<string>();




    // Start is called before the first frame update
    void Start()
    {
        
        //サーバーに繋がっていないなら
        if (!PhotonNetwork.IsConnected)
        {
            //マスターサーバーに接続する
            PhotonNetwork.ConnectUsingSettings();
        }
        //サーバーに繋がって、ロビーにいるなら
        else if (PhotonNetwork.InLobby)
        {
            //何もしない
        }
        //サーバーに繋がって、ルームにいるなら
        else if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        //サーバーに繋がって、ロビーにもルームにもいないなら
        else 
        {
            //ロビーに入る
            PhotonNetwork.JoinLobby();
        }
        
    }





    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var aRoomInfo in roomList) 
        {
            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || !aRoomInfo.RemovedFromList) 
            {
                if (_cachedRoomNames.Contains(aRoomInfo.Name)) 
                {
                    _cachedRoomNames.Remove(aRoomInfo.Name);
                }
                continue;
            }
            if (!_cachedRoomNames.Contains(aRoomInfo.Name))
            {
                _cachedRoomNames.Add(aRoomInfo.Name);
            }
        }
    }

    public List<string> CachedRoomNames
    {
        get { return _cachedRoomNames; }
    }

    public override void OnConnectedToMaster()
    {
        //現在立っている部屋の情報を得る為にロビーに入る
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //本来は必要ないはずだが、ルームの参加に失敗すると、ロビーにいる状態なのに、
        //PhotonNetwork.InLobby=false PhotonNetwork.NetworkClientState=ConnectingToMasterServerのように変数の値がバグるので、これを実行する。
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //本来は必要ないはずだが、ルームの作成に失敗すると、ルームの参加に失敗した時と同様のバグが起こるので、これを実行する。
        PhotonNetwork.JoinLobby();
    }

    public void Update()
    {
    }
}

