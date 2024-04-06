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

    [SerializeField] GameObject _loadingOnConnectingObj;
    private List<string> _cachedRoomNames=new List<string>();

    public TextMeshProUGUI ClientStateOverlayTextMesh;
    public GameObject ClientStateOverlayObj;

    public TextMeshProUGUI InRoomOverlayTextMesh;
    public GameObject InRoomOverlayObj;
    public Button LeaveRoomButton;

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
            //ルームをでて、マスターサーバーに接続
            PhotonNetwork.LeaveRoom();
        }
        //サーバーに繋がって、ロビーにもルームにもいないなら
        else 
        {
            //ロビーに入る
            PhotonNetwork.JoinLobby();
        }
        LeaveRoomButton.onClick.AddListener(() => { PhotonNetwork.LeaveRoom(); });
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
        switch (PhotonNetwork.NetworkClientState) 
        {
            //ClientStateOverlayObjを表示を変更させる
            case ClientState.PeerCreated:
                ClientStateOverlayTextMesh.text = "サーバーに接続中…";
                break;
            case ClientState.ConnectedToNameServer:
                ClientStateOverlayTextMesh.text = "サーバーに接続中…";
                break;
            case ClientState.ConnectingToNameServer:
                ClientStateOverlayTextMesh.text = "サーバーに接続中…";
                break;
            case ClientState.ConnectingToMasterServer:
                ClientStateOverlayTextMesh.text = "サーバーに接続中…";
                break;
            case ClientState.JoiningLobby:
                ClientStateOverlayTextMesh.text = "ロビーに接続中…";
                break;
            case ClientState.Joining:
                ClientStateOverlayTextMesh.text = $"部屋に参加中";
                break;
            case ClientState.Joined:
                InRoomOverlayTextMesh.text = $"他の参加者を待っています…({PhotonNetwork.CurrentRoom.PlayerCount}/4)";
                break;


        }
        if (PhotonNetwork.NetworkClientState == ClientState.JoinedLobby)
        {
            //ロビーにいるときは2つのStateを表すオーバーレイを非表示にする
            ClientStateOverlayObj.SetActive(false);
            InRoomOverlayObj.SetActive(false);
            ClientStateOverlayTextMesh.text = "サーバーに接続中…";
        }
        else if (PhotonNetwork.NetworkClientState == ClientState.Joined)
        {
            //ルームにいるときはInRoomOverlayを表示する。
            InRoomOverlayObj.SetActive(true);
            ClientStateOverlayObj.SetActive(false);
        }
        else 
        {
            //それ以外の時はClientStateOverlayを表示する
            ClientStateOverlayObj.SetActive(true);
            InRoomOverlayObj.SetActive(false);
        }
    }
}
