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
    //4�l�W�܂������ɑJ�ڂ���V�[���̖��O
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
            //���[��������A�}�X�^�[�T�[�o�[�ɐڑ�
            PhotonNetwork.LeaveRoom();
        }
        //�T�[�o�[�Ɍq�����āA���r�[�ɂ����[���ɂ����Ȃ��Ȃ�
        else 
        {
            //���r�[�ɓ���
            PhotonNetwork.JoinLobby();
        }
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

    }
}
