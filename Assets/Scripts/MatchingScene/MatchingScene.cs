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
    public string BattleSceneName="TestScene";
    // Start is called before the first frame update
    void Start()
    {
        isTransitedNextScene=false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバー接続成功");
        _loadingOnConnectingObj.SetActive(false);

        //現在立っている部屋の情報を得る為にロビーに入る
        if (PhotonNetwork.IsConnected)
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

    bool isTransitedNextScene;

    // Update is called once per frame
    void Update()
    {
        if (!isTransitedNextScene && PhotonNetwork.InRoom&&PhotonNetwork.CurrentRoom.PlayerCount>=4) 
        {
            SceneManager.LoadScene(BattleSceneName);
            isTransitedNextScene = true;
        }
    }
}
