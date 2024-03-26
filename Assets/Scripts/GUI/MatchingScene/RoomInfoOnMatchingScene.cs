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
    //以下4つが、上4つの変数に対応するテキストを表示する
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

    //実際に表示される情報を、現在のRoomInfo/OwnerID/IsSetKeyword/CurMemberNumの値に合わせて変更する。
    public void ResetTextMeshsText() 
    {
        roomNameTextMesh.text = RoomName;
        ownerTextMesh.text = $"{OwnerID}";
        isSetKeywordTextMesh.text = IsSetKeyword ? "あり" :"無し";
        curMemberNumTextMesh.text =$"{CurMemberNum}/4";
        dispDescription.DispMessage = $"部屋「{RoomName}」に参加する(残り2名)";
    }

    public void OnClicked() 
    {
        PhotonNetwork.JoinRoom(RoomName);
        waitingMatchingOverlayObj.SetActive(true);
    }
}
