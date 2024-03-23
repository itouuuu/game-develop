using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class PhotonFindRoom : MonoBehaviourPunCallbacks
{


    private Dictionary<string, RoomInfo> cachedRoomList;

    // Start is called before the first frame update
    void Start()
    {
        cachedRoomList = new Dictionary<string, RoomInfo>();
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        Debug.Log("���}�X�^�[�T�[�o�[�ڑ�����");

        //���r�[�ɓ���
        if (PhotonNetwork.IsConnected)//�}�X�^�[�T�[�o�[�Ɛڑ����Ă���Ƃ��̓��r�[�ɓ��邱�Ƃ��ł���
        {
            PhotonNetwork.JoinLobby();
        }
    }


    //============================ROOM���X�g�̍X�V============================

    //�@ ���[�����X�g�ɍX�V�����������̃R�[���o�b�N
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("���[�����X�g���X�V����܂���");
        UpdateCachedRoomList(roomList);//�A
        UpdateRoomListView();//�B
    }

    //�A���[�����X�g�̍X�V
    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // �L���b�V�����ꂽ�������X�g������ꂽ�A�����Ȃ��Ȃ����A�܂��͍폜�ς݂Ƃ��ă}�[�N���ꂽ�ꍇ�A�L���b�V�����ꂽ�������X�g���畔�����폜����
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }
                continue;
            }
            // �L���b�V�����ꂽ���[�������X�V����
            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            // �L���b�V���ɐV������������ǉ�����
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }

    //�BNODE�̍X�V
    private void UpdateRoomListView()
    {
        //�L���b�V���ɃA�N�Z�X���Ă��܂���
        foreach (RoomInfo info in cachedRoomList.Values)
        {
            Debug.Log("������:" + info.Name + "�l���F" + (byte)info.PlayerCount + "Max:" + info.MaxPlayers);
        }
    }

}