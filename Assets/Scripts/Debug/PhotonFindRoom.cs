using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class PhotonFindRoom : MonoBehaviourPunCallbacks
{


    private Dictionary<string, RoomInfo> cachedRoomList;

    // Start is called before the first frame update
    void Start()
    {
        cachedRoomList = new Dictionary<string, RoomInfo>();
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("★マスターサーバー接続成功");

        //ロビーに入る
        if (PhotonNetwork.IsConnected)//マスターサーバーと接続しているときはロビーに入ることができる
        {
            PhotonNetwork.JoinLobby();
        }
    }


    //============================ROOMリストの更新============================

    //① ルームリストに更新があった時のコールバック
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("ルームリストが更新されました");
        UpdateCachedRoomList(roomList);//②
        UpdateRoomListView();//③
    }

    //②ルームリストの更新
    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // キャッシュされた部屋リストが閉じられた、見えなくなった、または削除済みとしてマークされた場合、キャッシュされた部屋リストから部屋を削除する
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }
                continue;
            }
            // キャッシュされたルーム情報を更新する
            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            // キャッシュに新しい部屋情報を追加する
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }

    //③NODEの更新
    private void UpdateRoomListView()
    {
        //キャッシュにアクセスしていまする
        foreach (RoomInfo info in cachedRoomList.Values)
        {
            Debug.Log("部屋名:" + info.Name + "人数：" + (byte)info.PlayerCount + "Max:" + info.MaxPlayers);
        }
    }

}