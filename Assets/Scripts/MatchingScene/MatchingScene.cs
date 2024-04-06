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

        //�T�[�o�[�Ɍq�����Ă��Ȃ��Ȃ�
        if (!PhotonNetwork.IsConnected)
        {
            _loadingOnConnectingObj.SetActive(true);
            //�}�X�^�[�T�[�o�[�ɐڑ�����
            PhotonNetwork.ConnectUsingSettings();
        }
        //�T�[�o�[�Ɍq�����āA���r�[�ɂ���Ȃ�
        else if (PhotonNetwork.InLobby)
        {
            //�������Ȃ�
        }
        //�T�[�o�[�Ɍq�����āA���[���ɂ���Ȃ�
        else if (PhotonNetwork.InRoom)
        {
            _loadingOnConnectingObj.SetActive(true);
            //���[�����łāA�}�X�^�[�T�[�o�[�ɐڑ�
            PhotonNetwork.LeaveRoom();
        }
        //�T�[�o�[�Ɍq�����āA���r�[�ɂ����[���ɂ����Ȃ��Ȃ�
        else 
        {
            //���r�[�ɓ���
            PhotonNetwork.JoinLobby();
        }
        LeaveRoomButton.onClick.AddListener(() => { PhotonNetwork.LeaveRoom(); });
    }

    public override void OnConnectedToMaster()
    {
        _loadingOnConnectingObj.SetActive(false);

        //���ݗ����Ă��镔���̏��𓾂�ׂɃ��r�[�ɓ���
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
            //ClientStateOverlayObj��\����ύX������
            case ClientState.PeerCreated:
                ClientStateOverlayTextMesh.text = "�T�[�o�[�ɐڑ����c";
                break;
            case ClientState.ConnectedToNameServer:
                ClientStateOverlayTextMesh.text = "�T�[�o�[�ɐڑ����c";
                break;
            case ClientState.ConnectingToNameServer:
                ClientStateOverlayTextMesh.text = "�T�[�o�[�ɐڑ����c";
                break;
            case ClientState.ConnectingToMasterServer:
                ClientStateOverlayTextMesh.text = "�T�[�o�[�ɐڑ����c";
                break;
            case ClientState.JoiningLobby:
                ClientStateOverlayTextMesh.text = "���r�[�ɐڑ����c";
                break;
            case ClientState.Joining:
                ClientStateOverlayTextMesh.text = $"�����ɎQ����";
                break;
            case ClientState.Joined:
                InRoomOverlayTextMesh.text = $"���̎Q���҂�҂��Ă��܂��c({PhotonNetwork.CurrentRoom.PlayerCount}/4)";
                break;


        }
        if (PhotonNetwork.NetworkClientState == ClientState.JoinedLobby)
        {
            //���r�[�ɂ���Ƃ���2��State��\���I�[�o�[���C���\���ɂ���
            ClientStateOverlayObj.SetActive(false);
            InRoomOverlayObj.SetActive(false);
            ClientStateOverlayTextMesh.text = "�T�[�o�[�ɐڑ����c";
        }
        else if (PhotonNetwork.NetworkClientState == ClientState.Joined)
        {
            //���[���ɂ���Ƃ���InRoomOverlay��\������B
            InRoomOverlayObj.SetActive(true);
            ClientStateOverlayObj.SetActive(false);
        }
        else 
        {
            //����ȊO�̎���ClientStateOverlay��\������
            ClientStateOverlayObj.SetActive(true);
            InRoomOverlayObj.SetActive(false);
        }
    }
}
