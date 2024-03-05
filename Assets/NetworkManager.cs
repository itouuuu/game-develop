using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class NetworkManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField roomNameInput;
    void Start()
    {
        //PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する。
        PhotonNetwork.ConnectUsingSettings();
    }

    //マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "TemplateRoom"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        //CreateRoom();
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    //ルームへの参加が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {
        // 自身のアバター（ネットワークオブジェクト）を生成する
        var p = new Vector3(0, 0, 0);
        PhotonNetwork.Instantiate("Prefabs/PlayerWizard", p, Quaternion.identity);
    }

    //ルームの作成
    public void CreateRoom()
    {
        string createRoomName = roomNameInput.text;

        //ルーム名が入力されてないなら自動生成
        if(createRoomName == "")
        {
            createRoomName  = "TemplateRoom";// + Random.Range(1000, 9999);
        }
        //ルームのオプションの設定
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        //最大参加人数
        roomOptions.MaxPlayers = 2;
        // ==指定したルーム名と同じルーム名が存在している場合、PhotonNetworkの方で作成できないようになっている==
        PhotonNetwork.CreateRoom(createRoomName, roomOptions , TypedLobby.Default);
    }
}
