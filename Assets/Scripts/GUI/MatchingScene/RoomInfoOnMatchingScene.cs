using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomInfoOnMatchingScene : MonoBehaviour
{
    public string RoomName;
    public int OwnerID;
    public bool IsSetKeyword;
    public int CurMemberNum;
    //�ȉ�4���A��4�̕ϐ��ɑΉ�����e�L�X�g��\������
    [SerializeField] private TextMeshProUGUI _roomNameTextMesh;
    [SerializeField] private TextMeshProUGUI _ownerTextMesh;
    [SerializeField] private TextMeshProUGUI _isSetKeywordTextMesh;
    [SerializeField] private TextMeshProUGUI _CurMemberNumTextMesh;

    [SerializeField]private DispDescriptionOnHover _dispDescription;
    // Start is called before the first frame update
    void Start()
    {
        ResetTextMeshsText();

        

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void Reset()
    {
        _dispDescription = GetComponent<DispDescriptionOnHover>();
        Transform temp = this.transform.Find("RoomName");
        _roomNameTextMesh=temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("RoomOwner");
        _ownerTextMesh =temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("KeywordExistence");
        _isSetKeywordTextMesh = temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("MemberNum");
        _CurMemberNumTextMesh = temp.GetComponent<TextMeshProUGUI>();

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
        _roomNameTextMesh.text = RoomName;
        _ownerTextMesh.text = $"{OwnerID}";
        _isSetKeywordTextMesh.text = IsSetKeyword ? "����" :"����";
        _CurMemberNumTextMesh.text =$"{CurMemberNum}/4";
        _dispDescription.DispMessage = $"�����u{RoomName}�v�ɎQ������(�c��2��)";
    }
}
