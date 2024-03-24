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
    //以下4つが、上4つの変数に対応するテキストを表示する
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

    //実際に表示される情報を、現在のRoomInfo/OwnerID/IsSetKeyword/CurMemberNumの値に合わせて変更する。
    public void ResetTextMeshsText() 
    {
        _roomNameTextMesh.text = RoomName;
        _ownerTextMesh.text = $"{OwnerID}";
        _isSetKeywordTextMesh.text = IsSetKeyword ? "あり" :"無し";
        _CurMemberNumTextMesh.text =$"{CurMemberNum}/4";
        _dispDescription.DispMessage = $"部屋「{RoomName}」に参加する(残り2名)";
    }
}
