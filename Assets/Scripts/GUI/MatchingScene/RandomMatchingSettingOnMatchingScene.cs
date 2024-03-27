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
    public GameObject waitingMatchingOverlayObj;
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
                waitingMatchingOverlayObj.SetActive(true);
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
        roomProperty["IsRandom"] = "true";
        string[] publicPropsForLobby = new string[] { "IsRandom" };


        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.CustomRoomProperties = roomProperty;
        roomOptions.CustomRoomPropertiesForLobby = publicPropsForLobby;
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        Debug.Log("CreateRandomRoom");
        waitingMatchingOverlayObj.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo aRoomInfo in roomList) 
        {
            bool isRandom = false;//���̕����������_���}�b�`���O�p�̕�����
            if (aRoomInfo.CustomProperties.ContainsKey("IsRandom") && aRoomInfo.CustomProperties["IsRandom"] is string && ((string)aRoomInfo.CustomProperties["IsRandom"]) == "true") //�J�X�^���v���p�e�B���݂Ĕ��f
            {
                isRandom = true;
            }

            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || aRoomInfo.RemovedFromList|| !isRandom) 
            {
                continue;
            }
            int cachedElementIndex = randomMatchingRoomCache.FindIndex((aData) => { return aData.roomName == aRoomInfo.Name; });
            if (cachedElementIndex >= 0)// ���ł�randomMatchingRoomCache�ɑ��݂��Ă��镔���̏��Ȃ�
            {
                randomMatchingRoomCache[cachedElementIndex] = (randomMatchingRoomCache[cachedElementIndex].roomName, aRoomInfo.PlayerCount);
            }
            else //�܂�randomMatchingRoomCache�ɂȂ������̏��Ȃ�
            {
                randomMatchingRoomCache.Add((aRoomInfo.Name, aRoomInfo.PlayerCount));
            }
            
        }

        Debug.Log($"CurrentRandomRoomNum:{randomMatchingRoomCache.Count}");
    }

}
