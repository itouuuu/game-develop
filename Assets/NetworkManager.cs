using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "TemplateRoom"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("TemplateRoom", new RoomOptions(), TypedLobby.Default);
    }

    // ルームへの参加が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {
        // 自身のアバター（ネットワークオブジェクト）を生成する
        var p = new Vector3(0, 0, 0);
        PhotonNetwork.Instantiate("Prefabs/PlayerWizard", p, Quaternion.identity);
    }
}
