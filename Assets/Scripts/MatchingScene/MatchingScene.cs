using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchingScene : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject _loadingOnConnectingObj;
    private List<string> _cachedRoomNames=new List<string>();
    //4人集まった時に遷移するシーンの名前
    // Start is called before the first frame update
    void Start()
    {
        
        //サーバーに繋がっていないなら
        if (!PhotonNetwork.IsConnected)
        {
            _loadingOnConnectingObj.SetActive(true);
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
            _loadingOnConnectingObj.SetActive(true);
            //ルームを去り、マスターサーバーに接続
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
        _loadingOnConnectingObj.SetActive(false);

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


    // Update is called once per frame
    void Update()
    {

    }
}
