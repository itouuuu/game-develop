using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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

    private List<string> _cachedRoomNames=new List<string>();




    // Start is called before the first frame update
    void Start()
    {
        
        //�T�[�o�[�Ɍq�����Ă��Ȃ��Ȃ�
        if (!PhotonNetwork.IsConnected)
        {
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
            PhotonNetwork.LeaveRoom();
        }
        //�T�[�o�[�Ɍq�����āA���r�[�ɂ����[���ɂ����Ȃ��Ȃ�
        else 
        {
            //���r�[�ɓ���
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

    public override void OnConnectedToMaster()
    {
        //���ݗ����Ă��镔���̏��𓾂�ׂɃ��r�[�ɓ���
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //�{���͕K�v�Ȃ��͂������A���[���̎Q���Ɏ��s����ƁA���r�[�ɂ����ԂȂ̂ɁA
        //PhotonNetwork.InLobby=false PhotonNetwork.NetworkClientState=ConnectingToMasterServer�̂悤�ɕϐ��̒l���o�O��̂ŁA��������s����B
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //�{���͕K�v�Ȃ��͂������A���[���̍쐬�Ɏ��s����ƁA���[���̎Q���Ɏ��s�������Ɠ��l�̃o�O���N����̂ŁA��������s����B
        PhotonNetwork.JoinLobby();
    }

    public void Update()
    {
    }
}

