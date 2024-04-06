using NUnit.Framework;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomMatchingSettingOnMatchingScene : MonoBehaviourPunCallbacks
{
    public Button startMatchingButton;
    public List<(string roomName, int curMemberNum)> randomMatchingRoomCache=new List<(string roomName, int curMemberNum)>();

    // Start is called before the first frame update
    void Start()
    {
        startMatchingButton.onClick.AddListener(OnStartMatchingButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartMatchingButtonClicked() 
    {
        //�󂫂̂��郉���_���}�b�`���O�p�̕�����T���B
        for (int i = 0; i < randomMatchingRoomCache.Count; i++) 
        {
            //�󂫂�����ΎQ��
            if (randomMatchingRoomCache[i].curMemberNum < 4) 
            {
                PhotonNetwork.JoinRoom(randomMatchingRoomCache[i].roomName);
                Debug.Log("JoinedRandomRoom");
                return;
            }
        }
        int loopCounter = 0;
        string roomName = $"_______RandomRoom{loopCounter}";

        //�܂����݂��Ȃ������_���}�b�`���O�p�̕����̖��O��T��
        while (randomMatchingRoomCache.FindIndex((aData) => { return aData.roomName == roomName; }) >= 0)//�������O�̂̃����_���}�b�`���O�p�̕��������łɑ��݂��Ă������
        {
            loopCounter++;
            roomName = $"_______RandomRoom{loopCounter}";
        }

        ExitGames.Client.Photon.Hashtable roomProperty = new ExitGames.Client.Photon.Hashtable();
        roomProperty[Consts.IS_RANDOM_MATCHING] = "true";
        string[] publicPropsForLobby = new string[] { Consts .IS_RANDOM_MATCHING};


        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.CustomRoomProperties = roomProperty;
        roomOptions.CustomRoomPropertiesForLobby = publicPropsForLobby;
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        Debug.Log("CreateRandomRoom");
        //waitingMatchingOverlayObj.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo aRoomInfo in roomList) 
        {
            bool isRandom = aRoomInfo.CustomProperties.GetCastValue<string>(Consts.IS_RANDOM_MATCHING,"")=="true"?true:false;//���̕����������_���}�b�`���O�p�̕�����
            


            
            int cachedElementIndex = randomMatchingRoomCache.FindIndex((aData) => { return aData.roomName == aRoomInfo.Name; });
            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || aRoomInfo.RemovedFromList || !isRandom)//�����A���X�g����r�����ׂ������Ȃ�
            {
                if (cachedElementIndex >= 0)
                {
                    randomMatchingRoomCache.RemoveAt(cachedElementIndex);//���X�g����r��
                }
            }
            else if (cachedElementIndex >= 0)// ���ł�randomMatchingRoomCache�ɑ��݂��Ă��镔���̏��Ȃ�
            {
                //���̕����̏����X�V
                randomMatchingRoomCache[cachedElementIndex] = (randomMatchingRoomCache[cachedElementIndex].roomName, aRoomInfo.PlayerCount);
            }
            else //�܂�randomMatchingRoomCache�ɂȂ������̏��Ȃ�
            {
                //���̕����̏���ǉ�
                randomMatchingRoomCache.Add((aRoomInfo.Name, aRoomInfo.PlayerCount));
            }
            
        }

        Debug.Log($"CurrentRandomRoomNum:{randomMatchingRoomCache.Count}");
    }

}
