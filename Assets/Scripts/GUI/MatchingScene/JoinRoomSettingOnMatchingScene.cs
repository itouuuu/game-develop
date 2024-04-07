using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomSettingOnMatchingScene : MonoBehaviourPunCallbacks
{

    public struct Roominfo
    {
        public string Name;
        public string ownerName;
        public int OwnerID;
        public string keyword;
        public int CurMemberNum;
        public bool IsAbleToEnter;
        public RoomInfoOnMatchingScene InfoDispObj;

        public Roominfo(string name,string ownerName,int ownerID, string keyword,int curMemberNum, RoomInfoOnMatchingScene infoDispObj,bool  isAbleToEnter) 
        {
            Name = name;
            this.ownerName = ownerName;
            OwnerID = ownerID;
            this.keyword = keyword;
            CurMemberNum = curMemberNum;
            InfoDispObj = infoDispObj;
            IsAbleToEnter = isAbleToEnter;
        }
    }

    [SerializeField]RoomInfoOnMatchingScene roomInfoPrefab;
    private List<Roominfo> _roomInfos = new List<Roominfo> ();

    [SerializeField] private Transform viewportContent;
    public MessageStackDescription messageStackDescription;
    public Button UpdateRoomListButton;
    public GameObject NoRoomNowObj;
    //public GameObject matchingWaitingOverlayObj;


    // Start is called before the first frame update
    void Start()
    {
        UpdateRoomListButton.onClick.AddListener(RemoveClosedRoomInfos);
        ResizeViewPortContent();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResizeViewPortContent()
    {
        float contentYSize=(20+20+100)*_roomInfos.Count+150;
        Vector2 contentScale= ((RectTransform)viewportContent).sizeDelta;
        contentScale.y = contentYSize;
        ((RectTransform)viewportContent).sizeDelta = contentScale;
    }

    public void DispNoRoomNowIfSo() 
    {
        if (_roomInfos.Count == 0)
        {
            NoRoomNowObj.SetActive(true);
        }
        else 
        {
            NoRoomNowObj.SetActive(false);
        }
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"PhotonNetwork.InLobby:{PhotonNetwork.InLobby}");

        Debug.Log($"���[�����X�g���X�V����܂���:{roomList.Count}");


        foreach (var aRoomInfo in roomList)
        {
            int aRoomIndex = _roomInfos.FindIndex((aData) => { return aData.Name == aRoomInfo.Name; });//���������X�g�ɂ��łɓ����Ă���ꍇ�A���̃C���f�b�N�X�B(�����łȂ��ꍇ-1)
            bool isRandom = aRoomInfo.CustomProperties.GetCastValue<string>(Consts.IS_RANDOM_MATCHING, "") == "true" ? true : false;//�����������_���}�b�`���O�p�̕�����
            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || aRoomInfo.RemovedFromList|| aRoomInfo.PlayerCount>=4|| isRandom)//���������X�g�����Ă���ׂ��Ŗ����Ȃ�(����Ȃ��Ȃ�)
            {
                if (aRoomIndex >= 0)//���łɃ��X�g�ɓ����Ă���Ȃ�
                {
                    //isAbleToEnter��false�ɂ��āA�\����ς���
                    Roominfo temp = _roomInfos[aRoomIndex];
                    temp.IsAbleToEnter = false;
                    _roomInfos[aRoomIndex] =temp;
                    _roomInfos[aRoomIndex].InfoDispObj.SetValues(_roomInfos[aRoomIndex]);
                    /*
                    if (_roomInfos[aRoomIndex].InfoDispObj) 
                    {
                        Destroy(_roomInfos[aRoomIndex].InfoDispObj.gameObject);//���̕����̏���\������GameObject������
                    }
                    _roomInfos.RemoveAt(aRoomIndex);//�����̏����i�[���Ă��郊�X�g���������
                    */
                }
                continue;
            }

            if (aRoomIndex < 0)//���X�g�ɓ����Ă��Ȃ��Ȃ�
            {
                string keyword = aRoomInfo.CustomProperties.GetCastValue<string>(Consts.ROOM_PASSWARD,"");
                string ownerNickName= aRoomInfo.CustomProperties.GetCastValue<string>(Consts.ROOM_OWNER_NICKNAME, "nickName");
                Debug.Log($"{aRoomInfo.Name}'sKeyword:{ keyword}");
                
                RoomInfoOnMatchingScene newInfoObj = Instantiate(roomInfoPrefab, viewportContent); //�����̏���\������GameObject���쐬
                //newInfoObj�ɃV�[�����̃R���|�[�l���g��GameObject�̎Q�Ƃ�n��
                //newInfoObj.waitingMatchingOverlayObj = matchingWaitingOverlayObj;
                _roomInfos.Add(new Roominfo(aRoomInfo.Name, ownerNickName, aRoomInfo.masterClientId, keyword, aRoomInfo.PlayerCount, newInfoObj,true));//���X�g�ɕ����̏���ǉ�
                newInfoObj.SetValues(_roomInfos[_roomInfos.Count - 1]);//�����̏���\������GameObject�ɁA����\������ׂ̏���n��
            }
            else    //���łɃ��X�g�ɓ����Ă���Ȃ�
            {
                Roominfo temp = _roomInfos[aRoomIndex];
                temp.CurMemberNum = aRoomInfo.PlayerCount;
                _roomInfos[aRoomIndex]=temp;//�����̏���\������GameObject�ɁA����\������ׂ̏���n��
                
            }
        }
        ResizeViewPortContent();
        DispNoRoomNowIfSo();
    }

    //_roomInfos����A���Ă��镔�������O�B�Ή�����Q�[���I�u�W�F�N�g���폜
    public void RemoveClosedRoomInfos() 
    {
        List<int> removedInfosIndexes=new List<int>();
        for ( int i=0;i<_roomInfos.Count;i++ ) 
        {
            Roominfo aRoomInfo = _roomInfos[i];
            if (!aRoomInfo.IsAbleToEnter) 
            {
                removedInfosIndexes.Add(i);
            }
        }
        for (int i = removedInfosIndexes.Count - 1; i >= 0; i--) 
        {
            RoomInfoOnMatchingScene infoDispObj = _roomInfos[removedInfosIndexes[i]].InfoDispObj;
            if (infoDispObj)
            {
                Destroy(infoDispObj.gameObject);//���̕����̏���\������GameObject������
            }
            _roomInfos.RemoveAt(removedInfosIndexes[i]);//�����̏����i�[���Ă��郊�X�g���������
        }
        ResizeViewPortContent();
        DispNoRoomNowIfSo();
    }
}
