using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JoinRoomSettingOnMatchingScene : MonoBehaviourPunCallbacks
{
    public struct Roominfo
    {
        public string Name;
        public int OwnerID;
        public bool IsSetKeyword;
        public int CurMemberNum;
        public RoomInfoOnMatchingScene InfoDispObj;

        public Roominfo(string name,int ownerID,bool isSetKeyword,int curMemberNum, RoomInfoOnMatchingScene infoDispObj) 
        {
            Name = name;
            OwnerID = ownerID;
            IsSetKeyword = isSetKeyword;
            CurMemberNum = curMemberNum;
            InfoDispObj = infoDispObj;
        }
    }

    [SerializeField]RoomInfoOnMatchingScene roomInfoPrefab;
    private List<Roominfo> _roomInfos = new List<Roominfo> ();

    [SerializeField] private Transform viewportContent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"���[�����X�g���X�V����܂���{roomList.Count}");
        if (roomList.Count > 0) 
        {
            Debug.Log($"RoomName:{roomList[0].Name}");
        }

        foreach (var aRoomInfo in roomList)
        {
            int aRoomIndex = _roomInfos.FindIndex((aData) => { return aData.Name == aRoomInfo.Name; });
            Debug.Log(!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || aRoomInfo.RemovedFromList);
            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || aRoomInfo.RemovedFromList)//���������X�g�����Ă���ׂ��Ŗ����Ȃ�
            {
                if (aRoomIndex >= 0)//���łɃ��X�g�ɓ����Ă���Ȃ�
                {
                    if (_roomInfos[aRoomIndex].InfoDispObj) 
                    {
                        Destroy(_roomInfos[aRoomIndex].InfoDispObj.gameObject);//���̕����̏���\������GameObject������
                    }
                    _roomInfos.RemoveAt(aRoomIndex);//�����̏����i�[���Ă��郊�X�g���������
                }
                continue;
            }

            if (aRoomIndex < 0)//���X�g�ɓ����Ă��Ȃ��Ȃ�
            {
                Debug.Log("�C���X�^���s�c�Ƃ�����`");
                RoomInfoOnMatchingScene newInfoObj = Instantiate(roomInfoPrefab, viewportContent); //�����̏���\������GameObject���쐬
                _roomInfos.Add(new Roominfo(aRoomInfo.Name, aRoomInfo.masterClientId, false, aRoomInfo.PlayerCount, newInfoObj));//�����̏����i�[���Ă��郊�X�g�ɂ��̕����̏���ǉ�
                newInfoObj.SetValues(_roomInfos[_roomInfos.Count - 1]);//�����̏���\������GameObject�ɁA����\������ׂ̏���n��
            }
            else    //���łɃ��X�g�ɓ����Ă���Ȃ�
            {
                Roominfo temp = _roomInfos[aRoomIndex];
                temp.CurMemberNum = aRoomInfo.PlayerCount;
                _roomInfos[aRoomIndex]=temp;//�����̏���\������GameObject�ɁA����\������ׂ̏���n��
            }
        }

    }
}
