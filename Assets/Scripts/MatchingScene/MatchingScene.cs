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
        Debug.Log("���}�X�^�[�T�[�o�[�ڑ�����");
        _loadingOnConnectingObj.SetActive(false);

        //���r�[�ɓ���
        if (PhotonNetwork.IsConnected)//�}�X�^�[�T�[�o�[�Ɛڑ����Ă���Ƃ��̓��r�[�ɓ��邱�Ƃ��ł���
        {
            PhotonNetwork.JoinLobby();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
