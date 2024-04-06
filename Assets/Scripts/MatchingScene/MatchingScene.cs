using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public override void OnConnectedToMaster()
    {

        //現在立っている部屋の情報を得る為にロビーに入る
        if (PhotonNetwork.IsConnected)
        {
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


    
}
