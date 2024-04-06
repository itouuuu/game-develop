using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientStateOverlay : MonoBehaviour
{
    public GameObject ClientStateOverlayObj;
    public TextMeshProUGUI ClientStateOverlayTextMesh;

    public GameObject InRoomOverlayObj;
    public TextMeshProUGUI InRoomOverlayTextMesh;
    public Button LeaveRoomButton;

    // Start is called before the first frame update
    void Start()
    {
        LeaveRoomButton.onClick.AddListener(() => { PhotonNetwork.LeaveRoom(); });
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
