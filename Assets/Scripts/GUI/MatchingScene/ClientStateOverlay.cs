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
