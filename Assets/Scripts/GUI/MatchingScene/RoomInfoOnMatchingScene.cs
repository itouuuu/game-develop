using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfoOnMatchingScene : MonoBehaviour
{
    public string RoomName;
    public int OwnerID;
    public bool IsSetKeyword;
    public int CurMemberNum;
    //�ȉ�4���A��4�̕ϐ��ɑΉ�����e�L�X�g��\������
    public TextMeshProUGUI roomNameTextMesh;
    public TextMeshProUGUI ownerTextMesh;
    public TextMeshProUGUI isSetKeywordTextMesh;
    public TextMeshProUGUI curMemberNumTextMesh;
    public Button button;

    public DispDescriptionOnHover dispDescription;
    public GameObject waitingMatchingOverlayObj;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!waitingMatchingOverlayObj) 
        {
            waitingMatchingOverlayObj = GameObject.Find("WaitingOverlay");
        }
        if (!dispDescription) 
        {
            GameObject descriptionObj=GameObject.Find("Description");
            if (descriptionObj) 
            {
                dispDescription = descriptionObj.GetComponent<DispDescriptionOnHover>();
            }
        }
        button.onClick.AddListener(OnClicked);
        ResetTextMeshsText();

        

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void Reset()
    {
        button=GetComponent<Button>();
        dispDescription = GetComponent<DispDescriptionOnHover>();
        Transform temp = this.transform.Find("RoomName");
        roomNameTextMesh=temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("RoomOwner");
        ownerTextMesh =temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("KeywordExistence");
        isSetKeywordTextMesh = temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("MemberNum");
        curMemberNumTextMesh = temp.GetComponent<TextMeshProUGUI>();

    }

    public void SetValues(JoinRoomSettingOnMatchingScene.Roominfo roomInfo) 
    {
        RoomName = roomInfo.Name;
        OwnerID = roomInfo.OwnerID;
        IsSetKeyword = roomInfo.IsSetKeyword;
        CurMemberNum = roomInfo.CurMemberNum;
        ResetTextMeshsText();
    }

    //���ۂɕ\�����������A���݂�RoomInfo/OwnerID/IsSetKeyword/CurMemberNum�̒l�ɍ��킹�ĕύX����B
    public void ResetTextMeshsText() 
    {
        roomNameTextMesh.text = RoomName;
        ownerTextMesh.text = $"{OwnerID}";
        isSetKeywordTextMesh.text = IsSetKeyword ? "����" :"����";
        curMemberNumTextMesh.text =$"{CurMemberNum}/4";
        dispDescription.DispMessage = $"�����u{RoomName}�v�ɎQ������(�c��2��)";
    }

    public void OnClicked() 
    {
        PhotonNetwork.JoinRoom(RoomName);
        waitingMatchingOverlayObj.SetActive(true);
    }
}
