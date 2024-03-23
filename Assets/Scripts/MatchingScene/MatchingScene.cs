using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScene : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _loadingOnConnectingObj;
    private List<string> _cachedRoomNames=new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("★マスターサーバー接続成功");
        _loadingOnConnectingObj.SetActive(false);

        //ロビーに入る
        if (PhotonNetwork.IsConnected)//マスターサーバーと接続しているときはロビーに入ることができる
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Debug.Log("ルームリストが更新されました");
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
        //Debug.Log(_cachedRoomNames.Count);
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
