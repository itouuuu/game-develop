using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScene : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject _loadingOnConnectingObj;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
